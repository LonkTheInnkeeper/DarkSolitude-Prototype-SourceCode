using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;
    [SerializeField] Slider voiceVolume;

    [Space]
    [SerializeField] TextMeshProUGUI textLang;
    [SerializeField] TextMeshProUGUI voiceLang;

    [Space]
    [SerializeField] AudioClip buttonClip;

    GameMenuDialoguesAnimations animations;

    UIManager uiMan;
    SaveLoadManager saveMan;
    GameManager gameMan;
    AudioManager audioMan;

    SettingsData settingsData;

    private void Start()
    {
        uiMan = UIManager.Instance;
        saveMan = SaveLoadManager.Instance;
        gameMan = GameManager.Instance;
        audioMan = AudioManager.Instance;

        animations = GetComponent<GameMenuDialoguesAnimations>();
    }

    private void Update()
    {
        if (musicVolume.gameObject.activeInHierarchy)
        {
            MusicVolumeUpdate(musicVolume.value);
            SfxVolumeUpdate(sfxVolume.value);
            VoiceVolumeUpdate(voiceVolume.value);
        }
    }


    //
    // --- Back dialogue ---
    //
    public void OpenBackDialogue()
    {
        animations.BackButton();
    }

    public void MainMenuTransferBtn()
    {
        gameMan.SwitchScene(0);
    }

    public void CloseMenuBtn()
    {
        if (animations == null)
        {
            animations = GetComponent<GameMenuDialoguesAnimations>();
        }

        animations.CloseMenu();
    }


    //
    // --- Save/Load dialogue ---
    //
    public void OpenSaveLoadDialogue()
    {
        animations.SaveLoadButton();
    }

    public void SaveBtn()
    {
        audioMan.PlayUI(buttonClip);
        animations.SaveFileButton();
    }

    public void LoadBtn()
    {
        audioMan.PlayUI(buttonClip);
        animations.LoadFileButton();
    }

    public void OverwriteBtn()
    {
        audioMan.PlayUI(buttonClip);
        animations.SaveFileButton();
    }

    public void DeteleBtn()
    {
        audioMan.PlayUI(buttonClip);
        animations.DeleteFileButton();
    }

    public void SellectSlot(int index)
    {
        audioMan.PlayUI(buttonClip);
        uiMan.saveLoadUI.SellectSlot(index);
    }

    public void LoadGameBtn()
    {
        audioMan.PlayUI(buttonClip);
        saveMan.LoadGameData(saveMan.sellectedSaveSlot.gameData);
    }

    public void DeteleFileBtn()
    {
        audioMan.PlayUI(buttonClip);
        saveMan.DeteleFile();
    }


    //
    // --- Settings dialogue ---
    //
    public void OpenSettingsDialogue()
    {
        animations.SettingsButton();

        settingsData = gameMan.settingsData;

        textLang.text = settingsData.textLang.ToString();
        voiceLang.text = settingsData.voiceLang.ToString();

        musicVolume.value = settingsData.music;
        voiceVolume.value = settingsData.voice;
        sfxVolume.value = settingsData.effects;
    }

    public void TextlanguageChangeBtn(bool increaseIndex)
    {
        audioMan.PlayUI(buttonClip);

        if (increaseIndex)
            gameMan.settingsData.NextTextLang();

        else
            gameMan.settingsData.PreviousTextLang();

        textLang.text = gameMan.settingsData.textLang.ToString();
    }

    public void VoiceLanguageChangeBtn(bool increaseIndex)
    {
        audioMan.PlayUI(buttonClip);

        if (increaseIndex)
            gameMan.settingsData.NextVoiceLang();

        else
            gameMan.settingsData.PreviousVoiceLang();

        voiceLang.text = gameMan.settingsData.voiceLang.ToString();
    }

    private void MusicVolumeUpdate(float volume)
    {
        settingsData.music = musicVolume.value;
    }

    private void SfxVolumeUpdate(float volume)
    {
        settingsData.effects = sfxVolume.value;
    }

    private void VoiceVolumeUpdate(float volume)
    {
        settingsData.voice = voiceVolume.value;
    }
}
