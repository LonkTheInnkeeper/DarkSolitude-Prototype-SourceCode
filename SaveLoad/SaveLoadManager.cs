using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    GameManager gameMan;
    InventoryManager inventoryMan;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        gameMan = GameManager.Instance;
        inventoryMan = InventoryManager.Instance;
    }

    private void Update()
    {
        if (InputManager.Instance.SaveGame())
        {
            SavePlayerData(gameMan.playerData);
        }
        if (InputManager.Instance.LoadGame())
        {
            LoadPlayerData();
        }
    }

    public void SavePlayerData(PlayerData playerData)
    {
        inventoryMan.inventory.SaveInventory();
        gameMan.player.GetComponent<Player>().SavePosition();

        SaveLoadSystem.SavePlayerData(playerData);
    }

    public void SaveSettings(SettingsData settingsData)
    {
        SaveLoadSystem.SaveSettingsData(settingsData);
    }

    public void LoadPlayerData()
    {
        PlayerData playerData = new PlayerData();

        if (SaveLoadSystem.CheckPlayerData())
        {
            playerData = SaveLoadSystem.LoadPlayerData();
        }
        else
        {
            SaveLoadSystem.SavePlayerData(playerData);
        }

        gameMan.playerData = playerData;
        gameMan.player.GetComponent<Player>().LoadPosition();
        inventoryMan.inventory.LoadInventory();
    }

    public SettingsData LoadSettings()
    {
        SettingsData settingsData = new SettingsData();

        if (SaveLoadSystem.CheckSettingsData())
        {
            settingsData = SaveLoadSystem.LoadSettingsData();
        }
        else
        {
            SaveLoadSystem.SaveSettingsData(settingsData);
        }

        return settingsData;
    }
}
