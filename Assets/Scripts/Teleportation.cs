using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private GameObject _otbjectToTP;
    [SerializeField] private PlayerSetUp _playerSetUp;
    public bool isInTeleportation = false;
    void Start()
    {
        _playerSetUp = FindObjectOfType<PlayerSetUp>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerSetUp.isTp = true;
            isInTeleportation = false ;
            other.GetComponent<Player>().isTeleport = true;
            StartCoroutine(_playerSetUp.TPSetUp(_otbjectToTP.transform.position, this));
        }
    }
}
