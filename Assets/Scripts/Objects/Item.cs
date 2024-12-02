using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
