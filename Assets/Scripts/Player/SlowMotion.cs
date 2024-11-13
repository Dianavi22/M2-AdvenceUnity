using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    public bool _isSlowMo = false;
    public int slowMoCount;
    [SerializeField] private Timer _timer;
    private bool _isInCD = false;
    [SerializeField] Slider _sliderSlowMo;
    void Start()
    {
        _sliderSlowMo.value = slowMoCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && slowMoCount > 0)
        {
            _isSlowMo = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space)) {
            Time.timeScale = 1f;
            _timer._slowMoMulti = 1;
            _isSlowMo = false;
        }
        if (_isSlowMo && !_isInCD && slowMoCount > 0)
        {
            _isInCD = true;
            Time.timeScale = 0.5f;
            _timer._slowMoMulti = 2;
            StartCoroutine(SlowMoDecrement());
        }
        if (slowMoCount <= 0 && _isSlowMo)
        {
            Time.timeScale = 1f;
            _timer._slowMoMulti = 1;
            _isSlowMo = false;
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
