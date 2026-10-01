using System;

[Serializable]
public class GameData
{
    public PlayerData playerData = new PlayerData();
    public AreaData areaData = new AreaData();
    public InventoryData inventoryData = new InventoryData();

    public int index;
    public string fileName = "Empty name";
    public string fileDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

    public void UpdateFileDate()
    {
        fileDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
    }
}
