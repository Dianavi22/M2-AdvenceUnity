using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _maxItem = 50;
    [SerializeField] Timer _timer;
    public int currentNumberItem;
    public int nbDeath;
    [SerializeField] TMP_Text _nbItemText;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _scoreDeathText;
    [SerializeField] TMP_Text _hightScore;
    [SerializeField] TMP_Text _hightDeathScore;
    [SerializeField] GameObject _victoryCanvas;
    [SerializeField] GameObject _player;
    [SerializeField] DataManager _dataManager;
    [SerializeField] TMP_Text _newRecordTxt;
    [SerializeField] TMP_Text _newRecordDeathTxt;
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

        if (nbDeath < _dataManager.minNbDeath)
        {
            _dataManager.minNbDeath = nbDeath;
            PlayerPrefs.SetFloat("MinNbDeath", _dataManager.minNbDeath);
            _newRecordDeathTxt.gameObject.SetActive(true);
        }
        _dataManager.ShowTimer();
        _hightScore.text = _dataManager.hightScoreTxt;
        _hightDeathScore.text = _dataManager.minNbDeath.ToString();
        _scoreText.text = _timer.scoreText.text.ToString();
        _scoreDeathText.text = nbDeath.ToString();
        DisablePlayer();
        _victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    private void DisablePlayer()
    {
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponent<PlayerSetUp>().enabled = false;
    }
}
