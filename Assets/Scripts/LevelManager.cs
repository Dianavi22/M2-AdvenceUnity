using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _trail;
    [SerializeField] List<Material> _trailMaterials = new List<Material>();
    public int phase;
    public Vector3 phaseRestart;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartPhaseOne()
    {
        _camera.clearFlags = CameraClearFlags.SolidColor;
    }

    public void PhaseTwo() {
        _trail.GetComponent<TrailRenderer>().material = _trailMaterials[phase+1];
    }
}
