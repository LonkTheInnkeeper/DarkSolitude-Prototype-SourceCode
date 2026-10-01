using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

[Serializable]
public class StoryRunner
{
    public Story currentStory;
    StoryTagData currentTags;
    StoryData storyData;

    GameManager gameMan;
    StoryManager storyMan;
    AudioManager audioMan;

    IStoryView currentView;
    string currentChoice = "";
    string currentSpeaker = "";
    public string storyName;

    List<string> storyActions;

    InkAPI inkAPI;

    public StoryRunner()
    {
        gameMan = GameManager.Instance;
        storyMan = StoryManager.Instance;
        audioMan = AudioManager.Instance;
    }

    public enum StoryType
    {
        Dialogue,
        Comment,
        Diary
    }

    public void StartStory(TextAsset inkFile, IStoryView storyView, string storyName)
    {
        if (inkFile == null)
        {
            Debug.LogWarning("Ink file is missing");
            return;
        }

        if (storyView.GetStoryType() == StoryType.Dialogue)
            storyData = SaveLoadSystem.LoadStoryData(storyName, gameMan.settingsData.textLang.ToString());

        else if (storyView.GetStoryType() == StoryType.Comment)
            storyData = SaveLoadSystem.LoadCommentsData(storyName, gameMan.settingsData.textLang.ToString());

        else if (storyView.GetStoryType() == StoryType.Diary)
            storyData = SaveLoadSystem.LoadDiaryData(storyName, gameMan.settingsData.textLang.ToString());

        gameMan.player.GetComponent<Movement>().ForceStop();

        storyActions = new List<string>();

        currentView = storyView;
        gameMan.SetGameState(GameManager.GameState.Dialogue);
        currentView.Open();

        currentSpeaker = string.Empty;
        currentChoice = string.Empty;

        currentStory = new Story(inkFile.text);
        inkAPI = new InkAPI(currentStory);
        this.storyName = storyName;

        GameEvents.OnDialogueStart?.Invoke();

        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory != null && currentStory.canContinue)
        {
            string text = currentStory.Continue();
            ProcessTags();

            currentStory = currentView.ProcessStory(currentStory);

            currentView.PrintStoryBlock(GetStoryBlock());
            PlayVoice(currentStory.currentText.TrimEnd());
        }
        else
        {
            StopBackground();
            ExitStory();
        }
    }

    StoryBlock GetStoryBlock()
    {
        StoryBlock block = new StoryBlock();

        if (storyData == null) Debug.Log("StoryData null");
        if (currentStory == null) Debug.Log("CurrentStory null");

        if (storyData.textDictionary.TryGetValue(currentStory.currentText.TrimEnd(), out string text))
        {
            block.text = text;
        }
        else
        {
            block.text = $"Missing text {currentStory.currentText} in {storyName}/{gameMan.settingsData.textLang.ToString()}";
        }

        foreach (var choice in currentStory.currentChoices)
        {
            if (storyData.choiceDictionary.TryGetValue(choice.text, out string choiceValue))
            {
                block.choiceList.Add(choiceValue);
            }
            else
            {
                block.choiceList.Add($"Missing choice {choice.text} in {storyName}/{gameMan.settingsData.textLang.ToString()}");
            }
        }

        block.lastChoice = currentChoice;
        block.tagData = currentTags;

        return block;
    }

    void ProcessTags()
    {
        currentTags = StoryTagParser.Parse(currentStory);

        if (currentTags.speaker != null)
        {
            Debug.Log($"Speaking: {currentTags.speaker}");
            storyMan.SetActiveCharacter(currentTags.speaker);
            currentSpeaker = currentTags.speaker;
        }

        if (currentTags.line != null)
        {
            //PlayVoice();
        }

        if (currentTags.action != null)
        {
            storyActions.Add(currentTags.action);
            //storyMan.GetComponent<StoryActions>().PlayAction(currentTags.action);
            //Debug.Log("Story action: " + currentTags.action);
        }

        if (currentTags.background == "stop")
        {
            StopBackground();
        }
        else if (currentTags.background != null)
        {
            PlayBackground();
        }
    }

    public void ExitStory()
    {
        Resources.UnloadUnusedAssets();

        currentView.Close();

        currentStory = null;
        currentChoice = "";

        Debug.Log("Story events: " + storyActions.Count);

        if (storyActions.Count != 0)
            LocationManager.Instance.GetActiveILocation().TriggerStoryActions(storyActions);

        gameMan.SetGameState(GameManager.GameState.Navigation);

        GameEvents.OnDialogueEnd?.Invoke();
    }

    public void MakeChoice(int index)
    {
        Debug.Log($"Making choice {index}");

        if (currentStory.currentChoices.Count == 0)
        {
            currentChoice = string.Empty;
            ContinueStory();
            return;
        }

        else if (currentStory.currentChoices.Count <= index)
        {
            Debug.LogWarning("Story choice index is out of range");
            return;
        }

        currentChoice = storyData.choiceDictionary[currentStory.currentChoices[index].text];
        currentStory.ChooseChoiceIndex(index);

        ContinueStory();
    }

    public void PlayVoice(string text)
    {
        string line = currentTags.line;
        AudioClip clip = null;

        if (currentView is DialogueView)
        {
            clip = storyMan.storyDatabase.GetDialogueVoice(storyName, text);
        }

        else if (currentView is CommentView)
        {
            clip = storyMan.storyDatabase.GetCommentVoice(storyName, text);
        }

        audioMan.voice.PlayVoice(clip);
    }

    void PlayBackground()
    {
        audioMan.voice.PlayBackground(currentTags.background);
    }

    void StopBackground()
    {
        audioMan.voice.StopBackground();
    }
}

public class StoryBlock
{
    public string text;
    public List<string> choiceList = new List<string>();
    public string lastChoice;
    public StoryTagData tagData;
}
