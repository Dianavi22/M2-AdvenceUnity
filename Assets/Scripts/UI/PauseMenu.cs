using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseCanvas;
    [SerializeField] List<GameObject> _button;
    [SerializeField] ParticleSystem _borderPart;
    public bool isPause = false;
    void Start()
    {
        Time.timeScale = 1;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
        Time.timeScale = 0;
        isPause = true;
        _borderPart.Play();
        _pauseCanvas.SetActive(true);
    }

    public void Unpause()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
