using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceUI : MonoBehaviour, IUITrigger
{
    [SerializeField] TextMeshProUGUI choiceText;
    Button button;
    int choiceIndex;

    AudioManager audioMan;

    private void Start()
    {
        audioMan = AudioManager.Instance;
    }

    public void SetChoice(string choiceText, int index)
    {
        //this.choiceText.fontSize = GameManager.Instance.settingsData.fontSize;

        this.choiceText.text = (index + 1).ToString() + ". " + choiceText;
        this.choiceIndex = index;

        button = GetComponent<Button>();
        button.onClick.AddListener(() => MakeChoice());
    }

    public void MakeChoice()
    {
        audioMan.uiAudioSource.clip = audioMan.diaogueSellect;
        audioMan.uiAudioSource.Play();
        StoryManager.Instance.MakeStoryChoice(choiceIndex);
    }

    public void CursorOnUI(bool trigger)
    {
        if (trigger)
        {
            audioMan.uiAudioSource2.clip = audioMan.diaogueHover;
            audioMan.uiAudioSource2.Play();

            GetComponent<Animator>().SetTrigger("Sellect");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Desellect");
        }
    }
}
