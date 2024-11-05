using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseCanvas;
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
