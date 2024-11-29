using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReelMainMenuManager : MonoBehaviour
{


    [SerializeField] GameObject _titleWiggle;
    [SerializeField] GameObject _title;
    [SerializeField] GameObject _credits;

    [SerializeField] List<GameObject> _buttonPart = new List<GameObject>();
    [SerializeField] List<GameObject> _buttonSprite = new List<GameObject>();

    [SerializeField] TypeSentence typesSentence;

    [SerializeField] ParticleSystem _bolt1;
    [SerializeField] ParticleSystem _bolt2;
    [SerializeField] ParticleSystem _explision;
    [SerializeField] GameObject _reelMenu;
    [SerializeField] GameObject _reelMenuCanvas;

    private bool _isTrueMenu;

    private void Start()
    {

        _reelMenu.SetActive(true);
        _reelMenuCanvas.SetActive(true);    
        typesSentence.WriteMachinEffect("Neon Swing Game", _title.GetComponent<TMP_Text>(), 0.09f);
        StartCoroutine(StyleButtons());
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditButton()
    {
        _titleWiggle.SetActive(false);
        _credits.SetActive(true);

    }

    public void QuitGame()
    {
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
    }
}
