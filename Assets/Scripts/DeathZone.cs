using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] Vector3 _spawnCoord;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _deathClip;
    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_deathClip,0.6f);
            GameObject _player = other.gameObject;
           StartCoroutine(_player.GetComponent<PlayerSetUp>().RespawnPlayer(_spawnCoord));
        }
    }
}
