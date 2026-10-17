using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private string npcName;
    [SerializeField, TextArea(2, 4)] private string[] lines;

    public void Talk()
    {
        dialogueManager.StartDialogue(npcName, lines);
    }
}