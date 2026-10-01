using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public ScreenShade shade;
    bool settingsUp = false;
    public GameObject settings;

    private void Start()
    {
        settings.SetActive(false);
        settingsUp = false;
    }

    public void NewGame()
    {
        shade.ShadeOnEvent();
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        Animator settingsAnim = settings.GetComponent<Animator>();

        if (settingsUp)
        {
            settingsAnim.SetTrigger("Close");
            settingsUp = false;
        }
        else
        {
            settings.SetActive(true);
            settingsAnim.SetTrigger("Open");
            settingsUp = true;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
