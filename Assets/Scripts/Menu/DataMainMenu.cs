using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataMainMenu : MonoBehaviour
{
    [SerializeField] ReelMainMenuManager _trueMenu;
    [SerializeField] MainMenuManager _fakeMenu;
    [SerializeField] private bool _isDebug;
    private int _isTrueMenu;
    void Start()
    {
        
        Time.timeScale = 1;
        if (_isDebug)
        {
            _isTrueMenu = 1;
            if (_isTrueMenu == 1)
            {
                _trueMenu.enabled = true;
            }
            else
            {
                _fakeMenu.enabled = true;
            }
        }
        else
        {
            if (!PlayerPrefs.HasKey("Finished"))
            {
                _isTrueMenu = PlayerPrefs.GetInt("Finished");
                PlayerPrefs.SetInt("Finished", 0);
                _fakeMenu.enabled = true;
            }
            else
            {
                _isTrueMenu = PlayerPrefs.GetInt("Finished");
                if (_isTrueMenu == 1)
                {
                    _trueMenu.enabled = true;
                }
                else
                {
                    _fakeMenu.enabled = true;
                }
            }
        }
    }
}
