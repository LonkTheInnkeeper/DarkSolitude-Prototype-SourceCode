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
    [SerializeField] string buttonClip;

    GameMenuDialoguesAnimations animations;

    UIManager uiMan;
    SaveLoadManager saveMan;
    GameManager gameMan;

    SettingsData settingsData;

    private void Start()
    {
        uiMan = UIManager.Instance;
        saveMan = SaveLoadManager.Instance;
        gameMan = GameManager.Instance;
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
        AudioManager.Instance.ui.Play(buttonClip);
        animations.SaveFileButton();
    }

    public void LoadBtn()
    {
        AudioManager.Instance.ui.Play(buttonClip);
        animations.LoadFileButton();
    }

    public void OverwriteBtn()
    {
        AudioManager.Instance.ui.Play(buttonClip);
        animations.SaveFileButton();
    }

    public void DeteleBtn()
    {
        AudioManager.Instance.ui.Play(buttonClip);
        animations.DeleteFileButton();
    }

    public void SellectSlot(int index)
    {
        AudioManager.Instance.ui.Play(buttonClip);
        uiMan.saveLoadUI.SellectSlot(index);
    }

    public void LoadGameBtn()
    {
        AudioManager.Instance.ui.Play(buttonClip);
        saveMan.LoadGameData(saveMan.sellectedSaveSlot.gameData);
    }

    public void DeteleFileBtn()
    {
        AudioManager.Instance.ui.Play(buttonClip);
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
        AudioManager.Instance.ui.Play(buttonClip);

        if (increaseIndex)
            gameMan.settingsData.NextTextLang();

        else
            gameMan.settingsData.PreviousTextLang();

        textLang.text = gameMan.settingsData.textLang.ToString();
    }

    public void VoiceLanguageChangeBtn(bool increaseIndex)
    {
        AudioManager.Instance.ui.Play(buttonClip);

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
