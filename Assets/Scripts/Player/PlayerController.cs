using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Camera _myCam;

    private float _horizontal;
    [SerializeField] private float _speed = 4f;
    private float _jumpingPower = 15f;

    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;

    [SerializeField] private GameObject _circle;
    [SerializeField] private float _circleRadius = 2f;

    [SerializeField] private GameObject _hookPrefab;
    [SerializeField] private float _hookSpeed = 20f;
    [SerializeField] private float _grappleSpeed = 10f;
    [SerializeField] private float _grappleMaxDistance = 2f;
    [SerializeField] private float _grappleDistanceMargin = 1f;
    [SerializeField] private float _impulseStrength = 10f;

    [Header("Length of the Grappling Hook")]
    [SerializeField] float distance = 0;

    private GameObject _spawnHook;
    private Vector3 _grapplePoint;
    public bool _isGrappling = false;
    private bool _canJump = false;
    private bool _isSuspended = false;


    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Swing Parameters")]
    [SerializeField] private float _swingAmplitude = 0.5f; 
    [SerializeField] private float _swingFrequency = 2f; 

    void Start()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.positionCount = 2;
            _lineRenderer.enabled = false; 
        }
        else
        {
            Debug.LogError("LineRenderer non attribué dans l'inspecteur !");
        }
    }

    void Update()
    {
        FollowMouseWithCircle();

        if (Input.GetMouseButtonDown(0))
        {
            Hook();
        }

        if(_spawnHook != null)
            {
            distance = Vector3.Distance(transform.position, _spawnHook.transform.position);
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(_spawnHook);
            }
            if (!_isGrappling && distance > _grappleMaxDistance)
            {
                Destroy(_spawnHook);
            }
        }

        if (_isGrappling && Input.GetMouseButton(0))
            {
            Grapple();
            //Update visual
            UpdateGrappleLine();
        }

        if (_isGrappling && Input.GetMouseButtonUp(0))
        {
            StopGrapple();
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
        _spawnHook = Instantiate(_hookPrefab, _circle.transform.position, _circle.transform.rotation);
        Rigidbody hookRb = _spawnHook.GetComponent<Rigidbody>();

        Vector3 direction = (_circle.transform.position - transform.position).normalized;
        hookRb.velocity = direction * _hookSpeed;

        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position); 
        _lineRenderer.SetPosition(1, transform.position); 
    }

    public void StartGrapple(Vector3 hitPoint, GameObject go)
    {
        _grapplePoint = hitPoint;
        _isGrappling = true;

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, hitPoint);
    }
    private void Grapple()
    {
        Vector3 direction = (_grapplePoint - transform.position).normalized;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((direction * _grappleSpeed) * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && distance < _grappleMaxDistance)
        {
            rb.AddForce((-Vector3.up * _grappleSpeed) * _speed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce((Vector3.right - direction) * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce((-Vector3.right - direction) * _speed * Time.deltaTime);
        }

        _canJump = true;
    }


    private void StopGrapple()
    {
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
            _lineRenderer.SetPosition(1, _grapplePoint);      
        }
    }

}