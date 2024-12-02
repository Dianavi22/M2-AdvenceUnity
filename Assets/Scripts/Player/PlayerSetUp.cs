using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//PlayerSetUp is responsible for handling position changes during a despawn or teleportation
public class PlayerSetUp : MonoBehaviour
{
    [HideInInspector] public bool isInDeathZone = false;
    [HideInInspector] public bool isTp = false;

    [Header("References")]
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ShakyCame _shakyCam;
    [SerializeField] private Grounded _grounded;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SlowMotion _slowMo;

    [Header("Components")]
    [HideInInspector] public Transform startTranform;
    [SerializeField] private MeshRenderer _playerMeshRenderer;
    [SerializeField] private Camera _cam;
    private Rigidbody _rb;

    [Header("Visuel")]
    [SerializeField] ParticleSystem _respawnPart;
    [SerializeField] ParticleSystem _deathPart;
    [SerializeField] private TrailRenderer _trailRenderer;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _respawnPlayerSound;

    [Header("Debug")]
    [SerializeField] private bool isDebugMode = false;

    private static float _timeLerpDissolve = 0f;
    private bool _isDissolve = false;
    private float minDissolve;
    private float maxDissolve;

    void Start()
    {
        startTranform = this.transform;
        _rb = this.GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.transform.position = _levelManager.phaseRestart;
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }

        if (_isDissolve)
        {
            DissolvePlayer(minDissolve, maxDissolve);
        }
        _cam.gameObject.transform.localPosition = new Vector3(0, 4, 13);
        
        //DEBUG
        #region Debug TP
        if (isDebugMode)
        {
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
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                this.transform.position = new Vector3(34.9000015f, -85.3000031f, 0);
                _playerController._isGrappling = false;
                isInDeathZone = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                this.transform.position = new Vector3(-111.400002f, -49.5f, 0);
                _playerController._isGrappling = false;
                isInDeathZone = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                this.transform.position = new Vector3(-104, 70.4000015f, 0);
                _playerController._isGrappling = false;
                isInDeathZone = false;
            }
        }
        #endregion
    }

    public IEnumerator RespawnPlayer(Vector3 deathZone)
    {
        if (!isTp)
        {
            _slowMo.isSlowMo = false;
            _timeLerpDissolve = 0;
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
            DespawnPlayer();
            yield return new WaitForSeconds(1.2f);
            _respawnPart.Play();
            yield return new WaitForSeconds(0.1f);
            _isDissolve = false;
            this.transform.position = deathZone;
            _levelManager.SetColorPlateforms();
            _audioSource.PlayOneShot(_respawnPlayerSound, 0.6f);
            yield return new WaitForSeconds(0.3f);
            AfterRespawn();
            yield return new WaitForSeconds(1f);
            _isDissolve = false;
        }
    }

    private void DespawnPlayer()
    {
        _shakyCam.ShakyCameCustom(1, 0.3f);
        DespawnGFX();
        PlayerStateDeath();
        isInDeathZone = false;
        _gameManager.nbDeath++;
    }

    private void PlayerStateDeath()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        _playerController.StopGrapple();
        _playerController.enabled = false;
        _rb.useGravity = false;
    }

    private void DespawnGFX()
    {
        minDissolve = -1;
        maxDissolve = 1;
        _isDissolve = true;
        _deathPart.Play();
        _trailRenderer.time = 0;
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

    public IEnumerator TPSetUp(Vector3 _newPosition, Teleportation _tp)
    {
        yield return new WaitForSeconds(0.5f);
        _trailRenderer.time = 0;
        _rb.velocity = new Vector3(0, 0, 0);
        _playerController.StopGrapple();
        transform.position = _newPosition;
        yield return new WaitForSeconds(0.1f);
        _trailRenderer.time = 1;
        isTp = false;

    }
}
