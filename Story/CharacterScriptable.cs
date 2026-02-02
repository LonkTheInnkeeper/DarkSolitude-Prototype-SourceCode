using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptables/Character")]
public class CharacterScriptable : ScriptableObject
{
    public string firstName;
    public string lastName;
    public Sprite portrait;

    public string GetName()
    {
        return firstName + " " + lastName;
    }
}
