using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public bool isReadyToBegin = false;

    [Header("References")]
    [SerializeField] TypeSentence _typeSentence;

    [Header("Components")]
    [SerializeField] TMP_Text _tutoTxtPosition;
    [SerializeField] TMP_Text _tutoTxtOkayPosition;
    [SerializeField] Rigidbody _rb;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSourceSoundsTuto;

    [SerializeField] List<string> stringList = new List<string>();

    private int i = 0;
    private bool _finishTuto = false;
    private Vector3 jump;
    private float jumpForce = 2.0f;
    private bool _canJump = true;
    void Start()
    {
        Time.timeScale = 1;
        jump = new Vector3(0.0f, 3.0f, 0.0f);
    }

    void Update()
    {
        if (!_typeSentence.isTyping && !_finishTuto)
        {
            StartCoroutine(TutoTexts());
        }

        if (_finishTuto && Input.GetKeyDown(KeyCode.Space))
        {
            _finishTuto = true;
            StartCoroutine(Jump());
        }
        if (Input.GetKeyDown(KeyCode.Space) && !_finishTuto)
        {
            _finishTuto = true;

            try
            {
                StopCoroutine(_typeSentence.TypeCurrentSentence(stringList[i], _tutoTxtPosition));
                _audioSourceSoundsTuto.gameObject.SetActive(false);
            }
            catch
            {
                //
            }
            _tutoTxtPosition.color = new Color32(0, 0, 0, 0);
            _tutoTxtOkayPosition.text = "Okay...";
            StartCoroutine(Jump());
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
    public IEnumerator Jump()
    {
        _canJump = false;
        _rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        isReadyToBegin = true;
    }

}
