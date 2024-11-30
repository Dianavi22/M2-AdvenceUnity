using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReelMainMenuManager : MonoBehaviour
{


    [SerializeField] GameObject _titleWiggle;
    [SerializeField] GameObject _title;
    [SerializeField] GameObject _credits;
    [SerializeField] GameObject _creditsTitle;

    [SerializeField] List<GameObject> _buttonPart = new List<GameObject>();
    [SerializeField] List<GameObject> _buttonSprite = new List<GameObject>();

    [SerializeField] TypeSentence typesSentence;

    [SerializeField] ParticleSystem _bolt1;
    [SerializeField] ParticleSystem _bolt2;
    [SerializeField] ParticleSystem _explision;
    [SerializeField] GameObject _reelMenu;
    [SerializeField] GameObject _reelMenuCanvas;

    [SerializeField] List<Button> _buttons;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _click;

    [SerializeField] Animator _animator;

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
