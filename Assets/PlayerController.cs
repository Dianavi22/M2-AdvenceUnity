using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float _grappleDistanceMargin = 1f;

    private GameObject _spawnHook;
    private Vector3 _grapplePoint;
    private bool _isGrappling = false;
    private bool _canJump = false;

    void Update()
    {
        if (!_isGrappling)
        {
            rb.velocity = new Vector2(_horizontal * -_speed, rb.velocity.y);
        }

        FollowMouseWithCircle();

        if (Input.GetMouseButtonDown(0))
        {
            Hook();
        }

        if (_isGrappling)
        {
            Grapple();
        }

        HandleFall();
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
        if (hookRb == null)
        {
            hookRb = _spawnHook.AddComponent<Rigidbody>();
        }

        Vector3 direction = (_circle.transform.position - transform.position).normalized;

        hookRb.velocity = direction * _hookSpeed;

        _spawnHook.AddComponent<Hook>().Initialize(this);
    }

    public void StartGrapple(Vector3 hitPoint)
    {
        _grapplePoint = hitPoint;
        _isGrappling = true;
    }

    private void Grapple()
    {
        Vector3 direction = (_grapplePoint - transform.position).normalized;

        rb.velocity = direction * _grappleSpeed;

        if (Vector3.Distance(transform.position, _grapplePoint) < _grappleDistanceMargin)
        {
            _isGrappling = false;
            rb.velocity = Vector2.zero;
        }
        _canJump = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpingPower);
            _canJump = false;
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void HandleFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }
}
