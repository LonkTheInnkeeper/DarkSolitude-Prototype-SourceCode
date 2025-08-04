using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    public List<TextAsset> en;
    public List<TextAsset> cz;

    public TextAsset GetDialogue(string name)
    {
        if (!PlayerPrefs.HasKey("Localicastion"))
            return en.FirstOrDefault(item => item.name == name);

        string localisation = PlayerPrefs.GetString("Localicastion");

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
