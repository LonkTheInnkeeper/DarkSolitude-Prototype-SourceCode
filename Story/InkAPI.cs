using Ink.Runtime;

public class InkAPI
{
    GameManager gameMan;

    public InkAPI(Story story)
    {
        gameMan = GameManager.Instance;
        BindToStory(story);
    }

    void BindToStory(Story story)
    {
        story.BindExternalFunction("GetWorldState", (string id) => GetWorldState(id));
        story.BindExternalFunction("SetWorldState", (string id, bool value) => SetWorldState(id, value));
        story.BindExternalFunction("SwitchScene", (string id) => SwitchScene(id));
    }

    bool GetWorldState(string id)
    {
        return gameMan.GetWorldState(id);
    }

    void SetWorldState(string id, bool value)
    {
        gameMan.SetWorldstate(id, value);
    }

    void SwitchScene(string id)
    {
        
    }
}
