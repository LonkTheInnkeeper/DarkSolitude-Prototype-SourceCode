using UnityEngine;

public static class SystemTools
{
    public static T ParseEnum<T>(string value) where T : struct
    {
        if (System.Enum.TryParse(value, true, out T result))
            return result;

        throw new System.Exception($"Invalid enum value: {value}");
    }
}
