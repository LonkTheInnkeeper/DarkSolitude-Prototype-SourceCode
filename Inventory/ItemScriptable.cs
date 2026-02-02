using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item")]
public class ItemScriptable : ScriptableObject
{
    public string itemName;
    public string itemDesctipion;
    public Sprite inventoryIcon;
    public Mesh inventoryModel;
    public Material inventoryMaterial;
    public Texture2D cursorIcon;

    [Header("Combination")]
    public ItemScriptable secondItem;
    public ItemScriptable comboResult;

    [Space]
    public string comboLine;
}
