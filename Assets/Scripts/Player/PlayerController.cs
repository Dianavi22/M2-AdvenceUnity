using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 grapplePoint;
    public bool _isGrappling = false;

    [Header("References")]
    [SerializeField] private SlowMotion _slowMo;

    [Header("Components")]
    [SerializeField] Camera _myCam;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _hookPrefab;

    [Header("Value")]
    [SerializeField] private float _hookSpeed = 20f;
    [SerializeField] private float _circleRadius = 2.35f;
    [SerializeField] private float _grappleMaxDistance = 2f;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _speedVertical = 4f;
    [SerializeField] private float _grappleSpeed = 10f;
    [SerializeField] float distanceGrapplingHook = 0;

    [Header("Swing Parameters")]
    [SerializeField] private float _swingAmplitude = 0.5f;
    [SerializeField] private float _swingFrequency = 2f;

    [Header("Visuel")]
    [SerializeField] ParticleSystem _fireGrappinPart;
    [SerializeField] ParticleSystem _collisionPart;
    [SerializeField] ParticleSystem _destroyHookPart;
    [SerializeField] GameObject _grapPointPartContener;
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSounds;
    [SerializeField] AudioClip _hitSounds;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] AudioClip _catchPlate;
    [SerializeField] AudioClip _destroyHookSound;

    private GameObject _spawnHook;
    private bool _canJump = false;
    private float _horizontal;
    private bool _isSuspended = false;

    void Start()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.positionCount = 2;
            _lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        FollowMouseWithCircle();

        if (Input.GetMouseButtonDown(0))
        {
            Hook();
        }

        if (_spawnHook != null)
        {
            distanceGrapplingHook = Vector3.Distance(transform.position, _spawnHook.transform.position);
            if (Input.GetMouseButtonUp(0) || (!_isGrappling && distanceGrapplingHook > _grappleMaxDistance))
            {

                _destroyHookPart.transform.position = _spawnHook.transform.position;
                _destroyHookPart.Play();
                _audioSounds.PlayOneShot(_destroyHookSound,0.5f);
                Destroy(_spawnHook);
            }
        }

        if (_isGrappling && Input.GetMouseButton(0))
        {
            Grapple();
            UpdateGrappleLine();
        }

        if (_isGrappling && Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("GrappleSurface"))
        {
            _collisionPart.Play();
            _audioSounds.PlayOneShot(_hitSounds, 0.6f);

        }
    }

    private void FollowMouseWithCircle()
    {
        Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, transform.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 cursorWorldPosition = ray.GetPoint(distance);
            Vector3 direction = (cursorWorldPosition - transform.position).normalized;
            Vector3 circlePosition = transform.position + direction * _circleRadius;
            _circle.transform.position = circlePosition;
        }
    }

    private void Hook()
    {
        _audioSounds.PlayOneShot(_shootSound, 0.5f);
        _spawnHook = Instantiate(_hookPrefab, _circle.transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z));
        Rigidbody hookRb = _spawnHook.GetComponent<Rigidbody>();
        Vector3 direction = (_circle.transform.position - transform.position).normalized;
        if (_slowMo.isSlowMo)
        {
            hookRb.velocity = direction * (_hookSpeed * 2);
        }
        else
        {
            hookRb.velocity = direction * _hookSpeed;
        }

        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
    }


    public void StartGrapple(Vector3 hitPoint, GameObject go)
    {
        grapplePoint = hitPoint;

        _isGrappling = true;
        _audioSounds.PlayOneShot(_catchPlate, 0.25f);
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, hitPoint);
        Vector3 direction = _spawnHook.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        _grapPointPartContener.transform.rotation = rotation;
        _fireGrappinPart.Play();
    }
    private void Grapple()
    {
        Vector3 direction = (grapplePoint - transform.position).normalized;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((direction *_grappleSpeed) * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && distanceGrapplingHook < _grappleMaxDistance)
        {
            rb.AddForce((-direction * _grappleSpeed) * _speedVertical * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce((Vector3.right) * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce((-Vector3.right) * _speed * Time.deltaTime);
        }

        _canJump = true;
    }


    public void StopGrapple()
    {
        if (_spawnHook != null)
        {
            try
            {
                _spawnHook.GetComponent<Hook>().ChangePlatMat();
            }
            catch
            {
                //OSEF
            }
        }
        _isGrappling = false;
        _isSuspended = false;
        Destroy(_spawnHook);
        _lineRenderer.enabled = false;
    }

    private void UpdateGrappleLine()
    {
        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, grapplePoint);
        }
    }

}