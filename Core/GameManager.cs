using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CameraControl cameraControl;
    public Camera activeCamera;
    public GameObject player;
    public MouseControl mouseControl;

    private GameState gameState;
    private GameState previousGameState;
    public PlayerState playerState;

    public GameData gameData;
    public SettingsData settingsData;

    [HideInInspector] public bool closeupState;

    [SerializeField] ScreenShade screenShade;

    [Space]
    [SerializeField] TextMeshProUGUI gameStateDebug;
    [SerializeField] TextMeshProUGUI localDebug;
    [SerializeField] TextMeshProUGUI playerStateDebug;

    public enum GameState
    {
        Navigation,
        Dialogue,
        Inventory,
        ItemHandling,
        Menu,
        StoryEvent,
        Closeup,
        Debug
    }

    public enum PlayerState
    {
        Idle,
        Walking,
        Running
    }

    private void Awake()
    {
        Instance = this;

        gameData = new GameData();
        settingsData = SaveLoadSystem.LoadSettingsData();

        localDebug.text = settingsData.textLang.ToString();

        if (settingsData == null)
            settingsData = new SettingsData();
    }

    private void Start()
    {
        activeCamera = Camera.main;
        GameEvents.OnLocationLoad.Invoke();
    }

    private void Update()
    {
        gameStateDebug.text = $"Game State: {gameState}";
    }

    public void SetGameState(GameState state)
    {
        print("Setting state: " + state.ToString());
        previousGameState = gameState;
        gameState = state;
        GameEvents.OnGameStateChange?.Invoke();
    }

    public void SetGameState(string state)
    {
        print("Setting state");
        previousGameState = gameState;
        gameState = SystemTools.ParseEnum<GameState>(state);
        GameEvents.OnGameStateChange?.Invoke();
    }

    public GameState GetGameState() { return gameState; }

    public GameState GetPreviousGamerState() { return previousGameState; }

    public bool GetWorldState(string key)
    {
        if (gameData.areaData.CheckKey(key) && gameData.areaData.worldStates[key] == true)
        {
            return true;
        }

        return false;
    }

    public void SetWorldstate(string key, bool state)
    {
        if (key == string.Empty) return;

        print($"Adding key: {key}, {state}");

        if (gameData.areaData.worldStates.ContainsKey(key))
        {
            gameData.areaData.worldStates[key] = state;
        }
        else
        {
            gameData.areaData.worldStates.Add(key, state);
        }
    }

    public void SwitchScene(int index)
    {
        screenShade.ShadeOnAction(() =>
        {
            SceneManager.LoadScene(index);
        });
    }
}
