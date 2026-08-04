using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;

    [Header("Characters")]
    [SerializeField] List<CharacterScriptable> characters;
    public CharacterScriptable mainCharacter;
    public CharacterScriptable activeCharacter;

    [Space]
    [HideInInspector] public StoryDatabase storyDatabase;

    [Space]
    [SerializeField] bool debugMessage = true;

    [HideInInspector] public SwitchAnimation exitAnimation;
    public StoryRunner storyRunner;
    UIManager uiMan;

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
        uiMan = UIManager.Instance;
        storyRunner = new StoryRunner();
        storyDatabase = GetComponent<StoryDatabase>();
    }

    public void SetActiveCharacter(string charName)
    {
        if (charName == "null")
        {
            activeCharacter = null;
            return;
        }

        activeCharacter = characters.FirstOrDefault(item => item.name == charName);

        if (activeCharacter == default || activeCharacter == null)
        {
            DebugMessage("Character " + charName + " does not exist");
        }
        else
        {
            DebugMessage("Setting character " + charName);
        }
    }

    public CharacterScriptable GetActiveCharacter()
    {
        return activeCharacter;
    }

    public void StartDialogue(string dialogueName)
    {
        if (storyRunner == null) storyRunner = new StoryRunner();

        IStoryView dialogue = new DialogueView(uiMan.dialogueUI);
        TextAsset story = storyDatabase.GetDialogue(dialogueName);

        storyRunner.StartStory(story, dialogue, dialogueName);
    }

    public void StartDialogue(TextAsset textAsset)
    {
        if (storyRunner == null) storyRunner = new StoryRunner();

        IStoryView dialogue = new DialogueView(uiMan.dialogueUI);
        storyRunner.StartStory(textAsset, dialogue, textAsset.name);
    }

    public void StartDiary(string diaryName)
    {
        if (storyRunner == null) storyRunner = new StoryRunner();

        IStoryView diary = new DiaryView(uiMan.diaryUI);
        storyRunner.StartStory(storyDatabase.GetDiary(diaryName), diary, diaryName);
    }

    public void StartComment(string commentName)
    {
        if (storyRunner == null) storyRunner = new StoryRunner();

        if (uiMan == null) uiMan = UIManager.Instance;

        IStoryView commentView = new CommentView(uiMan.commentUI);
        storyRunner.StartStory(storyDatabase.GetComment(commentName), commentView, commentName);
    }

    public void StartInfotext(string infotextName)
    {

    }

    public void MakeStoryChoice(int index)
    {
        print("Making choice");
        storyRunner.MakeChoice(index);
    }

    public Story GetCurrentStory()
    {
        if (storyRunner != null)
            return storyRunner.currentStory;
        else
            return null;
    }

    void DebugMessage(string message)
    {
        if (debugMessage)
            Debug.Log(message);
    }
}
