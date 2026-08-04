using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class DiaryView : IStoryView
{
    DiaryUI ui;

    string text;
    StoryTagData tagData;

    public DiaryView(DiaryUI ui)
    {
        this.ui = ui;
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
        else
        {
            text += "\n";
            StoryManager.Instance.MakeStoryChoice(0);
        }
    }

    public Story ProcessStory(Story story) => story;

    public void PrintStoryBlock(StoryBlock block)
    {
        throw new System.NotImplementedException();
    }
}
