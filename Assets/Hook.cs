using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float hookMaxDistance;
    [SerializeField] private bool _isHooked;
    [SerializeField] private Rigidbody _rb;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
    }

    public void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Update()
    {
        if (_playerController != null)
        {
            if(Vector3.Distance(transform.position, _playerController.transform.position) > hookMaxDistance
                && !_isHooked) {
                Destroy(gameObject);
            }    
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("GrappleSurface"))
            {
                ConfigurableJoint joint = collision.gameObject.GetComponent<ConfigurableJoint>();
                joint.connectedBody = _playerController.rb;


                Vector3 hitPoint = collision.contacts[0].point;
                _playerController.StartGrapple(hitPoint, collision.gameObject);
                _isHooked = true;
               _rb.isKinematic = true;
                gameObject.transform.position = hitPoint;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }
}
