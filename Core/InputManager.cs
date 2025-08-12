using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool SaveGame()
    {
        return Input.GetKeyDown(KeyCode.S);
    }

    public bool LoadGame()
    {
        return Input.GetKeyDown(KeyCode.L);
    }

    public bool OpenMenu()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
