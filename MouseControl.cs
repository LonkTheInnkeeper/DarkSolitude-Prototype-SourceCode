using UnityEngine;

public class MouseControl : MonoBehaviour
{
    RaycastHit hit;
    Collider collider_;
    Movement playerMovement;
    GameManager gameMan;
    InventoryManager inventoryMan;
    UIManager uiMan;

    [Header("Outline Colors")]
    [SerializeField] Color infoColor;
    [SerializeField] Color textColor;
    [SerializeField] Color itemColor;
    [SerializeField] Color interactionColor;

    private void Start()
    {
        uiMan = UIManager.Instance;
        inventoryMan = InventoryManager.Instance;
        gameMan = GameManager.Instance;
        playerMovement = gameMan.player.GetComponent<Movement>();
    }

    void Update()
    {
        hit = MouseTools.GetMouseRayHit();

        if (hit.collider == null) return;

        if (collider_ == null)
        {
            collider_ = hit.collider;
        }

        LeftClick();
        RightClick();
    }

    private void LeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Navigation();
            UseItem();
        }
    }

    private void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            inventoryMan.inventory.DesellectItem();
            uiMan.inventoryUI.CloseInventory();
        }
    }


    private void UseItem()
    {
        if (gameMan.gameState != GameManager.GameState.ItemHandling) return;

        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

        if (interactable != null) 
        {
            gameMan.SwitchGameState(GameManager.GameState.Navigation);
            playerMovement.SetInteractable(interactable);
            uiMan.inventoryUI.CloseInventory();
        }
    }

    private void Navigation()
    {
        if (gameMan.gameState != GameManager.GameState.Navigation) return;

        if (playerMovement == null)
            playerMovement = gameMan.player.GetComponent<Movement>();

        if (inventoryMan.activeItem != null)
        {
            inventoryMan.inventory.ReturnItem();
        }

        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            print("Setting interactable " + interactable);
            playerMovement.SetInteractable(interactable);
        }
        else
        {
            Vector3 target = MouseTools.GetMouseRayHit().point;
            print("Setting destination " + target);
            playerMovement.SetDestination(target);
        }
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SetItemCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}
