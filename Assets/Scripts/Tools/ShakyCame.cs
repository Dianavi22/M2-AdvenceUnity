using System.Collections;
using UnityEngine;

// Shaky cam, makes the camera shake based on a duration and a radius
public class ShakyCame : MonoBehaviour
{
    private Transform _pointToShake;
    private float _speed = 0;
    private Vector3 _offset;

    [Header("Configuration de la duree et de la distance de secousse")]
    private float _duration; 
    private float _radius; 

    private bool isShaking = false; 

    Vector3 center = Vector3.zero;

    private void Start()
    {
        _pointToShake = this.gameObject.transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _pointToShake.position + _offset, _speed * Time.deltaTime);
        if (isShaking)
        {
            isShaking = false;
            StartCoroutine(Shaking());
        }
    }

    public void ShakyCameCustom(float d, float r)
    {
        _duration = d;
        _radius = r;
        isShaking=true;
    }

    IEnumerator Shaking() 
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere * _radius + center;
            yield return null;
        }
        transform.position = startPosition;
    }
}
