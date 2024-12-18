using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReelMainMenuManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TypeSentence typesSentence;
    [SerializeField] ShakyCame _shakyCame;

    [Header("Components")]
    [SerializeField] GameObject _titleWiggle;
    [SerializeField] GameObject _title;
    [SerializeField] GameObject _credits;
    [SerializeField] GameObject _creditsTitle;
    [SerializeField] GameObject _reelMenu;
    [SerializeField] GameObject _reelMenuCanvas;
    [SerializeField] List<Button> _buttons;
    [SerializeField] List<GameObject> _buttonPart = new List<GameObject>();
    [SerializeField] List<GameObject> _buttonSprite = new List<GameObject>();

    [Header("Visuel")]
    [SerializeField] Animator _animator;
    [SerializeField] ParticleSystem _bolt1;
    [SerializeField] ParticleSystem _bolt2;
    [SerializeField] ParticleSystem _explision;
   
    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _audioSourceFakeMusic;
    [SerializeField] AudioSource _audioSourceMusic;
    [SerializeField] AudioClip _click;
    [SerializeField] AudioClip _boltSound;

    private bool _isTrueMenu;
    private bool _isCreditOpen = false;

    private void Start()
    {

        _reelMenu.SetActive(true);
        _reelMenuCanvas.SetActive(true);    
        typesSentence.WriteMachinEffect("Neon Swing Game", _title.GetComponent<TMP_Text>(), 0.09f);
        StartCoroutine(StyleButtons());
    }
    public void StartGame()
    {

        _audioSource.PlayOneShot(_click, 0.5f);
        SceneManager.LoadScene(1);
    }

    public void CreditButton()
    {
        _audioSource.PlayOneShot(_click, 0.5f);
        
        if (_isCreditOpen)
        {
            _credits.GetComponent<TMP_Text>().color = new Color(0,0,0,0);
            _creditsTitle.GetComponent<TMP_Text>().color = new Color(0,0,0,0);
            _titleWiggle.GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
            _isCreditOpen = false;
        }
        else
        {
            _titleWiggle.GetComponent<TMP_Text>().color = new Color(0, 0, 0, 0);
            _credits.GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
            _creditsTitle.GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
            _isCreditOpen = true;
        }
        _animator.SetTrigger("Normal");
    }

    public void QuitGame()
    {
        _audioSource.PlayOneShot(_click, 0.5f);
        Application.Quit();
    }

    private IEnumerator StyleButtons()
    {
        
        yield return new WaitForSeconds(1.3f);
        
        for (int i = 0; i < _buttonSprite.Count; i++)
        {
            _buttonSprite[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < _buttonPart.Count; i++)
        {
            _buttonPart[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        _bolt1.Play();
        _bolt2.Play();
        _shakyCame.ShakyCameCustom(0.3f, 0.3f);
        _audioSource.PlayOneShot(_boltSound, 0.5f);
        _audioSourceFakeMusic.volume = 0;
        _audioSourceMusic.volume = 0.7f;

        yield return new WaitForSeconds(0.2f);
        _title.SetActive(false);
        _titleWiggle.GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
        _explision.Play();
        ActiveButtons();
    }

    private void ActiveButtons()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].interactable = true;
        }
    }
}
