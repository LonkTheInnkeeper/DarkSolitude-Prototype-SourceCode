using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<InventoryItemUI> items;
    [SerializeField] RectTransform inventoryPanel;

    GameManager gameMan;

    private void Start()
    {
        gameMan = GameManager.Instance;
        inventoryPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {

            if (gameMan.gameState == GameManager.GameState.Navigation)
            {
                gameMan.SwitchGameState(GameManager.GameState.Inventory);
                inventoryPanel.gameObject.SetActive(true);
            }
            else
            {
                gameMan.SwitchGameState(GameManager.GameState.Navigation);
                inventoryPanel.gameObject.SetActive(false);
            }
        }
    }

    public void LoadInventory()
    {
        InventoryData data = GameManager.Instance.playerData.inventoryData;

        for (int i = 0; i < data.items.Count; i++)
        {
            items[i].SetItem(InventoryManager.Instance.itemDatabase.GetItem(data.items[i]));
        }
    }

    public void CloseInventory()
    {
        if (inventoryPanel.gameObject.activeInHierarchy)
            inventoryPanel.gameObject.SetActive(false);

        if (gameMan.gameState == GameManager.GameState.Inventory)
        {
            gameMan.SwitchGameState(GameManager.GameState.Navigation);
        }
    }

    public void AddItem(ItemScriptable item)
    {
        foreach (var itemUI in items)
        {
            if (itemUI.IsEmpty())
            {
                itemUI.SetItem(item);
                break;
            }
        }
    }
}
