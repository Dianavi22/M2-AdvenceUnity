using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlat : MonoBehaviour
{
    private PlayerController _playerController;
    public bool isDestroying = false;
    private LevelManager _levelManager;
    private GameObject _currentHook;
    [SerializeField] ParticleSystem _destroyPS;
    [SerializeField] ParticleSystem _respawnPS;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _destroyPlateSound;
    [SerializeField] AudioClip _respawnPlat;
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        _levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    }

    void Update()
    {

    }

    private void DestroyPlat()
    {
        _destroyPS.Play();
        _audioSource.PlayOneShot(_destroyPlateSound, 0.2f);
        isDestroying = true;
        if (_currentHook != null)
        {
            _playerController.StopGrapple();
        }
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    public IEnumerator RespawnPlat()
    {
        if (isDestroying)
        {
            _respawnPS.Play();
            isDestroying = false;
            _audioSource.PlayOneShot(_respawnPlat, 0.4f);
            yield return new WaitForSeconds(0.2f);
        }
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Hook") && !isDestroying && _levelManager.phase >= 4)
        {
            _currentHook = collision.collider.gameObject;
            Invoke("DestroyPlat", 2.8f);
        }
    }
}
