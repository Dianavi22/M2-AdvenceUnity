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
        isDestroying = true;
        if (_currentHook != null)
        {
            Destroy(_currentHook);
        }
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Hook") && !isDestroying && _levelManager.phase >= 4)
        {
            _currentHook = collision.collider.gameObject;
            Invoke("DestroyPlat", 2.3f);
        }
    }
}
