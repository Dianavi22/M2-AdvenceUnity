using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float hookMaxDistance;
    [SerializeField] private bool _isHooked;
    [SerializeField] private Rigidbody _rb;

    private LevelManager _levelManager;
    private PlayerController _playerController;

    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] Material _cyan;
    private Material _thisMat;
    private MeshRenderer _meshPlat;

    [SerializeField] TrailRenderer _trailRenderer;

    [SerializeField] List<Material> _levelMat = new List<Material>();
    [SerializeField] List<Color32> _colorsTails;
    [SerializeField] ParticleSystem _particleSystemTrail;
    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>().gameObject.GetComponent<LevelManager>();
        _playerController = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
    }

    public void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }


    private void Start()
    {
        this.GetComponent<Renderer>().material = _levelMat[_levelManager.phase - 1];
        _trailRenderer.startColor = _colorsTails[_levelManager.phase - 1];
        _particleSystemTrail.GetComponent<Renderer>().material = _levelMat[_levelManager.phase - 1];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !_isHooked)
        {
            if (collision.gameObject.CompareTag("GrappleSurface"))
            {
                joint = gameObject.AddComponent<ConfigurableJoint>();
                Vector3 hitPoint = collision.contacts[0].point;
                _playerController.StartGrapple(hitPoint, collision.gameObject);
                _isHooked = true;
                _rb.isKinematic = true;
                gameObject.transform.position = hitPoint;
                _thisMat = collision.collider.gameObject.GetComponent<MeshRenderer>().material;
                collision.collider.gameObject.GetComponent<MeshRenderer>().material = _cyan;
                SetUpHookGFX();
                _meshPlat = collision.collider.gameObject.GetComponent<MeshRenderer>();
                SetUpConfigurableJoint();
            }
        }
    }

    private void SetUpHookGFX()
    {
        _particleSystemTrail.Stop();
        _trailRenderer.time = 0;
        ParticleSystem _grapPart = this.GetComponentInChildren<ParticleSystem>();
        _grapPart.GetComponent<Renderer>().material = _levelMat[_levelManager.phase - 1];
        _grapPart.GetComponent<ParticleSystemRenderer>().trailMaterial = _levelMat[_levelManager.phase - 1];
        _grapPart.Play();
    }

    public void ChangePlatMat()
    {
        _meshPlat.gameObject.GetComponent<MeshRenderer>().material = _thisMat;
    }

    private void SetUpConfigurableJoint()
    {
        joint.connectedBody = _playerController.rb;
        joint.connectedAnchor = transform.position;
        joint.axis = new Vector3(1, 1, 0);
        joint.secondaryAxis = new Vector3(1, 1, 0);
        joint.anchor = new Vector3(0, 0, transform.position.z);
        joint.anchor = new Vector3(0, 0, transform.position.z);

        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Locked;

        joint.autoConfigureConnectedAnchor = false;

        SoftJointLimit limit = joint.linearLimit;
        limit.limit = 4f;
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
