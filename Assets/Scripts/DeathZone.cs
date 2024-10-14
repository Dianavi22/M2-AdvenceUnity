using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] Vector3 _spawnCoord;
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
            GameObject _player = other.gameObject;
            _player.GetComponent<PlayerSetUp>().DeathZone(_spawnCoord);
        }
    }
}
