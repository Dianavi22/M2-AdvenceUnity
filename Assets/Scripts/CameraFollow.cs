using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public GameObject target;
    //[SerializeField] private float _timeOffset;
    //[SerializeField]  private Vector3 _posOffset;
    //[SerializeField]  private Vector3 _posOffsetTab;
    //private Vector3 _velocity;
    //private bool _isTab;

    private Vector3 _thisTransform;

    private void Start()
    {
        _thisTransform = this.transform.position;

    }
    private void Update()
    {
        if(this.transform.position != _thisTransform)
        {
            this.transform.position = _thisTransform;
        }
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    print("_posOffsetTab");
        //    transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + _posOffsetTab, ref _velocity, _timeOffset);
        //    _isTab = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.Tab))
        //{
        //    _isTab = false;
        //}
        //if(target != null)
        //{
        //    transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + _posOffset, ref _velocity, _timeOffset);
        //}

    }
}
