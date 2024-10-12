using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _maxItem = 50;
    public int currentNumberItem;
    [SerializeField] TMP_Text _nbItemText;
    void Start()
    {
        
    }

    void Update()
    {
        _nbItemText.text = currentNumberItem.ToString();
    }
}
