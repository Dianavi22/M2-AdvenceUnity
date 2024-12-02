using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    public bool isSlowMo = false;
    public int slowMoCount;
    [SerializeField] private Timer _timer;
    private bool _isInCD = false;
    [SerializeField] Slider _sliderSlowMo;
    private bool _isPlaying;
    [SerializeField] ParticleSystem _slowMoPart;
    [SerializeField] AudioSource _audioSourceSounds;
    [SerializeField] AudioClip _audioSlowMoClip;
    [SerializeField] AudioClip _audioStopSlowMoClip;

    private bool _isSoundSlowMoPlayed = false;
    private bool _isSoundStopSlowMoPlayed = true;
    void Start()
    {
        _sliderSlowMo.value = slowMoCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && slowMoCount > 0)
        {
            isSlowMo = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space)) {
            Time.timeScale = 1f;
            _timer._slowMoMulti = 1;
            isSlowMo = false;
        }
        if (isSlowMo && !_isInCD && slowMoCount > 0)
        {
            _isInCD = true;
            Time.timeScale = 0.5f;
            _timer._slowMoMulti = 2;
            StartCoroutine(SlowMoDecrement());
        }
        if (slowMoCount <= 0 && isSlowMo)
        {
            Time.timeScale = 1f;
            _timer._slowMoMulti = 1;
            isSlowMo = false;
        }
        _sliderSlowMo.value = slowMoCount;

        if (isSlowMo && !_isPlaying)
        {
            _isPlaying = true;
            _slowMoPart.Play();
        }
        else
        {
            _slowMoPart.Stop();
            _isPlaying = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && slowMoCount > 0 && !_isSoundSlowMoPlayed)
        {
            _audioSourceSounds.PlayOneShot(_audioSlowMoClip,0.3f);
            _isSoundSlowMoPlayed = true;
            _isSoundStopSlowMoPlayed = false;
        }
        if ((Input.GetKeyUp(KeyCode.Space) || slowMoCount <= 0) && !_isSoundStopSlowMoPlayed)
        {
            _isSoundStopSlowMoPlayed = true;
            _audioSourceSounds.PlayOneShot(_audioStopSlowMoClip, 0.35f);
            _isSoundSlowMoPlayed = false;
        }
    }

    private IEnumerator SlowMoDecrement()
    {
        slowMoCount--;
        yield return new WaitForSeconds(0.5f);
        _isInCD = false;

    }
}
