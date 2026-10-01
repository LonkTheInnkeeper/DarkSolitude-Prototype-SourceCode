using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CameraControl : MonoBehaviour
{
    Transform target;
    [SerializeField] float yOffset;
    [SerializeField] float camSpeed;
    [SerializeField] Transform cameraTransform;

    [Header("Zoom")]
    [SerializeField] float defaultZoom;
    [SerializeField] float dialogueZoom;
    [SerializeField] float zoomSpeed;

    [Header("Cam Shake")]
    [SerializeField] float shakeMagnitude;
    float shakeTime;

    bool forceSnap = false;

    GameManager gameman;

    private void Start()
    {
        gameman = GameManager.Instance;
        FocusOnPlayer();
    }

    void Update()
    {
        if (gameman.closeupState) return;

        if (forceSnap)
        {
            transform.position = target.position + new Vector3(0, yOffset, 0);
            forceSnap = false;
            return;
        }

            FollowTarget(target);

        if (shakeTime > 0)
        {
            cameraTransform.localPosition = Vector3.zero + Random.insideUnitSphere * shakeMagnitude;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            cameraTransform.localPosition = Vector3.zero;
        }
    }

    void FollowTarget(Transform target)
    {
        transform.position = Vector3.Lerp(
        transform.position,
        target.position + new Vector3(0, yOffset, 0),
        camSpeed * Time.deltaTime
    );
    }

    public void FocusOnPlayer()
    {
        if (target == null)
            target = GameManager.Instance.player.transform;

        forceSnap = true;
    }

    public void FocusOnTarget(Transform target)
    {
        transform.position = target.position;
    }

    public void Shake(float duration)
    {
        shakeTime = duration;
    }

    public void ToDialogueZoom()
    {
        StartCoroutine(ZoomRoutine(dialogueZoom));
    }

    public void ToDefaultZoom()
    {
        StartCoroutine(ZoomRoutine(defaultZoom));
    }

    IEnumerator ZoomRoutine(float zoomValue)
    {
        float time = 0;
        float currentZoom = Camera.main.orthographicSize;

        while (time < zoomSpeed)
        {
            time += Time.deltaTime;

            float t = time / zoomSpeed;
            t = Mathf.SmoothStep(0f, 1f, t);

            Camera.main.orthographicSize = Mathf.Lerp(
                currentZoom,
                zoomValue,
                t);

            yield return null;
        }

        Camera.main.orthographicSize = zoomValue;
    }

    private void OnEnable()
    {
        GameEvents.OnDialogueStart += ToDialogueZoom;
        GameEvents.OnDialogueEnd += ToDefaultZoom;
    }

    private void OnDisable()
    {
        GameEvents.OnDialogueStart -= ToDialogueZoom;
        GameEvents.OnDialogueEnd -= ToDefaultZoom;
    }
}
