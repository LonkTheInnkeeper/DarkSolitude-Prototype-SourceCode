
using System.Collections.Generic;

public interface ILocation
{
    public string GetID();
    public void ApplyState();
    public void TriggerStoryActions(List<string> actions);
}
