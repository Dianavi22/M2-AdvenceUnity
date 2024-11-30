using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Ce script est a placer dans la hiérarchie (ou dans le canva directement)
// Il contient une fonction qui peut etre appelee partout s'il a la reference de se script 
// Il faut lui passer les parammtres : Textes a ecrire, emplacement du texte, secondes entre chaque caracteres
public class TypeSentence : MonoBehaviour
{
   // Parametres 
   private TMP_Text _textPlace;
   private string _textToShow;
    private float _timeBetweenChar; // Temps en Seconde
    public bool isTyping = false;
    [SerializeField] List<AudioClip> _key;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] ShakyCame _shakyCame;
    [SerializeField] bool _titleMode;

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
                _shakyCame._duration = 0.01f;
                _shakyCame._radius = 0.1f;
                _shakyCame.isShaking = true;
            }
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);

        isTyping = false;
    }
}
