using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;

    public void LoadPosition(PlayerData playerData)
    {
        float2 savedCoords = playerData.GetPosition();
        Vector3 position = new Vector3(savedCoords.x,0 ,savedCoords.y);

        movement.WarpTo(position);
    }

    public void SavePosition(PlayerData playerData)
    {
        print(transform.position);
        playerData.SavePosition(transform.position.x, transform.position.z);
    }
}
