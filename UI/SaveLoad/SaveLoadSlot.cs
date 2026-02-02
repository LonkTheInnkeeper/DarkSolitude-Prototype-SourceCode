using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSlot : MonoBehaviour
{
    public int index;
    public PlayerData playerData;
    public Sprite screenshot;

    [Space]
    [SerializeField] TextMeshProUGUI fileName;
    [SerializeField] TextMeshProUGUI fileDate;
    [SerializeField] Image screenshotImage;

    public void SetSlot(PlayerData playerData, Sprite screenshot)
    {
        Debug.Log("Player data loaded to slot " + index);

        if (playerData == null) 
        {
            this.playerData = null;
            fileName.text = "Empty file";
            fileDate.text = string.Empty;
            this.screenshot = screenshot;
            screenshotImage.sprite = screenshot;
            return;
        }

        this.playerData = playerData;
        fileName.text = playerData.fileName;
        fileDate.text = playerData.fileDate;
        screenshotImage.sprite = screenshot;
    }
}
