

public interface IGridObject
{
    void GetXY(out int x, out int y);

    void SetXY(int x,int y);

    int GetIndex();

    void SetIndex(int index);

    string GetJsonData();

    void Initialize(string jsonData);

}
