using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    [SerializeField] GameManager _gameManager;
    [SerializeField] Timer _timer;
    [HideInInspector] public string hightScoreTxt;
    [HideInInspector] public float hightScore;
    [HideInInspector] public float minNbDeath;
    private bool isFirstPart = false;

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
