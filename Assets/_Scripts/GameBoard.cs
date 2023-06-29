﻿using System.Collections;
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
    /// <summary>
    /// Swaps tiles at given positions in grid
    /// </summary>
    /// <param name="firstTileGridPosition"></param>
    /// <param name="secondTileGridPosition"></param>
    public void SwapTilesAt(Vector3Int firstTileGridPosition, Vector3Int secondTileGridPosition)
    {
        Tile firstTile = GetTile(firstTileGridPosition);
        Tile secondTile = GetTile(secondTileGridPosition);

        (_tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y], _tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y]) = (_tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y], _tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y]);

        firstTile.GridPosition = secondTileGridPosition;
        secondTile.GridPosition = firstTileGridPosition;
    }
    /// <summary>
    /// Swaps tiles in grid
    /// </summary>
    /// <param name="firstTile"></param>
    /// <param name="secondTile"></param>
    public void SwapTiles(Tile firstTile, Tile secondTile)
    {
        Vector3Int firstTileGridPosition = firstTile.GridPosition;
        Vector3Int secondTileGridPosition = secondTile.GridPosition;

        (_tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y], _tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y]) = (_tilesInGrid[secondTileGridPosition.x, secondTileGridPosition.y], _tilesInGrid[firstTileGridPosition.x, firstTileGridPosition.y]);
        
        firstTile.GridPosition = secondTileGridPosition;
        secondTile.GridPosition = firstTileGridPosition;
    }


    public List<List<Tile>> FindAllCombinations()
    {
        List<List<Tile>> allCombinations = new List<List<Tile>>();

        for (int x = 0; x < _tilesInGrid.GetLength(0); x++)
        {
            allCombinations.AddRange(FindVerticalCombinationsAt(x));
        }

        for (int y = 0; y < _tilesInGrid.GetLength(1); y++)
        {
            allCombinations.AddRange(FindHorizontalCombinationsAt(y));
        }

        return allCombinations;
    }

    public List<List<Tile>> FindCombinationsAt(Vector3Int tileGridPosition)
    {
        List<List<Tile>> combinations = new List<List<Tile>>();

        combinations.AddRange(FindVerticalCombinationsAt(tileGridPosition.x));
        combinations.AddRange(FindHorizontalCombinationsAt(tileGridPosition.y));

        return combinations;
    }

    private List<List<Tile>> FindHorizontalCombinationsAt(int y)
    {
        List<List<Tile>> horizontalCombinations = new List<List<Tile>>();

        List<Tile> currentCombination = new List<Tile>();
        Tile lastTile = null;
        for (int x = 0; x < _tilesInGrid.GetLength(0); x++)
        {
            Vector3Int tileGridPosition = new Vector3Int(x, y, 0);
            Tile tile = GetTile(tileGridPosition);
            if (currentCombination.Count < 1) { currentCombination.Add(tile); lastTile = tile; continue; }
            
            if (tile.Type == lastTile.Type)
            {
                currentCombination.Add(tile);
            }
            else
            {
                if (currentCombination.Count >= 3)
                {
                    horizontalCombinations.Add(currentCombination);
                }
                currentCombination = new List<Tile>();
            }

            lastTile = tile;
        }

        return horizontalCombinations;
    }

    private List<List<Tile>> FindVerticalCombinationsAt(int x)
    {
        List<List<Tile>> verticalCombinations = new List<List<Tile>>();

        List<Tile> currentCombination = new List<Tile>();
        Tile lastTile = null;
        for (int y = 0; y < _tilesInGrid.GetLength(1); y++)
        {
            Vector3Int tileGridPosition = new Vector3Int(x, y, 0);
            Tile tile = GetTile(tileGridPosition);
            if (currentCombination.Count < 1) { currentCombination.Add(tile); lastTile = tile; continue; }

            if (tile.Type == lastTile.Type)
            {
                currentCombination.Add(tile);
            }
            else
            {
                if (currentCombination.Count >= 3)
                {
                    verticalCombinations.Add(currentCombination);
                }
                currentCombination = new List<Tile>();
            }

            lastTile = tile;
        }

        return verticalCombinations;
    }
}
