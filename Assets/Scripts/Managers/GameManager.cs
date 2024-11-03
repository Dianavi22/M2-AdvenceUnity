using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _maxItem = 50;
    [SerializeField] Timer _timer;
    public int currentNumberItem;
    [SerializeField] TMP_Text _nbItemText;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _hightScore;
    [SerializeField] GameObject _victoryCanvas;
    [SerializeField] GameObject _player;
    [SerializeField] DataManager _dataManager;
    [SerializeField] TMP_Text _newRecordTxt;
    public bool isFinish;
    void Start()
    {
        isFinish = false;
    }

    void Update()
    {
        _nbItemText.text = currentNumberItem.ToString();
        if (isFinish)
        {
            Victory();
        }
    }

    public void Victory()
    {
        isFinish = true;
        if (_timer.seconds <= _dataManager.hightScore)
        {
            _dataManager.hightScore = _timer.seconds;
            PlayerPrefs.SetFloat("HightScore", _dataManager.hightScore);
            _newRecordTxt.gameObject.SetActive(true);
        }
        _dataManager.ShowTimer();
        _hightScore.text = _dataManager.hightScoreTxt;
        _scoreText.text = _timer.scoreText.text.ToString();
        _player.SetActive(false);
        _victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
