using UnityEngine;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    RaycastHit hit;
    Collider collider_;

    Movement playerMovement;
    GameManager gameMan;
    InventoryManager inventoryMan;
    UIManager uiMan;

    [Header("Cursor icons")]
    [SerializeField] Texture2D arrow;
    [SerializeField] Texture2D hand;
    [SerializeField] Texture2D info;

    [SerializeField] Image cursorIcon;
    [SerializeField] Sprite defaultIcon;

    [Header("Effects")]
    [SerializeField] InteractionFeedbackHandler interactionFeedback;

    public enum CursorType
    {
        Arrow,
        Interaction,
        InfoText,
        Item
    }

    private void Start()
    {
        Cursor.SetCursor(arrow, Vector2.zero, CursorMode.Auto);

        uiMan = UIManager.Instance;
        inventoryMan = InventoryManager.Instance;
        gameMan = GameManager.Instance;

        playerMovement = gameMan.player.GetComponent<Movement>();
    }

    private void Update()
    {
        RightClick();

        hit = MouseTools.GetMouseRayHit();

        if (HandleItemMode()) return;
        if (!TryGetCollider()) return;

        HandleHoverUI();
        LeftClick();
    }

    // -------------------------
    // FLOW CONTROL
    // -------------------------

    private bool HandleItemMode()
    {
        if (gameMan.GetGameState() != GameManager.GameState.ItemHandling)
            return false;

        cursorIcon.rectTransform.position = Input.mousePosition;
        SwitchCursor(CursorType.Item);

        LeftClick();
        return true;
    }

    private bool TryGetCollider()
    {
        collider_ = hit.collider;

        if (collider_ != null)
            return true;

        uiMan.infoTextUI.ToggleInfotext(false, string.Empty);
        SwitchCursor(CursorType.Arrow);

        return false;
    }

    // -------------------------
    // HOVER LOGIC
    // -------------------------

    private void HandleHoverUI()
    {
        collider_.TryGetComponent(out IInteractable interactable);
        collider_.TryGetComponent(out InfoText infoText);

        UpdateCursor(interactable, infoText);
        UpdateInfoText(infoText);
    }

    private void UpdateCursor(IInteractable interactable, InfoText infoText)
    {
        if (interactable != null)
        {
            SwitchCursor(CursorType.Interaction);
        }
        else if (infoText != null)
        {
            SwitchCursor(CursorType.InfoText);
        }
        else
        {
            SwitchCursor(CursorType.Arrow);
        }
    }

    private void UpdateInfoText(InfoText infoText)
    {
        uiMan.infoTextUI.ToggleInfotext(
            infoText != null,
            infoText != null ? infoText.textKey : string.Empty
        );
    }

    // -------------------------
    // INPUT
    // -------------------------

    private void LeftClick()
    {
        if (!Input.GetMouseButton(0))
            return;

        Navigation();
        UseItem();
    }

    private void RightClick()
    {
        if (!Input.GetMouseButtonDown(1))
            return;

        if (gameMan.GetGameState() == GameManager.GameState.Inventory ||
            gameMan.GetGameState() == GameManager.GameState.ItemHandling)
        {
            inventoryMan.inventory.ReturnActiveItem();
            SwitchCursor(CursorType.Arrow);
        }
    }

    // -------------------------
    // GAMEPLAY LOGIC
    // -------------------------

    private void UseItem()
    {
        if (gameMan.GetGameState() != GameManager.GameState.ItemHandling)
            return;

        if (hit.collider != null && hit.collider.TryGetComponent(out IInteractable interactable))
        {
            playerMovement.SetInteractable(interactable);
        }
    }

    private void Navigation()
    {
        var state = gameMan.GetGameState();

        if (state != GameManager.GameState.Navigation &&
            state != GameManager.GameState.Closeup)
            return;

        if (hit.collider.TryGetComponent(out IInteractable interactable))
        {
            playerMovement.SetInteractable(interactable);
            //interactionFeedback.SpawnFeedback(hit.point);
        }

        if (state != GameManager.GameState.Navigation)
            return;

        if (inventoryMan.activeItem != null)
        {
            inventoryMan.inventory.ReturnActiveItem();
        }

        if (interactable == null)
        {
            Vector3 target = hit.point;
            playerMovement.SetDestination(target);
        }
    }

    // -------------------------
    // CURSOR
    // -------------------------

    public void SwitchCursor(CursorType cursorType)
    {
        if (cursorType != CursorType.Item)
        {
            Cursor.visible = true;
            cursorIcon.sprite = defaultIcon;
        }

        switch (cursorType)
        {
            case CursorType.Arrow:
                Cursor.SetCursor(arrow, Vector2.zero, CursorMode.Auto);
                break;

            case CursorType.Interaction:
                Cursor.SetCursor(hand, Vector2.zero, CursorMode.Auto);
                break;

            case CursorType.InfoText:
                Cursor.SetCursor(info, Vector2.zero, CursorMode.Auto);
                break;

            case CursorType.Item:
                Cursor.visible = false;

                if (inventoryMan.activeItem != null &&
                    cursorIcon.sprite != inventoryMan.activeItem.inventoryIcon)
                {
                    cursorIcon.sprite = inventoryMan.activeItem.inventoryIcon;
                }
                break;
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