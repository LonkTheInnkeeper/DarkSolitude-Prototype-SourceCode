using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEditor.PackageManager;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] List<CharacterScriptable> characters;
    public CharacterScriptable mainCharacter;
    public CharacterScriptable activeCharacter;
    [SerializeField] bool debugMessage = true;
    [SerializeField] string debugDialogue;

    public Dialogue dialogue;
    public DialogueDatabase database;
    public DialogueEventHandler events;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        dialogue = GetComponent<Dialogue>();
        database = GetComponent<DialogueDatabase>();
        events = GetComponent<DialogueEventHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.SwitchGameState(GameManager.GameState.Dialogue);
            dialogue.EnterDialogueMode(debugDialogue);
        }
    }

    public void SetActiveCharacter(string charName)
    {
        DebugMessage("Setting character " + charName);
        if (charName == "null")
        {
            activeCharacter = null;
            return;
        }

        activeCharacter = characters.FirstOrDefault(item => item.GetName() == charName);

        if (activeCharacter == default || activeCharacter == null)
        {
            DebugMessage("Character " + charName + " does not exist");
        }
    }

    void DebugMessage(string message)
    {
        if (debugMessage)
            Debug.Log(message);
    }
}
