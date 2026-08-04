using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
        if (gameData == null) 
        {
            this.gameData = null;
            fileName.text = UIManager.Instance.textData.uiTexts["saveLoad_empty"];
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
