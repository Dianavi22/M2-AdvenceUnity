using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Type sentence, displays chars one by one based on a duration
public class TypeSentence : MonoBehaviour
{
    public bool isTyping = false;

    [Header("References")]
    [SerializeField] ShakyCame _shakyCame;

    [Header("Components")]
    private TMP_Text _textPlace;

    [Header("Audio")]
    [SerializeField] List<AudioClip> _key;
    [SerializeField] AudioSource _audioSource;

    [SerializeField] bool _titleMode;
    private string _textToShow;
    private float _timeBetweenChar;
    
    public void WriteMachinEffect(string _currentTextToShow, TMP_Text _currentTextPlace, float _currentTimeBetweenChar) // Fonction à appeler depuis un autre script
    {
        isTyping = true;
        _textToShow = _currentTextToShow;
        _textPlace = _currentTextPlace;
        _timeBetweenChar = _currentTimeBetweenChar;
        StartCoroutine(TypeCurrentSentence(_textToShow, _textPlace));
    }
    public IEnumerator TypeCurrentSentence(string sentence, TMP_Text place)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(_timeBetweenChar);
            place.text += letter;
            _audioSource.PlayOneShot(_key[Random.Range(0,_key.Count)], 0.4f);
            if (_titleMode)
            {
                _shakyCame.ShakyCameCustom(0.01f, 0.1f);
            }
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);

        isTyping = false;
    }
}
