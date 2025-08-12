using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera activeCamera;
    public GameState gameState;
    public GameObject player;
    public MouseControl mouseControl;

    public PlayerData playerData;
    public SettingsData settingsData;

    [SerializeField] TextMeshProUGUI gameStateDebug;

    public enum GameState
    {
        Navigation,
        Dialogue,
        Inventory,
        ItemHandling,
        Menu,
        StoryEvent,
        Debug
    }

    private void Awake()
    {
        Instance = this;

        playerData = new PlayerData();
        settingsData = new SettingsData();
    }

    private void Start()
    {
        activeCamera = Camera.main;
        settingsData = SaveLoadManager.Instance.LoadSettings();
    }

    private void Update()
    {
        gameStateDebug.text = gameState.ToString();
    }

    public void SwitchGameState(GameState state)
    {
        gameState = state;
    }
}
