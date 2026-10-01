using System.Collections.Generic;
using Ink.Runtime;

public class DialogueView : IStoryView
{
    DialogueUI ui;
    StoryRunner.StoryType storyType;

    public DialogueView(DialogueUI ui)
    {
        this.ui = ui;
        storyType = StoryRunner.StoryType.Dialogue;
    }

    public void Open() => ui.OpenDialogue();

    public void Close() => ui.CloseDialogue();

    public void PrintStoryBlock(StoryBlock block) 
        => ui.PrintStoryBlock(block);

    public void DisplayText(string text, string choice, StoryTagData tags)
        => ui.PrintDialogueText(text, choice, tags);

    public void DisplayChoices(List<Choice> choices)
        => ui.PrintChoices(choices);

    public Story ProcessStory(Story story) => story;

    StoryRunner.StoryType IStoryView.GetStoryType() => storyType;
}
