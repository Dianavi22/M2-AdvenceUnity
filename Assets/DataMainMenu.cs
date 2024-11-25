using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataMainMenu : MonoBehaviour
{
    [SerializeField] GameObject _trueMenu;
    [SerializeField] GameObject _fakeMenu;
    private int _isTrueMenu;
    void Start()
    {
        if (PlayerPrefs.HasKey("Finished"))
        {
            _isTrueMenu = PlayerPrefs.GetInt("Finished");
            _isTrueMenu = 0;
        }
        else
        {
            if (_isTrueMenu == 1)
            {
                _fakeMenu.SetActive(false);
                _trueMenu.SetActive(true);
            }
            else
            {
                _trueMenu.SetActive(false);
                _fakeMenu.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
