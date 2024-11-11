using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _gameManager.isFinish = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.isFinish = true;
        }
    }
}
