using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.isFinish = true;
        }
    }
}
