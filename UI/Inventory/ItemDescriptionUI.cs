using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemDescriptionUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;

    [Space]
    [SerializeField] string itemSwitch;

    bool desctriprionOpen = false;

    AudioManager audioMan;

    private void Start()
    {
        audioMan = AudioManager.Instance;
    }

    public void ToggleDescription(ItemScriptable item)
    {
        UITextData data = UIManager.Instance.textData;

        if (item == null)
        {
            CloseDescription();
            return;
        }

        if (GameManager.Instance.GetGameState() != GameManager.GameState.ItemHandling)
            AudioManager.Instance.ui.Play(itemSwitch);

        if (!desctriprionOpen)
        {
            animator.SetTrigger("Open");
            desctriprionOpen = true;

            //audioMan.PlayUI(audioMan.database.GetInventoryClip("ItemDescriptionUp"));
        }

        if (data.uiTexts.ContainsKey("item_" + item.itemName))
            itemName.text = data.uiTexts["item_" + item.itemName];

        if (data.uiTexts.ContainsKey("description_" + item.itemName))
            itemDescription.text = data.uiTexts["description_" + item.itemName];
    }

    public void CloseDescription()
    {
        if (desctriprionOpen)
        {
            animator.SetTrigger("Close");

            //audioMan.PlayUI(audioMan.database.GetInventoryClip("ItemDescriptionDown"));
        }

        desctriprionOpen = false;
    }
}
