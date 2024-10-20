using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private GameObject _endBumpGameObject;
    private bool isBumper = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (isBumper)
        {
            print("If bumperForce");
            BumperForce();
        }
        else {
            EndBumper();
        }
    }

    private void BumperForce()
    {
        print("BumperForce");
        _rb.useGravity = false;
        print(" _rb.useGravity "+_rb.useGravity);
        _controller.enabled = false;
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.AddForce(new Vector3(0,1,0)*0.1f, ForceMode.Impulse);
    }

    private void EndBumper()
    {
        _controller.enabled = true;
        _rb.useGravity = true;
        _rb.AddForce(Vector3.zero);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bumper") && !isBumper)
        {
            isBumper = true;
            print(isBumper);
        }   
        if(other.gameObject == _endBumpGameObject)
        {
            isBumper = false;
        }
    }
}
