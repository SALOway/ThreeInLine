using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3Int Position;
    public TileBaseType BaseType { get; private set; }

    public void Init(Vector3Int position, TileBaseType baseType)
    {
        Position = position;
        BaseType = baseType;
    }
}

// Can be color or form
public enum TileBaseType
{
    Red,
    Green,
    Blue,
    Yellow,
    White,
    Gray,
    Black,
    Magenta,
    Orange
}


