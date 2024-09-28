using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
    }
    public void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("GrappleSurface"))
        {
            Vector3 hitPoint = collision.contacts[0].point;
            _playerController.StartGrapple(hitPoint);
            }

      
            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }
}
