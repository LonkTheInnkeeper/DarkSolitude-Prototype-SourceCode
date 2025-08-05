using Ink.Runtime;
using UnityEngine;

public class DialogueEventHandler : MonoBehaviour
{
    [SerializeField] bool debugMessage = true;

    InventoryManager inventoryMan;
    //AudioManager audioMan;
    //UIManager uiMan;
    DialogueManager dialogueMan;
    Story currentStory;

    private void Start()
    {
        inventoryMan = InventoryManager.Instance;
        //audioMan = AudioManager.Instance;
        //uiMan = UIManager.Instance;
        dialogueMan = DialogueManager.Instance;
    }

    public void RegisterBaseFunctions(Story currentStory)
    {
        this.currentStory = currentStory;

        currentStory.BindExternalFunction("AddItem", (string name) => AddItem(name));
        currentStory.BindExternalFunction("CheckItem", (string name) => CheckItem(name));
        currentStory.BindExternalFunction("RemoveItem", (string name) => RemoveItem(name));
        currentStory.BindExternalFunction("PlaySound", (string name) => PlaySound(name));
        currentStory.BindExternalFunction("PlayEvent", (string name) => PlayEvent(name));
        currentStory.BindExternalFunction("CharacterSpeaking", (string name) => CharacterSpeaking(name));
    }

    void AddItem(string name)
    {
        DebugMessage("Adding item " + name);
        inventoryMan.inventory.AddItem(name);
    }

    void CheckItem(string name)
    {
        DebugMessage("Checking item " + name);
        currentStory.variablesState[name] = inventoryMan.inventory.CheckItemInInventory(name);
    }

    public void RemoveItem(string name)
    {
        DebugMessage("Removing item " + name);
        //currentLocation.RemoveItem(name);
    }

    void PlaySound(string name)
    {
        DebugMessage("Playing sound " + name);
        //audioMan.Play(name);
    }

    void PlayEvent(string name)
    {
        DebugMessage("Playing event " + name);
        //currentLocation.Event(name);
    }

    void CharacterSpeaking(string name)
    {
        DebugMessage("Character " + name);

        dialogueMan.SetActiveCharacter(name);
    }

    void DebugMessage(string message)
    {
        if (debugMessage)
            Debug.Log(message);
    }
}
