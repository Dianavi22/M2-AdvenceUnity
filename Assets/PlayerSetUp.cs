using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
   [HideInInspector] public Transform startTranform;
    private PlayerController _playerController;
    public bool isInDeathZone = false;
    [SerializeField] private Grounded _grounded;
    [SerializeField] private Rigidbody _rb;

    void Start()
    {
        startTranform = this.transform;
        _playerController = this.GetComponent<PlayerController>();
        _rb = this.GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (isInDeathZone || Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position = new Vector3(0,0,0);
            _playerController._isGrappling = false;
            isInDeathZone = false;
        }
       

    }
}
