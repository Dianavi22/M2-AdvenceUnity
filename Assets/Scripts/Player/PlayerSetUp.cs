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
    }

    
    public IEnumerator RespawnPlayer(Vector3 deathZone)
    {
        _trailRenderer.time = 0;
        _rb.velocity = new Vector3(0, 0, 0);
        this.transform.position = deathZone;
        _playerController._isGrappling = false;
        isInDeathZone = false;
        this.gameObject.GetComponent<PlayerController>().StopGrapple();
        _playerController.enabled = false;
        _cameraFollow.target = null;
        yield return new WaitForSeconds(0.5f);
        _cameraFollow.target = this.gameObject;
        _playerController.enabled = true;
        _trailRenderer.time = 1;


    }
}
