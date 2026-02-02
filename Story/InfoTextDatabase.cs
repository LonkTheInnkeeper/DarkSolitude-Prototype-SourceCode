using UnityEngine;

public class InfoTextDatabase : MonoBehaviour
{
    public InfoTextData infoTextData;

    private void Start()
    {
        SwitchLocalisation();
    }

    public string GetInfoText(int index)
    {
        return infoTextData.data[index];
    }

    public void SwitchLocalisation()
    {
        if (!PlayerPrefs.HasKey("Localisation"))
        {
            infoTextData = SaveLoadSystem.LoadInfoText("en");
            print("Loaded default info " + infoTextData.localisation);
            return;
        }

        string localisation = PlayerPrefs.GetString("Localisation");

        switch (localisation)
        {
            case "en":
                infoTextData = SaveLoadSystem.LoadInfoText("en");
                break;

            case "cz":
                infoTextData = SaveLoadSystem.LoadInfoText("cz");
                break;

            default:
                break;
        }

        print("Loaded info " + infoTextData.localisation);
    }
}
