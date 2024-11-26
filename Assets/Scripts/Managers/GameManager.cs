using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentNumberItem;
    public int nbDeath;
    [SerializeField] int _maxItem = 50;
    [SerializeField] GameObject _player;

    [Header("References")]
    [SerializeField] DataManager _dataManager;
    [SerializeField] ShakyCame _shakyCame;
    [SerializeField] Timer _timer;

    [Header("Particules")]
    [SerializeField] GameObject _victoryPart;
    [SerializeField] GameObject _playerVictoryPart;

    [Header("UI")]
    [SerializeField] GameObject _victoryCanvas;
    [SerializeField] TMP_Text _nbItemText;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _scoreDeathText;
    [SerializeField] TMP_Text _hightScore;
    [SerializeField] TMP_Text _hightDeathScore;
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
            StartCoroutine(VictoryEffect());
        }
    }

    #region Victory
    private IEnumerator VictoryEffect()
    {
        isFinish = true;
        _player.GetComponent<PlayerController>().rb.useGravity = false;
        _player.GetComponent<PlayerController>().rb.velocity = Vector3.zero;
        _victoryPart.SetActive(true);
        _shakyCame.isShaking = true;
        _playerVictoryPart.SetActive(true);
        yield return new WaitForSeconds(5);
        _shakyCame.enabled = false;
        Victory();

    }

    private void PlayerPrefsSetUp()
    {
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
        PlayerPrefs.SetInt("Finished", 1);
    }

    public void Victory()
    {
        PlayerPrefsSetUp();
        _dataManager.ShowTimer();
        SetUpTexts();
        DisablePlayer();
        _victoryCanvas.SetActive(true);
    }

    private void SetUpTexts()
    {
        _hightScore.text = _dataManager.hightScoreTxt;
        _hightDeathScore.text = _dataManager.minNbDeath.ToString();
        _scoreText.text = _timer.scoreText.text.ToString();
        _scoreDeathText.text = nbDeath.ToString();
    }

    private void DisablePlayer()
    {
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponent<PlayerSetUp>().enabled = false;
    }
    #endregion
}
