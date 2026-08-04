using UnityEngine;

public class StoryActions : MonoBehaviour
{
    public Loc1Intro loc1Intro;

    public void PlayAction(string actionName)
    {
        switch (actionName)
        {
            case "cutscene1_phase2":
                loc1Intro.Phase2();
                break;
            case "cutscene1_phase3":
                loc1Intro.Phase3();
                break;
        }
    }
}
