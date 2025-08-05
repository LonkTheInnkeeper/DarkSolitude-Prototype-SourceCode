using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera activeCamera;
    public GameState gameState;
    public GameObject player;
    public PlayerData playerData;

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
    }

    private void Start()
    {
        activeCamera = Camera.main;
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
