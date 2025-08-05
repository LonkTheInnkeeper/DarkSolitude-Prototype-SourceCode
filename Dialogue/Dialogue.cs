using Ink.Runtime;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    Story currentStory;

    GameManager gameMan;
    DialogueManager dialogueMan;
    UIManager uiMan;

    string currentChoice = string.Empty;
    bool debugMessage = true;

    private void Start()
    {
        gameMan = GameManager.Instance;
        dialogueMan = DialogueManager.Instance;
        uiMan = UIManager.Instance;
    }

    public void EnterDialogueMode(string dialogueName)
    {
        TextAsset textFile = dialogueMan.database.GetDialogue(dialogueName);

        if (textFile == null)
        {
            DebugMessage("Dialogue file " + dialogueName + " does not exist.");
            return;
        }

        gameMan.SwitchGameState(GameManager.GameState.Dialogue);

        uiMan.dialogueUI.NewDialogue();

        currentStory = new Story(textFile.text);
        dialogueMan.events.RegisterBaseFunctions(currentStory);

        ContinueStory();

        DebugMessage("Dialogue starts with " + textFile.name);
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string text = currentStory.Continue();

            uiMan.dialogueUI.PrintDialogueText(text, currentChoice);
            uiMan.dialogueUI.PrintChoices(currentStory.currentChoices);
        }

        else
        {
            ExitDialogue();
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        DebugMessage("Choice index " + choiceIndex);

        if (currentStory.currentChoices.Count == 0 && choiceIndex == 0)
        {
            currentChoice = string.Empty;
            ContinueStory();
            return;
        }

        if (currentStory.currentChoices.Count <= choiceIndex) return;

        currentChoice = currentStory.currentChoices[choiceIndex].text;
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    private void ExitDialogue()
    {
        currentStory = null;
        currentChoice = string.Empty;
        uiMan.dialogueUI.ExitDialogue();
        gameMan.SwitchGameState(GameManager.GameState.Navigation);
    }

    void DebugMessage(string message)
    {
        if (debugMessage)
            Debug.Log(message);
    }
}
