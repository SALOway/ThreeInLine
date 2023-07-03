using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborTiles
{
    public Vector3Int BaseTileGridPosition { get; private set; }
    public IReadOnlyList<Tile> Horizontal;
    public IReadOnlyList<Tile> Vertical;
    public IReadOnlyList<Tile> Corner;

    public bool HasHorizontalNeighbors => Horizontal.Count > 0;
    public bool HasVerticalNeighbors => Vertical.Count > 0;
    public bool HasCornerNeighbors => Corner.Count > 0;


    public NeighborTiles(Vector3Int baseTile, IReadOnlyList<Tile> horizontalNeighbors, IReadOnlyList<Tile> verticalNeighbors, IReadOnlyList<Tile> cornerNeighbors)
    {
        BaseTileGridPosition = baseTile;
        Horizontal = horizontalNeighbors;
        Vertical = verticalNeighbors;
        Corner = cornerNeighbors;
    }
}
