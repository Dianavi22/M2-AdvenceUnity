using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _maxItem = 50;
    public int currentNumberItem;
    [SerializeField] TMP_Text _nbItemText;
    [SerializeField] GameObject _victoryCanvas;
    [SerializeField] GameObject _player;
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
        _player.SetActive(false);
        _victoryCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
