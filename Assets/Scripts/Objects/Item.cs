using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Item calls the TakeItem coroutine after contact
public class Item : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            other.GetComponent<PlayerEventLevel>().TakeItem();
            Destroy(gameObject);
        }
    }
}
