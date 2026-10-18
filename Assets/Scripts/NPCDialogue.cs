using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public int nextNode = -1;
}

[System.Serializable]
public class DialogueNode
{
    [TextArea(2, 4)] public string[] lines;
    public DialogueChoice[] choices;
}

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private string npcName;
    [SerializeField] private DialogueNode[] nodes;

    public void Talk()
    {
        dialogueManager.StartDialogue(npcName, nodes);
    }
}