using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MouseTools
{
    static bool debug = false;

    public static RaycastHit GetMouseRayHit()
    {
        Ray ray = GameManager.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit;
        }
        else
        {
            DebugLog("Mouse raycast is infinite");
            return default;
        }
    }

    static void DebugLog(string message)
    {
        if (debug)
            Debug.Log(message);
    }

    public static bool IsMouseOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    public static Texture2D SpriteToTexture(Sprite sprite)
    {
        // Vytvoøí novou Texturu ze Sprite
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.rect.x, (int)sprite.rect.y,
            (int)sprite.rect.width, (int)sprite.rect.height
        );
        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}
