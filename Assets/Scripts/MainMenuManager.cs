using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _credits;
    private bool _isActive;
    void Start()
    {

    }

    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditButton()
    {
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
        Application.Quit();
    }
}
