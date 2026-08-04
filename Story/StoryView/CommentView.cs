using System.Collections.Generic;
using Ink.Runtime;

public class CommentView : IStoryView
{
    CommentUI ui;

    public CommentView(CommentUI ui)
    {
        this.ui = ui;
    }

    public void Open() => ui.OpenComment();

    public void Close() => ui.CloseComment();

    public void DisplayText(string text, string choice, StoryTagData tags)
        => ui.PrintCommentText(text, choice, tags);

    public void DisplayChoices(List<Choice> choices) { }

    public Story ProcessStory(Story story) => story;

    public void PrintStoryBlock(StoryBlock block)
    {
        throw new System.NotImplementedException();
    }
}
