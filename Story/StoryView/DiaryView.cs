using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class DiaryView : IStoryView
{
    DiaryUI ui;
    StoryRunner.StoryType storyType;

    string text;
    StoryTagData tagData;

    public DiaryView(DiaryUI ui)
    {
        this.ui = ui;
        storyType = StoryRunner.StoryType.Diary;
    }

    public void Open() => ui.OpenDiary();

    public void Close() => ui.CloseDiary();

    public void DisplayText(string text, string choice, StoryTagData tags)
    {
        this.text += text;
        tagData = tags;

        if (tags.header != null)
        {
            ui.SetRunningText(tags.header);
        }
    }

    public void DisplayChoices(List<Choice> choices)
    {
        if (choices.Count != 0)
        {
            ui.PrintDiary(text, string.Empty, tagData);
            ui.PrintChoices(choices);
            text = string.Empty;
        }
    }

    void DisplayChoices(List<string> choices)
    {
        ui.PrintDiary(text, string.Empty, tagData);
        ui.PrintChoices(choices);
        text = string.Empty;
    }

    public Story ProcessStory(Story story) => story;

    public void PrintStoryBlock(StoryBlock block)
    {
        DisplayText(block.text, string.Empty, block.tagData);

        if (block.choiceList.Count > 0)
        {
            DisplayChoices(block.choiceList);
        }
        else
        {
            text += "\n";
            StoryManager.Instance.storyRunner.ContinueStory();
        }
    }

    StoryRunner.StoryType IStoryView.GetStoryType() => storyType;
}
