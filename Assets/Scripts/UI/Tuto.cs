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
    [SerializeField] AudioSource _audioSourceSoundsTuto;
    [SerializeField] PlayerFirstMove PlayerFirstMove;
    private int i = 0;
    private bool _finishTuto = false;

    private Vector3 jump;
    private float jumpForce = 2.0f;
    public bool isReadyToBegin = false;
    [SerializeField] Rigidbody _rb;
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
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !_finishTuto)
        {
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
            _finishTuto = true;
            Jump();
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

    public void Jump()
    {
        _canJump = false;
        _rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        Invoke("Prepare", 1f);
    }

    private void Prepare()
    {
        isReadyToBegin = true;
    }

}
