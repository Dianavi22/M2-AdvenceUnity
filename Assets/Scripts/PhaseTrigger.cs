using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhaseTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private int _idCurrentLevel;
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.phase = _idCurrentLevel;
        }
    }
}
