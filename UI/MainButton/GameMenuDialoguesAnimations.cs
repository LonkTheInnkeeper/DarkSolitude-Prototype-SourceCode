using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuDialoguesAnimations : MonoBehaviour
{
    [SerializeField] float animationSpeed;
    [SerializeField] float scaleXModifier;

    [Space]
    [SerializeField] RectTransform backRect;
    [SerializeField] RectTransform saveLoadRect;
    [SerializeField] RectTransform settingsRect;
    [SerializeField] RectTransform saveFileRect;
    [SerializeField] RectTransform loadFileRect;
    [SerializeField] RectTransform deleteFileRect;

    MenuDialogueType currentDialogue;

    AudioManager audioMan;

    public enum MenuDialogueType
    {
        Back,
        SaveLoad,
        Settings,
        SaveFile,
        LoadFile,
        DeleteFile,
        Default
    }

    private void Start()
    {
        audioMan = AudioManager.Instance;

        currentDialogue = MenuDialogueType.Default;
    }

    public void BackButton()
    {
        StopAllCoroutines();

        if (currentDialogue == MenuDialogueType.Back) return;

        CloseDialogue();

        currentDialogue = MenuDialogueType.Back;
        StartCoroutine(OpenRoutine(backRect));
    }

    public void SaveLoadButton()
    {
        StopAllCoroutines();

        if (currentDialogue == MenuDialogueType.SaveLoad) return;

        CloseDialogue();

        currentDialogue = MenuDialogueType.SaveLoad;
        StartCoroutine(OpenRoutine(saveLoadRect));
    }

    public void SettingsButton()
    {
        StopAllCoroutines();

        if (currentDialogue == MenuDialogueType.Settings) return;

        CloseDialogue();

        currentDialogue = MenuDialogueType.Settings;
        StartCoroutine(OpenRoutine(settingsRect));
    }

    public void SaveFileButton()
    {
        StopAllCoroutines();

        if (currentDialogue == MenuDialogueType.SaveFile) return;

        CloseDialogue();

        currentDialogue = MenuDialogueType.SaveFile;
        StartCoroutine(OpenRoutine(saveFileRect));
    }

    public void LoadFileButton()
    {
        StopAllCoroutines();

        if (currentDialogue == MenuDialogueType.LoadFile) return;

        CloseDialogue();

        currentDialogue = MenuDialogueType.LoadFile;
        StartCoroutine(OpenRoutine(loadFileRect));
    }

    public void DeleteFileButton()
    {
        StopAllCoroutines();

        if (currentDialogue == MenuDialogueType.DeleteFile) return;

        CloseDialogue();

        currentDialogue = MenuDialogueType.DeleteFile;
        StartCoroutine(OpenRoutine(deleteFileRect));
    }

    public void CloseDialogue()
    {
        switch (currentDialogue)
        {
            case MenuDialogueType.Back:
                StartCoroutine(CloseRoutine(backRect)); break;

            case MenuDialogueType.SaveLoad:
                StartCoroutine(CloseRoutine(saveLoadRect)); break;

            case MenuDialogueType.Settings:
                StartCoroutine(CloseRoutine(settingsRect));
                SaveLoadSystem.SaveSettingsData(GameManager.Instance.settingsData);
                break;

            case MenuDialogueType.SaveFile:
                StartCoroutine(CloseRoutine(saveFileRect)); break;

            case MenuDialogueType.LoadFile:
                StartCoroutine(CloseRoutine(loadFileRect)); break;

            case MenuDialogueType.DeleteFile:
                StartCoroutine(CloseRoutine(deleteFileRect)); break;

            case MenuDialogueType.Default:
                break;

            default:
                break;
        }
    }

    public void SetDefault()
    {
        currentDialogue = MenuDialogueType.Default;
    }

    IEnumerator OpenRoutine(RectTransform rect)
    {
        audioMan.PlayUI(audioMan.database.GetInventoryClip("ItemSellect"));

        Image mask = rect.GetComponent<Image>();

        rect.gameObject.SetActive(true);
        rect.localScale = new Vector3(scaleXModifier, 1, 1);
        mask.color = new Color(1, 1, 1, 0);

        float time = 0f;

        while (time < animationSpeed)
        {
            time += Time.deltaTime;

            // Rect
            float x = Mathf.Lerp(rect.localScale.x, 1, time / animationSpeed);
            rect.localScale = new Vector3(x, 1, 1);

            // Mask
            float a = Mathf.Lerp(mask.color.a, 1, time / animationSpeed);
            mask.color = new Color(1, 1, 1, a);

            yield return null;
        }

        rect.localScale = Vector3.one;
        mask.color = new Color(1, 1, 1, 1);
    }

    IEnumerator CloseRoutine(RectTransform rect)
    {
        Image mask = rect.GetComponent<Image>();

        rect.localScale = new Vector3(1, 1, 1);
        mask.color = new Color(1, 1, 1, 1);

        float time = 0f;

        while (time < animationSpeed)
        {
            time += Time.deltaTime;

            // Rect
            float x = Mathf.Lerp(rect.localScale.x, scaleXModifier, time / animationSpeed);
            rect.localScale = new Vector3(x, 1, 1);

            // Mask
            float a = Mathf.Lerp(mask.color.a, 0, time / animationSpeed);
            mask.color = new Color(1, 1, 1, a);

            yield return null;
        }

        rect.localScale = new Vector3(scaleXModifier, 1, 1);
        mask.color = new Color(1, 1, 1, 0);
        rect.gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        audioMan.PlayUI(audioMan.database.GetInventoryClip("InventoryDown"));

        GetComponent<Animator>().SetTrigger("Close");
        GetComponent<UICursorDistanceTrigger>().ResetState();

        CloseDialogue();
        SetDefault();

        GameManager.Instance.SetGameState(GameManager.GameState.Navigation);
    }

    public void OpenMenu()
    {
        audioMan.PlayUI(audioMan.database.GetInventoryClip("InventoryUp"));

        GetComponent<Animator>().SetTrigger("Open");

        GameManager.Instance.SetGameState(GameManager.GameState.Menu);
    }
}
