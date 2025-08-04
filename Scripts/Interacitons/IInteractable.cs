
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public List<Transform> GetInteractionPoints();
    public Vector3 GetPosition();
}
