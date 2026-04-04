using UnityEngine;

public class Item : MonoBehaviour, ISavable
{
    [SerializeField] string id;
    [SerializeField] GameObject itemGraphics;
    [SerializeField] ItemScriptable item;
    [SerializeField] bool hideItem;
    [Space]
    [SerializeField] AudioSource audioSource;

    public void ApplyState()
    {
        Interaction interaction = gameObject.GetComponent<Interaction>();

        if (!GameManager.Instance.CheckWorldState(id))
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

        if (audioSource != null)
        {
            audioSource.clip = audioMan.itemPickups[Random.Range(0, audioMan.itemPickups.Count)];
            audioSource.Play();
        }

        if (hideItem) itemGraphics.SetActive(false);

        GameManager.Instance.gameData.areaData.worldStates.Add(id, true);
    }

    private void OnEnable()
    {
        ApplyState();
    }
}
