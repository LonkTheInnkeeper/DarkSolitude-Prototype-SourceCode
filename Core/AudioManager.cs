using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public MusicPlayer music;
    public VoicePlayer voice;
    public SFXPlayer sfx;
    public UIPlayer ui;

    [SerializeField] Transform sourceParent;

    [SerializeField] AudioDatabase voiceBackgroundDatabase;
    [SerializeField] AudioDatabase uiDatabase;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        music = new MusicPlayer(sourceParent);
        voice = new VoicePlayer(sourceParent, voiceBackgroundDatabase);
        sfx = new SFXPlayer(sourceParent);
        ui = new UIPlayer(sourceParent, uiDatabase);
    }

    public void StopAllSounds()
    {
        sfx.StopAll();
        ui.StopAll();
    }

    public void PlayUI(string id)
    {
        ui.Play(id);
    }

    //private void OnEnable()
    //{
    //    GameEvents.OnLocationLoad += StopAllSounds;
    //}

    //private void OnDisable()
    //{
    //    GameEvents.OnLocationLoad -= StopAllSounds;
    //}
}
