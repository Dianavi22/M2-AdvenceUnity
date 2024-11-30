using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _credits;
    private bool _isActive;
    [SerializeField] GameObject _fakeMenuCanvas;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _fakeClick;
    void Start()
    {
        _fakeMenuCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    void Update()
    {

    }

    public void StartGame()
    {
        _audioSource.PlayOneShot(_fakeClick, 0.5f);
        SceneManager.LoadScene(1);
    }

    public void CreditButton()
    {
        _audioSource.PlayOneShot(_fakeClick, 0.5f);

        if (!_isActive)
        {

            _credits.SetActive(true);
            _isActive = true;
        }
        else
        {
            _credits.SetActive(false);
            _isActive = false;
        }

    }

    public void QuitGame()
    {
        _audioSource.PlayOneShot(_fakeClick, 0.5f);
        Application.Quit();
    }
}
