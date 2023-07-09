using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private List<Tile> _tilePrefabs = new List<Tile>();

    private static Dictionary<TileBaseType, Tile> _tiles = new Dictionary<TileBaseType, Tile>();

    private void Awake()
    {
        foreach (var tilePreafab in _tilePrefabs)
        {
            _tiles.Add(tilePreafab.BaseType, tilePreafab);
        }
    }

    public static Tile CreateTile(TileBaseType tileBaseType, Vector3 position, Transform parent)
    {
        if (_tiles == null || _tiles.Count == 0)
        {
            Debug.LogWarning("No tiles to create from");
            return null;
        }

        if (!_tiles.TryGetValue(tileBaseType, out Tile tilePrefab))
        {
            Debug.LogWarning($"No tile found for type {tileBaseType}");
            return null;
        }

        Tile newTile = Instantiate(tilePrefab, position, tilePrefab.transform.rotation, parent);
        return newTile;
    }
}