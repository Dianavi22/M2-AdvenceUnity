using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private GameManager _gameManager;
    private SlowMotion _slowmotion;
    [SerializeField] Slider _sliderSlowMo;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        _slowmotion = FindObjectOfType<SlowMotion>().GetComponent<SlowMotion>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _slowmotion.slowMoCount += 10;
            _sliderSlowMo.value += _slowmotion.slowMoCount / 100;

            _gameManager.currentNumberItem++;
            Destroy(gameObject);
        }
    }
}
