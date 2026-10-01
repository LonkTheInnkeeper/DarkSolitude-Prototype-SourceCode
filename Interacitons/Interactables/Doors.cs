using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    public Doors targetDoors;
    public GameObject targetLocation;

    [Space]
    public bool doorUnlocked;
    public bool doorOn;
    public Transform spawnPoint;

    [Space]
    [SerializeField] Animator doorAnimation;
    [SerializeField] DoorLock doorLock;

    [Space]
    [SerializeField] AudioEmitter openSound;
    [SerializeField] AudioEmitter closeSound;
    
    string openTrigger = "Open";
    string closeTrigger = "Close";

    bool transfering = false;

    private void Start()
    {
        doorLock.SetDoorLock(doorUnlocked);
        doorLock.SetDoorsOn(doorOn);
    }

    public void TransferLocation()
    {
        if (!doorUnlocked || !doorOn || transfering) return;

        ToggleDoorsAnimation(true);
        StartCoroutine(TransferingLocationRoutine());
    }

    public void TransferScene(int index)
    {
        if (!doorUnlocked || !doorOn || transfering) return;

        ToggleDoorsAnimation(true);
        StartCoroutine(TransferingSceneRoutine(index));
    }

    IEnumerator TransferingLocationRoutine()
    {
        transfering = true;
        yield return new WaitForSeconds(1);
        UIManager.Instance.screenShade.ShadeOnAction(() => LocationManager.Instance.SwitchLocation(this));
        transfering = false;
    }

    IEnumerator TransferingSceneRoutine(int index)
    {
        transfering = true;
        yield return new WaitForSeconds(1);
        UIManager.Instance.screenShade.ShadeOnAction(() => SceneManager.LoadScene(index));
        transfering = false;
    }

    public void SetDoorUnlocked(bool unlocked)
    {
        doorUnlocked = unlocked;
        doorLock.SetDoorLock(doorUnlocked);
    }

    public void SetDoorsOn(bool on)
    {
        doorLock.SetDoorsOn(on);
    }

    public bool ToggleDoorsAnimation(bool open)
    {
        AudioManager audioMan = AudioManager.Instance;

        if (doorAnimation == null)
        {
            doorAnimation = GetComponent<Animator>();
        }

        print("Toggling door");

        if (!open)
        {
            print("Closing door");
            doorAnimation.SetTrigger(closeTrigger);
            doorLock.ToggleLockAnimation("Open");

            openSound.Play();
        }
        else
        {
            print("Opening door");
            doorAnimation.SetTrigger(openTrigger);
            doorLock.ToggleLockAnimation("Close");

            closeSound.Play();
        }

        return true;
    }

    private void OnEnable()
    {
        transfering = false;
    }
}
