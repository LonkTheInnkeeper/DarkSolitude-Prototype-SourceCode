using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSlot : MonoBehaviour
{
    public int index;
    public GameData gameData;
    public Sprite screenshot;

    [Space]
    [SerializeField] TextMeshProUGUI fileName;
    [SerializeField] TextMeshProUGUI fileDate;
    [SerializeField] Image screenshotImage;

    public void SetSlot(GameData gameData, Sprite screenshot)
    {
        Debug.Log("Game data loaded to slot " + index);

        if (gameData == null) 
        {
            this.gameData = null;
            fileName.text = "Empty file";
            fileDate.text = string.Empty;
            this.screenshot = screenshot;
            screenshotImage.sprite = screenshot;
            return;
        }

        this.gameData = gameData;
        fileName.text = gameData.fileName;
        fileDate.text = gameData.fileDate;
        screenshotImage.sprite = screenshot;
    }
}
