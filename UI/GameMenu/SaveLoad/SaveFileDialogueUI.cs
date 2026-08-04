using System.Linq;
using TMPro;
using UnityEngine;

public class SaveFileDialogueUI : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TextMeshProUGUI charCounter;

    private void Update()
    {
        if (!inputField.gameObject.activeInHierarchy) return;

        charCounter.text = $"{inputField.text.Count().ToString()}/{inputField.characterLimit.ToString()}";
    }
}
