using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
   [HideInInspector] public Transform startTranform;
    public bool isInDeathZone = false;
    void Start()
    {
        startTranform = this.transform;

    }

    void Update()
    {
        if (isInDeathZone)
        {
            this.transform.position = new Vector3(0,0,0);
            isInDeathZone = false;
        }
    }
}
