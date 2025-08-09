using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;

    public void LoadPosition()
    {
        float2 savedCoords = GameManager.Instance.playerData.GetPosition();
        Vector3 position = new Vector3(savedCoords.x,0 ,savedCoords.y);

        movement.WarpTo(position);
    }

    public void SavePosition()
    {
        GameManager.Instance.playerData.SavePosition(transform.position.x, transform.position.z);
    }
}
