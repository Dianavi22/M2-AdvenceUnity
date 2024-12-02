using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detect if player is on the floor
public class Grounded : MonoBehaviour
{
    public bool isGrounded;
  
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        print("isGrounded");

    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;

    }
}
