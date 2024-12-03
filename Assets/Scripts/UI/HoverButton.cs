using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;    

    public void PlaySound()
    {
        if (audioSource != null && hoverSound != null && this.GetComponent<Button>().interactable)
        {
            audioSource.PlayOneShot(hoverSound, 0.23f);
        }
    }
}




