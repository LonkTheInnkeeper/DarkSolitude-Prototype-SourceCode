using UnityEngine;

public class Item : MonoBehaviour, ISavable
{
    [SerializeField] string id;
    [SerializeField] GameObject itemGraphics;
    [SerializeField] ItemScriptable item;
    [SerializeField] bool hideItem;

    public void ApplyState()
    {
        Interaction interaction = gameObject.GetComponent<Interaction>();

        if (!GameManager.Instance.GetWorldState(id))
        {
            interaction.eventAvailable = true;
            itemGraphics.SetActive(true);
        }

        else
        {
            interaction.eventAvailable = false;
            if (hideItem) itemGraphics.SetActive(false);
        }
    }

    public void PickUpItem()
    {
        InventoryManager inventoryMan = InventoryManager.Instance;
        AudioManager audioMan = AudioManager.Instance;

        inventoryMan.inventory.TryAddItem(item.itemName);

        GameManager.Instance.player.GetComponent<Animator>().SetTrigger("Gathering");

        GetComponent<AudioEmitter>().Play();

        if (hideItem)
        {
            GameManager.Instance.gameData.areaData.worldStates.Add(id, true);
            itemGraphics.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnLocationLoad += ApplyState;
    }
}
