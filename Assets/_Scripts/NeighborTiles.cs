using System.Collections.Generic;
using UnityEngine;

public class CornerNeighborTiles
{
    public Vector3Int BaseTileGridPosition { get; private set; }
    public IReadOnlyList<Tile> Tiles;

    public bool HasCornerNeighbors => Tiles.Count > 0;

    public CornerNeighborTiles(Vector3Int baseTile, IReadOnlyList<Tile> cornerNeighbors)
    {
        BaseTileGridPosition = baseTile;
        Tiles = cornerNeighbors;
    }
}
