using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text lineText;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private float typeSpeed;

    DialogueNode[] nodes;
    DialogueNode current;
    int index;
    bool choosing;
    bool isTyping;
    Coroutine typingCoroutine;

    public static bool IsActive { get; private set; }

    void Start()
    {
        IsActive = false;
    }

    public void StartDialogue(string speaker, DialogueNode[] newNodes)
    {
        if (newNodes == null || newNodes.Length == 0) return;

        nodes = newNodes;
        nameText.text = speaker;
        panel.SetActive(true);
        IsActive = true;
        ShowNode(0);
    }

    void Update()
    {
        if (!IsActive || choosing) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                CompleteLine();
            }
            else
            {
                Next();
            }
        }
    }

    void ShowNode(int nodeIndex)
    {
        if (nodeIndex < 0 || nodeIndex >= nodes.Length)
        {
            EndDialogue();
            return;
        }

        current = nodes[nodeIndex];

        if (current.lines == null || current.lines.Length == 0)
        {
            EndDialogue();
            return;
        }

        index = 0;
        choosing = false;
        choicesPanel.SetActive(false);
        ShowLine(current.lines[0]);
    }

    void Next()
    {
        index++;

        if (index < current.lines.Length)
        {
            ShowLine(current.lines[index]);
            return;
        }

        if (current.choices != null && current.choices.Length > 0)
        {
            ShowChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    // Typewriter

    void ShowLine(string line)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;

        lineText.text = line;
        lineText.maxVisibleCharacters = 0;
        lineText.ForceMeshUpdate();
        int total = lineText.textInfo.characterCount;

        for (int i = 1; i <= total; i++)
        {
            lineText.maxVisibleCharacters = i;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }

        isTyping = false;
    }

    void CompleteLine()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        lineText.maxVisibleCharacters = int.MaxValue;
        isTyping = false;
    }

    // Botones de opciones

    void ShowChoices()
    {
        choosing = true;
        choicesPanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            bool used = i < current.choices.Length;
            choiceButtons[i].gameObject.SetActive(used);
            if (!used) continue;

            choiceButtons[i].GetComponentInChildren<TMP_Text>().text = current.choices[i].text;
            choiceButtons[i].onClick.RemoveAllListeners();

            int chosen = i;
            choiceButtons[i].onClick.AddListener(() => SelectChoice(chosen));
        }
    }

    void SelectChoice(int i)
    {
        ShowNode(current.choices[i].nextNode);
    }

    void EndDialogue()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        isTyping = false;
        panel.SetActive(false);
        choicesPanel.SetActive(false);
        choosing = false;
        IsActive = false;
    }
}