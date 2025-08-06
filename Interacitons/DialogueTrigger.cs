using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset textAsset;
    [SerializeField] bool diary;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.dialogue.EnterDialogueMode(textAsset.name, diary);
    }
}
