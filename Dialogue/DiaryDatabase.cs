using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiaryDatabase : MonoBehaviour
{
    public List<TextAsset> en;
    public List<TextAsset> cz;

    public TextAsset GetDiary(string name)
    {
        if (!PlayerPrefs.HasKey("Localisation"))
            return en.FirstOrDefault(item => item.name == name);

        string localisation = PlayerPrefs.GetString("Localisation");

        switch (localisation)
        {
            case "en":
                return en.FirstOrDefault(item => item.name == name);

            case "cz":
                return cz.FirstOrDefault(item => item.name == name);

            default:
                return null;
        }
    }
}
