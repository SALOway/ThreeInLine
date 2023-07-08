using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileBaseType _baseType;

    public Vector3Int Position;
    public TileBaseType BaseType => _baseType;
}

// Can be color or form
public enum TileBaseType
{
    Red,
    Green,
    Blue,
    Yellow,
    White,
    Magenta,
    Orange
}


