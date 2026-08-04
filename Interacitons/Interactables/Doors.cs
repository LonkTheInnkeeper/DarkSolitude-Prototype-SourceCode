using System.Collections;
using UnityEngine;

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

    AudioSource audioSource;
    string openTrigger = "Open";
    string closeTrigger = "Close";

    bool transfering = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        doorLock.SetDoorLock(doorUnlocked);
        doorLock.SetDoorsOn(doorOn);
    }

    public void TransferLocation()
    {
        if (!doorUnlocked || !doorOn || transfering) return;

        ToggleDoorsAnimation(true);
        StartCoroutine(TransferingRoutine());
    }

    IEnumerator TransferingRoutine()
    {
        transfering = true;
        yield return new WaitForSeconds(1);
        UIManager.Instance.screenShade.ShadeOnAction(() => LocationManager.Instance.SwitchLocation(this));
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

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

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

            audioSource.clip = audioMan.doorOpen;
            audioSource.Play();
        }
        else
        {
            print("Opening door");
            doorAnimation.SetTrigger(openTrigger);
            doorLock.ToggleLockAnimation("Close");

            audioSource.clip = audioMan.doorClose;
            audioSource.Play();
        }

        return true;
    }

    private void OnEnable()
    {
        transfering = false;
    }
}
