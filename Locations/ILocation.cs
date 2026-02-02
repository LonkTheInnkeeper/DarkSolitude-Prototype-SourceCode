public interface ILocation
{
    public int GetIndex();
    public void SetData(LocationData data);
    public LocationData GetData();
    public bool ContainsKey(string key);
    public void AddKey (string key);
}
