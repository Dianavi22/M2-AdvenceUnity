using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Camera _myCam;

    private float _horizontal;
    private float _speed = 4f;
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
    private bool _isGrappling = false;
    private bool _canJump = false;
    private bool _isSuspended = false;

    [SerializeField] GameObject _hookedCube;

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
        distance = Vector3.Distance(_grapplePoint, transform.position);
        _hookedCube = go;
    }
    private void Grapple()
    {
        //gameObject.GetComponent<ConfigurableJoint>().connectedBody = _hookedCube.GetComponent<Rigidbody>();
        //gameObject.GetComponent<ConfigurableJoint>().anchor = _hookedCube.transform.position;
        Vector3 direction = (_grapplePoint - transform.position).normalized;
        distance = Vector3.Distance(transform.position, _grapplePoint);

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(direction * _grappleSpeed);
        }

        if (Input.GetKey(KeyCode.S) && distance < _grappleMaxDistance)
        {
            rb.AddForce(-direction * _grappleSpeed / 2);
        }

        //if (distance > _grappleMaxDistance)
        //{
        //    rb.AddForce(direction * 2f);
        //}

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.right + direction);
            rb.AddForce(new Vector3(direction.x * _grappleSpeed, direction.y, direction.z));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(-Vector3.right + direction);
        }

        _canJump = true;
    }


    private void StopGrapple()
    {
        _isGrappling = false;
        _isSuspended = false;  
        Destroy(_spawnHook);
        _lineRenderer.enabled = false;

        _hookedCube.GetComponent<ConfigurableJoint>().connectedBody = null;
    }

    private void UpdateGrappleLine()
    {
        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(0, transform.position);  
            _lineRenderer.SetPosition(1, _grapplePoint);      
        }
    }

    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    StopGrapple();

    //    if (context.performed && _canJump)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, _jumpingPower);
    //        _canJump = false;
    //    }

    //    if (context.canceled && rb.velocity.y > 0f)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    //    }
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }
}
