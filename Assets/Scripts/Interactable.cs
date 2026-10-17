using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    // Si un objeto es interactuable, se muestra "[E]" en pantalla

    public bool canInteract;
    [SerializeField] private GameObject text;
    public UnityEvent onInteract;

    public void ShowText(bool show)
    {
        if (text != null)
        {
            text.SetActive(show);
        }
    }
}
