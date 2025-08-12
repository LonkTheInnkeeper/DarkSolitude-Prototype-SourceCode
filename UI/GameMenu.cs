using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject gameMenuPanel;

    [Space]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject saveLoad;

    [Space]
    [SerializeField] Slider effectsSlider;
    [SerializeField] Slider voiceSlider;

    [Space]
    [SerializeField] TextMeshProUGUI fontSize;

    SettingsData settingsData;

    bool menuOpen = false;

    private void Start()
    {
        gameMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (menuOpen)
                CloseMenu();
            else
                OpenMenu();
        }

        UpdateSettings();
    }

    void OpenMenu()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Navigation) return;
        settingsData = GameManager.Instance.settingsData;

        gameMenuPanel.SetActive(true);
        menu.SetActive(true);
        settings.SetActive(false);
        saveLoad.SetActive(false);

        menuOpen = true;

        effectsSlider.value = settingsData.effects;
        voiceSlider.value = settingsData.voice;

        GameManager.Instance.SwitchGameState(GameManager.GameState.Menu);
    }

    public void CloseMenu()
    {
        settings.SetActive(false);
        saveLoad.SetActive(false);
        menu.SetActive(true);
        gameMenuPanel.SetActive(false);

        menuOpen = false;

        SaveLoadManager.Instance.SaveSettings(settingsData);
        GameManager.Instance.SwitchGameState(GameManager.GameState.Navigation);
    }

    void UpdateSettings()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Menu) return;

        fontSize.text = settingsData.fontSize.ToString();

        settingsData.effects = effectsSlider.value;
        settingsData.voice = voiceSlider.value;
    }

    public void IncreaseFont(bool increase)
    {
        if (increase)
            settingsData.fontSize += 2;
        else
            settingsData.fontSize -= 2;
    }
}
