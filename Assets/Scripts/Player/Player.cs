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

    [SerializeField] AudioSource _audioSourceSounds;
    [SerializeField] AudioClip _audioTPInClip;
    [SerializeField] AudioClip _audioTPOutClip;
    [SerializeField] AudioClip _takeItemClip;

    [SerializeField] ShakyCame _shakyCame;

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
        _audioSourceSounds.PlayOneShot(_takeItemClip, 2f);
        _shakyCame._duration = 0.05f;
        _shakyCame.isShaking = true;
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
        _audioSourceSounds.PlayOneShot(_audioTPInClip, 0.8f);
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
        _audioSourceSounds.PlayOneShot(_audioTPOutClip, 0.8f);

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
