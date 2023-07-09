using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private List<Tile> tilePrefabs;

    private static Dictionary<TileBaseType, Tile> tiles = new Dictionary<TileBaseType, Tile>();

    private void Awake()
    {
        foreach (var tilePreafab in tilePrefabs)
        {
            tiles.Add(tilePreafab.BaseType, tilePreafab);
        }
    }

    public static Tile CreateTile(TileBaseType tileBaseType)
    {
        if (tiles.Count == 0) return null;
        Tile tilePrefab = null;
        if (tiles.TryGetValue(tileBaseType, out tilePrefab))
        {
            tilePrefab = Instantiate(tilePrefab);
        }
        return tilePrefab;
    }
}
