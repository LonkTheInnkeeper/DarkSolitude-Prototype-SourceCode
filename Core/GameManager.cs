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

    [HideInInspector] public bool closeupState;

    [SerializeField] TextMeshProUGUI gameStateDebug;
    [SerializeField] TextMeshProUGUI localDebug;

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
        gameStateDebug.text = "Game state: " + gameState.ToString();

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetString("Localisation", "cz");
            DialogueManager.Instance.infoTextDatabase.SwitchLocalisation();
        }
        else if (Input.GetKeyDown(KeyCode.E)) 
        {
            PlayerPrefs.SetString("Localisation", "en");
            DialogueManager.Instance.infoTextDatabase.SwitchLocalisation();
        }

        localDebug.text = "Localisation: " + PlayerPrefs.GetString("Localisation");
    }

    public void SwitchGameState(GameState state)
    {
        gameState = state;
    }

    public void CloseupState(bool state)
    {
        closeupState = state;
    }
}
