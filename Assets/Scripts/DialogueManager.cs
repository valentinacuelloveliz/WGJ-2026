using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text lineText;

    string[] lines;
    int index;

    public static bool IsActive { get; private set; }

    void Start()
    {
        IsActive = false;
    }

    public void StartDialogue(string speaker, string[] newLines)
    {
        if (newLines == null || newLines.Length == 0) return;

        lines = newLines;
        index = 0;
        nameText.text = speaker;
        lineText.text = lines[0];
        panel.SetActive(true);
        IsActive = true;
    }

    void Update()
    {
        if (!IsActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            Next();
        }
    }

    void Next()
    {
        index++;

        if (index < lines.Length)
        {
            lineText.text = lines[index];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        panel.SetActive(false);
        IsActive = false;
    }
}