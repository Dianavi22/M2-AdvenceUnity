using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    public bool isSlowMo = false;
    public int slowMoCount;

    [Header("References")]
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Timer _timer;

    [Header("Components")]
    [SerializeField] Slider _sliderSlowMo;

    [Header("Visuel")]
    [SerializeField] ParticleSystem _slowMoPart;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSourceSounds;
    [SerializeField] AudioClip _audioSlowMoClip;
    [SerializeField] AudioClip _audioStopSlowMoClip;

    private bool _isInCD = false;
    private bool _isPlaying;
    private bool _isSoundSlowMoPlayed = false;
    private bool _isSoundStopSlowMoPlayed = true;
    void Start()
    {
        _sliderSlowMo.value = slowMoCount;
    }

    void Update()
    {
        #region Activation SlowMo
        if (Input.GetKeyDown(KeyCode.Space) && slowMoCount > 0)
        {
            isSlowMo = true;
            if (!_isSoundSlowMoPlayed)
            {
                _audioSourceSounds.PlayOneShot(_audioSlowMoClip, 0.3f);
                _isSoundSlowMoPlayed = true;
                _isSoundStopSlowMoPlayed = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) || slowMoCount <= 0)
        {
            Time.timeScale = 1f;
            _timer._slowMoMulti = 1;
            isSlowMo = false;
            if (!_isSoundStopSlowMoPlayed)
            {
                _isSoundStopSlowMoPlayed = true;
                _audioSourceSounds.PlayOneShot(_audioStopSlowMoClip, 0.35f);
                _isSoundSlowMoPlayed = false;
            }
        }
        #endregion

        if (isSlowMo)
        {
            _slowMoPart.Play();
            if(!_isInCD && slowMoCount > 0)
            {
                _isInCD = true;
                Time.timeScale = 0.5f;
                _timer._slowMoMulti = 2;
                StartCoroutine(SlowMoDecrement());
            }
        }
        else
        {
            _slowMoPart.Stop();
        }
        _sliderSlowMo.value = slowMoCount;

    }

    private IEnumerator SlowMoDecrement()
    {
        slowMoCount--;
        yield return new WaitForSeconds(0.5f);
        _isInCD = false;
    }
}
