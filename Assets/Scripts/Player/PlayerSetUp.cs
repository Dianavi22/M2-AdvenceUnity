using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
   [HideInInspector] public Transform startTranform;
    private PlayerController _playerController;
    public bool isInDeathZone = false;
    [SerializeField] private Grounded _grounded;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private MeshRenderer _playerMeshRenderer;
    private bool _isDissolve = false;
    private static float _timeLerpDissolve = 0f;
    [SerializeField] ParticleSystem _respawnPart;


    void Start()
    {
        startTranform = this.transform;
        _playerController = this.GetComponent<PlayerController>();
        _rb = this.GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position = new Vector3(0,0,0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.transform.position = _levelManager.phaseRestart;
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }

        //DEBUG

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.transform.position = new Vector3(0, 0, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.transform.position = new Vector3(23, -20, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.transform.position = new Vector3(-64.5f, -24.5f, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.transform.position = new Vector3(-104.400002f, 27.2000008f, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.transform.position = new Vector3(38.3300018f, 2.8900001f, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            this.transform.position = new Vector3(40.7000008f, 68.1999969f, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            this.transform.position = new Vector3(34.9000015f, -85.3000031f, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            this.transform.position = new Vector3(-111.400002f, -49.5f, 0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }

        if (_isDissolve)
        {
            DissolvePlayer(minDissolve, maxDissolve);
        }
    }

    private float minDissolve;
    private float maxDissolve;
    
    public IEnumerator RespawnPlayer(Vector3 deathZone)
    {
        _timeLerpDissolve = 0;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        DespawnPlayer();
        yield return new WaitForSeconds(1.2f);
        _respawnPart.Play();
        yield return new WaitForSeconds(0.1f);
        _isDissolve = false;
        this.transform.position = deathZone;
        _cameraFollow.target = this.gameObject;

        yield return new WaitForSeconds(0.3f);
        AfterRespawn();
        yield return new WaitForSeconds(1f);
        _isDissolve = false;
    }

    private void DespawnPlayer()
    {
        minDissolve = -1;
        maxDissolve = 1;
        _isDissolve = true;
        
        _trailRenderer.time = 0;
        _rb.velocity = new Vector3(0, 0, 0);
        _playerController._isGrappling = false;
        isInDeathZone = false;
        this.gameObject.GetComponent<PlayerController>().StopGrapple();
        _playerController.enabled = false;
        _cameraFollow.target = null;
        _rb.useGravity = false;
    }

    private void AfterRespawn()
    {
        _rb.useGravity = true;
        minDissolve = 1;
        maxDissolve = -1;
        _trailRenderer.time = 1;
        _playerController.enabled = true;
        _rb.useGravity = true;
        _isDissolve = true;

    }

    private void DissolvePlayer(float min, float max)
    {
        _playerMeshRenderer.material.SetFloat("_DissolveAmount", Mathf.Lerp(min, max, _timeLerpDissolve));
        _timeLerpDissolve += 0.5f * Time.deltaTime;

    }

    public IEnumerator TPSetUp(Vector3 _newPosition)
    {
        _trailRenderer.time = 0;
        _rb.velocity = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<PlayerController>().StopGrapple();
        transform.position = _newPosition;
        yield return new WaitForSeconds(0.1f);
        _trailRenderer.time = 1;

    }
}
