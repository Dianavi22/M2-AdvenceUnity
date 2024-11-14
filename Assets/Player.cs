using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem _takeItemPart;
  public void TakeItemVFX()
    {
        _takeItemPart.Play();
    }
    
}
