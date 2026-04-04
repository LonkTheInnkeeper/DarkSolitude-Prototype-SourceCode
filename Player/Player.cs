using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;

    public void LoadPosition(GameData gameData)
    {
        float2 savedCoords = gameData.playerData.GetPosition();
        Vector3 position = new Vector3(savedCoords.x,0 ,savedCoords.y);

        movement.WarpTo(position);
    }

    public void SavePosition(GameData gameData)
    {
        print(transform.position);
        gameData.playerData.SavePosition(transform.position.x, transform.position.z);
    }
}
