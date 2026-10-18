using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private bool isPlayButton;

    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIAudio.Instance == null || !button.interactable) return;
        UIAudio.Instance.PlayHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIAudio.Instance == null || !button.interactable) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (isPlayButton)
        {
            UIAudio.Instance.PlayPlay();
        }
        else
        {
            UIAudio.Instance.PlayClick();
        }
    }
}