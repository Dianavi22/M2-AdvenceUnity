using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlat : MonoBehaviour
{
    void Start()
    {
        Spawn();
    }

    void Update()
    {
        
    }

    public IEnumerator DestroyPlat()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

    private void Spawn()
    {
        print("Spawn");
    }
}
