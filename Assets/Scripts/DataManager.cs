using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public float hightScore;
    public float minNbDeath;
    private bool isFirstPart = false;
    [SerializeField] GameManager _gameManager;
    [SerializeField] Timer _timer;
    

    public string hightScoreTxt;
    void Start()
    {
        if (PlayerPrefs.HasKey("HightScore"))
        {
            hightScore = PlayerPrefs.GetFloat("HightScore");
            minNbDeath = PlayerPrefs.GetFloat("MinNbDeath");

            isFirstPart = false;
        }
        else
        {
            isFirstPart = true;
        }

    }

    void Update()
    {
        if(_gameManager.isFinish && isFirstPart)
        {
            ShowTimer();
            hightScore = _timer.seconds;
            minNbDeath = _gameManager.nbDeath;
            PlayerPrefs.SetFloat("MinNbDeath", minNbDeath);
        }

    }

    public void ShowTimer()
    {
        hightScoreTxt = hightScore.ToString();
        float minute = Mathf.FloorToInt(hightScore / 60);
        float sec = Mathf.FloorToInt(hightScore % 60);
        if (sec < 10)
        {
            hightScoreTxt = minute + " :0" + sec.ToString();
        }
        else
        {
            hightScoreTxt = minute + " : " + sec.ToString();
        }
    }
}
