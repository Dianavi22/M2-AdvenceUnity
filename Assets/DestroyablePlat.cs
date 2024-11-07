using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlat : MonoBehaviour
{
    private PlayerController _playerController;
    private bool isDestroying = false;
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    public IEnumerator DestroyPlat()
    {
        if (!isDestroying)
        {
            isDestroying = true;
            yield return new WaitForSeconds(3);
            _playerController.StopGrapple();
            isDestroying = false;
            this.gameObject.SetActive(false);
        }
    }

    
}
