using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevalManager : MonoBehaviour
{
    [SerializeField] GameObject _trail;
    [SerializeField] List<Material> _trailMaterials = new List<Material>();
    public int phase;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartPhaseOne()
    {

    }

    public void PhaseTwo() {
        _trail.GetComponent<TrailRenderer>().material = _trailMaterials[phase+1];
    }
}
