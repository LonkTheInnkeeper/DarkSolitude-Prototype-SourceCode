using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject itemGraphics;
    [SerializeField] ItemScriptable item;
    [SerializeField] bool hideItem;
    [Space]
    [SerializeField] AudioSource audioSource;

    public void PickUpItem()
    {
        InventoryManager inventoryMan = InventoryManager.Instance;
        AudioManager audioMan = AudioManager.Instance;

        //if (inventoryMan.activeItem != null)
        //{
        //    inventoryMan.inventory.ReturnItem();
        //    return;
        //}

        inventoryMan.inventory.AddItem(item);

        audioSource.clip = audioMan.itemPickups[Random.Range(0, audioMan.itemPickups.Count)];
        audioSource.Play();

        if (hideItem) itemGraphics.SetActive(false);
    }
}
