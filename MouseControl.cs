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

    [Header("Cursor icons")]
    [SerializeField] Texture2D arrow;
    [SerializeField] Texture2D hand;
    [SerializeField] Texture2D info;

    public enum CursorType
    {
        Arrow,
        Invetaction,
        InfoText
    }

    private void Start()
    {
        Cursor.SetCursor(arrow, Vector2.zero, CursorMode.Auto);

        uiMan = UIManager.Instance;
        inventoryMan = InventoryManager.Instance;
        gameMan = GameManager.Instance;
        playerMovement = gameMan.player.GetComponent<Movement>();
    }

    void Update()
    {
        hit = MouseTools.GetMouseRayHit();

        if (hit.collider == null)
        {
            uiMan.infoTextUI.ToggleInfotext(false, -1);
            SwitchCursor(CursorType.Arrow);
            return;
        }
        else
        {
            collider_ = hit.collider;
        }

        if (collider_ == null)
        {
            collider_ = hit.collider;
        }

        if (collider_.GetComponent<IInteractable>() != null)
            SwitchCursor(CursorType.Invetaction);
        else
            SwitchCursor(CursorType.Arrow);

        if (collider_.GetComponent<InfoText>() != null)
        {
            uiMan.infoTextUI.ToggleInfotext(true, collider_.GetComponent<InfoText>().textIndex);

            if (collider_.GetComponent<IInteractable>() == null)
            {
                SwitchCursor(CursorType.InfoText);
            }
        }
        else
        {
            uiMan.infoTextUI.ToggleInfotext(false, -1);
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
            if (gameMan.gameState == GameManager.GameState.Inventory ||
                gameMan.gameState == GameManager.GameState.ItemHandling)
            {
                inventoryMan.inventory.ReturnActiveItem();
                uiMan.inventoryUI.ToggleInventory();
            }
        }
    }

    private void UseItem()
    {
        if (gameMan.gameState != GameManager.GameState.ItemHandling) return;

        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            uiMan.inventoryUI.ToggleInventory();
            playerMovement.SetInteractable(interactable);
        }
    }

    private void Navigation()
    {
        if (gameMan.gameState != GameManager.GameState.Navigation &&
            gameMan.gameState != GameManager.GameState.Closeup)
            return;

        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            print("Setting interactable " + interactable);
            playerMovement.SetInteractable(interactable);
        }

        if (gameMan.gameState != GameManager.GameState.Navigation) return;

        if (playerMovement == null)
            playerMovement = gameMan.player.GetComponent<Movement>();

        if (inventoryMan.activeItem != null)
        {
            inventoryMan.inventory.ReturnActiveItem();
        }

        if (interactable == null)
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

    public void SwitchCursor(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.Arrow:
                Cursor.SetCursor(arrow, Vector2.zero, CursorMode.Auto);
                break;

            case CursorType.Invetaction:
                Cursor.SetCursor(hand, Vector2.zero, CursorMode.Auto);
                break;

            case CursorType.InfoText:
                Cursor.SetCursor(info, Vector2.zero, CursorMode.Auto);
                break;

            default:
                break;
        }
    }
}
