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
}
