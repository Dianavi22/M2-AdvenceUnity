using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseCanvas;
    private bool _isPause = false;
    void Start()
    {
        Time.timeScale = 1;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_isPause)
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
        _pauseCanvas.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
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
