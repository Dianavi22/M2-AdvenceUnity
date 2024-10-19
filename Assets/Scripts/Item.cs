using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameManager _gameManager;
    private SlowMotion _slowmotion;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        _slowmotion = FindObjectOfType<SlowMotion>().GetComponent<SlowMotion>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _slowmotion.slowMoCount += 10;
            _gameManager.currentNumberItem++;
            Destroy(gameObject);
        }
    }
}
