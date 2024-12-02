using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PlayerEventLevel, a script that handles teleportation and items,
// which are specific events the player can encounter during the game
public class PlayerEventLevel : MonoBehaviour
{
    public bool isTeleport = false;

    [Header("References")]
    [SerializeField] GameManager _gameManager;
    [SerializeField] SlowMotion _slowmotion;
    [SerializeField] ShakyCame _shakyCame;

    [Header("Component")]
    [SerializeField] GameObject _gfx;
    [SerializeField] Slider _sliderSlowMo;
    [SerializeField] Rigidbody _rb;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSourceSounds;
    [SerializeField] AudioClip _audioTPInClip;
    [SerializeField] AudioClip _audioTPOutClip;
    [SerializeField] AudioClip _takeItemClip;

    [Header("Particules")]
    [SerializeField] ParticleSystem _takeItemPart;
    [SerializeField] ParticleSystem _tpScaleDownPart;
    [SerializeField] ParticleSystem _tpScaleUpPart;
    [SerializeField] ParticleSystem _takeItemSliderPart;

    private Vector3 initialScale;
    private float scaleDownDuration = 0.4f;
    private float scaleUpDuration = 0.4f;

    private void Start()
    {
        initialScale = _gfx.transform.localScale; 
    }
    private void Update()
    {
        if (isTeleport)
        {
            isTeleport = false;
            TeleportFX();
        }
    }
    public void TakeItem()
    {
        _slowmotion.slowMoCount += 10;
        _sliderSlowMo.value += _slowmotion.slowMoCount / 100;
        _gameManager.currentNumberItem++;
        _takeItemPart.Play();
        _takeItemSliderPart.Play();
        _audioSourceSounds.PlayOneShot(_takeItemClip, 2f);
        _shakyCame.ShakyCameCustom(0.05f, 0.2f);
    }

    #region TP
    public void TeleportFX()
    {
        _rb.useGravity = false;
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
        _rb.useGravity = true;
        isTeleport = false;
    }
    #endregion
}
