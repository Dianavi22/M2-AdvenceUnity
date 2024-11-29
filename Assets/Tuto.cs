using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    [SerializeField] TypeSentence _typeSentence;
    [SerializeField] GameObject _player;
    [SerializeField] TMP_Text _tutoTxtPosition;
    [SerializeField] TMP_Text _tutoTxtOkayPosition;
    [SerializeField] List<string> stringList = new List<string>();
    private int i = 0;
    private bool _finishTuto = false;
    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!_typeSentence.isTyping && !_finishTuto)
        {
            StartCoroutine(TutoTexts());
        }

        if (_finishTuto)
        {
            IsFinishToReadTuto();
        }
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(_typeSentence.TypeCurrentSentence(stringList[i], _tutoTxtPosition));
            _tutoTxtPosition.color = new Color32(0, 0, 0, 0);
            _tutoTxtOkayPosition.text = "Okay...";
            _finishTuto = true;

        }
    }

    private IEnumerator TutoTexts()
    {
        _tutoTxtPosition.text = "";
        _typeSentence.WriteMachinEffect(stringList[i], _tutoTxtPosition, 0.1f);
        i++;
        if (i >= stringList.Count)
        {
            yield return new WaitForSeconds(2.5f);
            _finishTuto = true;
        }
    }

    private void IsFinishToReadTuto()
    {
        _player.GetComponent<PlayerFirstMove>().enabled = true;
    }

}
