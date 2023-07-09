using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
    private Tile[,] _tiles;
    public int Width => _tiles.GetLength(0);
    public int Height => _tiles.GetLength(1);

    public TileGrid(int height, int width)
    {
        _tiles = new Tile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                System.Array values = System.Enum.GetValues(typeof(TileBaseType));
                TileBaseType randomTileBaseType = (TileBaseType)values.GetValue(Random.Range(0, values.GetLength(0)));
                Tile tile = TileGenerator.CreateTile(randomTileBaseType);
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tile.Position = tilePosition;
                _tiles[x, y] = tile;

                tile.transform.position = tilePosition;
            }
        }
    }
    public TileGrid(TileBaseType[,] tileLayout)
    {
        int height = tileLayout.GetLength(0);
        int width = tileLayout.GetLength(1);
        _tiles = new Tile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                TileBaseType tileBaseType = tileLayout[height - y - 1, x];
                Tile tile = TileGenerator.CreateTile(tileBaseType);
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tile.Position = tilePosition;
                _tiles[x, y] = tile;
            }
        }
    }

    public Tile GetTile(Vector3Int tilePosition)
    {
        Tile tile = null;
        bool XIsValid = tilePosition.x >= 0 && tilePosition.x < Width;
        bool YIsValid = tilePosition.y >= 0 && tilePosition.y < Height;
        if (_tiles != null && XIsValid && YIsValid)
        {
            tile = _tiles[tilePosition.x, tilePosition.y];
        }
        return tile;
    }

    public void SwapTiles(Tile firstTile, Tile secondTile)
    {
        Vector3Int firstTilePosition = firstTile.Position;
        Vector3Int secondTilePosition = secondTile.Position;

        (_tiles[firstTilePosition.x, firstTilePosition.y], _tiles[secondTilePosition.x, secondTilePosition.y]) = (_tiles[secondTilePosition.x, secondTilePosition.y], _tiles[firstTilePosition.x, firstTilePosition.y]);

        // Update tile positions
        firstTile.Position = secondTilePosition;
        secondTile.Position = firstTilePosition;
    }
}
