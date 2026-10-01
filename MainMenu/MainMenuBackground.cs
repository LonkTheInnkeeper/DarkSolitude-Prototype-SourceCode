using System.Collections;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    public Sprite doorOn;
    public Sprite doorOff;
    public Sprite doorLocked;

    [Space]
    public ParticleSystemRenderer smog;
    public Material green;
    public Material red;
    public Material shade;

    SpriteRenderer renderer_;

    [Space]
    public Texture2D mouseCursor;

    [SerializeField] AudioEmitter emitter;
    

    private void Start()
    {
        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.Auto);

        renderer_ = GetComponent<SpriteRenderer>();
        renderer_.sprite = doorOn;

        StartCoroutine(DoorFlicker());
    }

    IEnumerator DoorFlicker()
    {
        while (true)
        {
            renderer_.sprite = doorOn;
            smog.material = green;
            yield return new WaitForSeconds(GetRandomTime(3, 5));

            renderer_.sprite = doorOff;
            smog.material = shade;
            emitter.Play();
            yield return new WaitForSeconds(0.1f);

            if (PercentChance(20))
            {
                renderer_.sprite = doorLocked;
                smog.material = red;
                yield return new WaitForSeconds(0.5f);

                renderer_.sprite = doorOff;
                smog.material = shade;
                emitter.Play();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private float GetRandomTime(float a, float b)
    {
        return Random.Range(a, b);
    }

    private bool FlipCoin()
    {
        int coin = Random.Range(0, 2);

        return coin == 1 ? true : false;
    }

    private bool PercentChance(int chance)
    {
        int value = Random.Range(0, 101);

        return value <= chance ? true : false;
    }
}
