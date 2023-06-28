using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that storing information about the game board and its tiles. 
/// It provides functionality to perform operations over the tiles.
/// </summary>
public class GameBoard : MonoBehaviour
{
    private Tile[,] _tilesInGrid;

    /// <summary>
    /// Returns tile at the given position on grid
    /// </summary>
    /// <param name="tileGridPosition"></param>
    /// <returns></returns>
    public Tile GetTile(Vector3Int tileGridPosition)
    {
        Tile tile = null;
        if (_tilesInGrid != null)
        {
            bool XIsValid = tileGridPosition.x >= 0 && tileGridPosition.x < _tilesInGrid.GetLength(0);
            bool YIsValid = tileGridPosition.y >= 0 && tileGridPosition.y < _tilesInGrid.GetLength(1);

            if (XIsValid && YIsValid) tile = _tilesInGrid[tileGridPosition.x, tileGridPosition.y];
        }
        return tile;
    }

    /// <summary>
    /// Set current tiles on game board
    /// </summary>
    /// <param name="tiles"></param>
    public void SetTiles(Tile[,] tiles)
    {
        _tilesInGrid = tiles;
    }

    public void SwapTilesAt(Vector3Int firstTileGridPosition, Vector3Int secondTileGridPosition)
    {
        Tile firstTile = GetTile(firstTileGridPosition);
        Tile secondTile = GetTile(secondTileGridPosition);

        (_tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y], _tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y]) = (_tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y], _tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y]);

        firstTile.GridPosition = secondTileGridPosition;
        secondTile.GridPosition = firstTileGridPosition;
    }

    public void SwapTiles(Tile firstTile, Tile secondTile)
    {
        Vector3Int firstTileGridPosition = firstTile.GridPosition;
        Vector3Int secondTileGridPosition = secondTile.GridPosition;

        (_tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y], _tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y]) = (_tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y], _tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y]);
        
        firstTile.GridPosition = secondTileGridPosition;
        secondTile.GridPosition = firstTileGridPosition;
    }
}
