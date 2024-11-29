using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem _takeItemPart;
    [SerializeField] ParticleSystem _tpScaleDownPart;
    [SerializeField] ParticleSystem _tpScaleUpPart;
    [SerializeField] ParticleSystem _takeItemSliderPart;
    public bool isTeleport = false;
    [SerializeField] GameObject _gfx;

    private Vector3 initialScale;
    public float scaleDownDuration = 1.0f;
    public float scaleUpDuration = 1.0f;

    private void Start()
    {
        initialScale = _gfx.transform.localScale; 
    }

    public void TakeItemVFX()
    {
        _takeItemPart.Play();
        _takeItemSliderPart.Play();
    }

    public void TeleportFX()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(ScaleDownCoroutine());
    }

    private IEnumerator ScaleDownCoroutine()
    {
        Vector3 targetScale = Vector3.zero;
        float elapsedTime = 0f;
        _tpScaleDownPart.Play();
        while (elapsedTime < scaleDownDuration)
        {
            _gfx.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDownDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _gfx.transform.localScale = targetScale;
        StartCoroutine(ScaleUpCoroutine());
    }

    private IEnumerator ScaleUpCoroutine()
    {
        Vector3 targetScale = initialScale;
        float elapsedTime = 0f;
        _tpScaleUpPart.Play();
        while (elapsedTime < scaleUpDuration)
        {
            _gfx.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / scaleUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _gfx.transform.localScale = targetScale;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        isTeleport = false;
    }

    private void Update()
    {
        if (isTeleport)
        {
            isTeleport = false; 
            TeleportFX();
        }
    }
}
