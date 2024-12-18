using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseCanvas;
    [SerializeField] List<GameObject> _button;
    [SerializeField] ParticleSystem _borderPart;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _click;
    [SerializeField] List<AudioSource> _sources;
    [SerializeField] SlowMotion _slowMo;
    public bool isPause = false;
    [SerializeField] GameManager _gameManager;
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !_gameManager.isFinish)
        {
            if (isPause)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        isPause = true;
        _slowMo.isSlowMo = false;
        Time.timeScale = 0;
        print(Time.timeScale);
        _audioSource.PlayOneShot(_click, 0.5f);
        for (int i = 0; i < _sources.Count; i++)
        {
            if (_sources[i].volume!= 0)
            {
                _sources[i].volume = 0.3f;
            }
        }
        _borderPart.Play();
        _pauseCanvas.SetActive(true);
    }

    public void Unpause()
    {
        _audioSource.PlayOneShot(_click, 0.5f);
        for (int i = 0; i < _sources.Count; i++)
        {
            if (_sources[i].volume != 0)
            {
                _sources[i].volume = 1f;
            }
        }
        Time.timeScale = 1;
        _borderPart.Stop();
        isPause = false;
        _pauseCanvas.SetActive(false);
        UpdateButtonScale();

    }

    private void UpdateButtonScale()
    {
        Vector3 scale = _button[0].GetComponent<Transform>().localScale;
        scale.x = 1;
        _button[0].GetComponent<Transform>().localScale = scale;

        Vector3 scale2 = _button[1].GetComponent<Transform>().localScale;
        scale2.x = 1;
        _button[1].GetComponent<Transform>().localScale = scale2;

        Vector3 scale3 = _button[2].GetComponent<Transform>().localScale;
        scale3.x = 1;
        _button[2].GetComponent<Transform>().localScale = scale3;
    }

    public void Retry()
    {
        _audioSource.PlayOneShot(_click, 0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        _audioSource.PlayOneShot(_click, 0.5f);
        SceneManager.LoadScene(0);
    }
}
