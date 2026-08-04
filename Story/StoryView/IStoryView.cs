using System.Collections.Generic;
using Ink.Runtime;

public interface IStoryView
{
    void Open();
    void Close();
    void PrintStoryBlock(StoryBlock block);
    void DisplayText(string text, string choice, StoryTagData tags);
    void DisplayChoices(List<Choice> choices);
    Story ProcessStory(Story story);
}
