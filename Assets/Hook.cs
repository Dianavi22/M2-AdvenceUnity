using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float hookMaxDistance;
    [SerializeField] private bool _isHooked;
    [SerializeField] private Rigidbody _rb;

    private PlayerController _playerController;
    [SerializeField] private ConfigurableJoint joint;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
    }

    public void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("GrappleSurface"))
            {
                joint = gameObject.AddComponent<ConfigurableJoint>();

                Vector3 hitPoint = collision.contacts[0].point;
                _playerController.StartGrapple(hitPoint, collision.gameObject);
                _isHooked = true;
                _rb.isKinematic = true;
                gameObject.transform.position = hitPoint;

                SetUpConfigurableJoint();
            }
        }
    }

    private void SetUpConfigurableJoint()
    {
        joint.connectedBody = _playerController.rb;
        joint.connectedAnchor = transform.position;
        joint.axis = new Vector3(1, 1, 0);
        joint.secondaryAxis = new Vector3(1, 1, 0);
        joint.anchor = transform.position;
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Locked;

        joint.autoConfigureConnectedAnchor = false;
        //joint.angularXMotion = ConfigurableJointMotion.Limited;
        //joint.angularYMotion = ConfigurableJointMotion.Limited;
        //joint.angularZMotion = ConfigurableJointMotion.Locked;

        SoftJointLimit limit = joint.linearLimit;
        limit.limit = 5f;
        limit.contactDistance = 1f;
        joint.linearLimit = limit;

        SoftJointLimitSpring limitSpring = joint.linearLimitSpring;
        limitSpring.spring = 2;
        limitSpring.damper = 5;
        joint.linearLimitSpring = limitSpring;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }
}
