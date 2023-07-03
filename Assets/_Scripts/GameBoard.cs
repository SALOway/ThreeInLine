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
    public Tile GetTileAt(Vector3Int tileGridPosition)
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
    public void SetTiles(Tile[,] tiles) => _tilesInGrid = tiles;
    /// <summary>
    /// Swaps tiles at given positions in grid
    /// </summary>
    /// <param name="firstTileGridPosition"></param>
    /// <param name="secondTileGridPosition"></param>
    public void SwapTilesAt(Vector3Int firstTileGridPosition, Vector3Int secondTileGridPosition)
    {
        Tile firstTile = GetTileAt(firstTileGridPosition);
        Tile secondTile = GetTileAt(secondTileGridPosition);

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

    public List<Combination> FindAllCombinations()
    {
        List<Combination> allCombinations = new List<Combination>();

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

    public List<Combination> FindCombinationsAt(Vector3Int tileGridPosition)
    {
        List<Combination> combinations = new List<Combination>();

        combinations.AddRange(FindVerticalCombinationsAt(tileGridPosition.x));
        combinations.AddRange(FindHorizontalCombinationsAt(tileGridPosition.y));

        return combinations;
    }

    private List<Combination> FindHorizontalCombinationsAt(int y)
    {
        List<Combination> horizontalCombinations = new List<Combination>();

        Stack<Tile> currentCombination = new Stack<Tile>();
        for (int x = 0; x < _tilesInGrid.GetLength(0); x++)
        {
            Vector3Int tileGridPosition = new Vector3Int(x, y, 0);
            Tile tile = GetTileAt(tileGridPosition);

            if (currentCombination.Count < 1)
            {
                currentCombination.Push(tile);
            }
            else
            {
                if (tile.Type == currentCombination.Peek().Type)
                {
                    currentCombination.Push(tile);
                }
                else
                {
                    if (currentCombination.Count >= 3)
                    {
                        horizontalCombinations.Add(new Combination(currentCombination.ToArray(), CombinationType.Horizontal));
                    }
                    currentCombination.Clear();
                    currentCombination.Push(tile);
                }
            }

            if (currentCombination.Count >= 3)
            {
                horizontalCombinations.Add(new Combination(currentCombination.ToArray(), CombinationType.Horizontal));
            }
        }
        return horizontalCombinations;
    }

    private List<Combination> FindVerticalCombinationsAt(int x)
    {
        List<Combination> verticalCombinations = new List<Combination>();

        Stack<Tile> currentCombination = new Stack<Tile>();
        for (int y = 0; y < _tilesInGrid.GetLength(1); y++)
        {
            Vector3Int tileGridPosition = new Vector3Int(x, y, 0);
            Tile tile = GetTileAt(tileGridPosition);
            if (currentCombination.Count < 1)
            {
                currentCombination.Push(tile);
            }
            else
            {
                if (tile.Type == currentCombination.Peek().Type)
                {
                    currentCombination.Push(tile);
                }
                else
                {
                    if (currentCombination.Count >= 3)
                    {
                        verticalCombinations.Add(new Combination(currentCombination.ToArray(), CombinationType.Vertical));
                    }
                    currentCombination.Clear();
                    currentCombination.Push(tile);
                }
            }

            if (currentCombination.Count >= 3)
            {
                verticalCombinations.Add(new Combination(currentCombination.ToArray(), CombinationType.Vertical));
            }
        }

        return verticalCombinations;
    }

    public int CountAllTilesWithAvailableMoves()
    {
        HashSet<Tile> uniqueTilesWithMove = new HashSet<Tile>();
        for (int x = 0; x < _tilesInGrid.GetLength(0); x++)
        {
            for (int y = 0; y < _tilesInGrid.GetLength(1); y++)
            {
                Vector3Int tileGridPosition = new Vector3Int(x, y, 0);
                FindTilesWithMoveAt(tileGridPosition).ForEach(tile => uniqueTilesWithMove.Add(tile));
            }
        }


        foreach (Tile tile in uniqueTilesWithMove)
        {
            Debug.Log($"Tile Position: ({tile.GridPosition.x},{tile.GridPosition.y})");
        }

        int movesCounter = uniqueTilesWithMove.Count;
        return movesCounter;
    }

    private List<Tile> FindTilesWithMoveAt(Vector3Int tileGridPosition)
    {
        List<Tile> tilesWithMove = new List<Tile>();
        NeighborTiles neighborTiles = GetNeighborTilesAt(GetTileAt(tileGridPosition));

        if (neighborTiles.HasCornerNeighbors)
        {
            if (neighborTiles.Corner.Count > 1)
            {
                tilesWithMove.AddRange(FindTilesForCornerMove(neighborTiles));
            }

            if (neighborTiles.HasHorizontalNeighbors)
            {
                tilesWithMove.AddRange(FindTilesForHorizontalMove(neighborTiles));
            }

            if (neighborTiles.HasVerticalNeighbors)
            {
                tilesWithMove.AddRange(FindTilesForVerticalMove(neighborTiles));
            }
        }
        return tilesWithMove;
    }

    private List<Tile> FindTilesForCornerMove(NeighborTiles neighborTiles)
    {
        List<Tile> tilesWithMove = new List<Tile>();
        foreach (Tile cornerTile in neighborTiles.Corner)
        {
            if (cornerTile == null) continue;
            foreach (Tile other in neighborTiles.Corner)
            {
                if (other == null || cornerTile.GridPosition == other.GridPosition) continue;
                if (cornerTile.GridPosition.y == other.GridPosition.y || cornerTile.GridPosition.x == other.GridPosition.x)
                {
                    tilesWithMove.Add(GetTileAt(neighborTiles.BaseTileGridPosition));
                }
            }
        }
        return tilesWithMove;
    }

    private List<Tile> FindTilesForHorizontalMove(NeighborTiles neighborTiles)
    {
        List<Tile> tilesWithMove = new List<Tile>();
        foreach (Tile horizontlaTile in neighborTiles.Horizontal)
        {
            if (horizontlaTile == null) continue;
            foreach (Tile cornerTile in neighborTiles.Corner)
            {
                if (cornerTile == null) continue;

                if (horizontlaTile.GridPosition.x != cornerTile.GridPosition.x)
                {
                    tilesWithMove.Add(cornerTile);
                }
            }
        }
        return tilesWithMove;
    }
    
    private List<Tile> FindTilesForVerticalMove(NeighborTiles neighborTiles)
    {
        List<Tile> tilesWithMove = new List<Tile>();
        foreach (Tile verticalTile in neighborTiles.Vertical)
        {
            if (verticalTile == null) continue;
            foreach (Tile cornerTile in neighborTiles.Corner)
            {
                if (cornerTile == null) continue;

                if (verticalTile.GridPosition.y != cornerTile.GridPosition.y)
                {
                    tilesWithMove.Add(cornerTile);
                }
            }
        }
        return tilesWithMove;
    }

    public NeighborTiles GetNeighborTilesAt(Tile tile)
    {
        List<Tile> horizontalNeighbors = new List<Tile>();
        List<Tile> verticalNeighbors = new List<Tile>();
        List<Tile> cornerNeighbors = new List<Tile>();
        for (int x = tile.GridPosition.x - 1; x <= tile.GridPosition.x + 1; x++)
        {
            for (int y = tile.GridPosition.y - 1; y <= tile.GridPosition.y + 1; y++)
            {
                Vector3Int neighborTileGridPosition = new Vector3Int(x, y, 0);
                Tile neighborTile = GetTileAt(neighborTileGridPosition);

                if (neighborTile == null) continue;
                if (neighborTile.GridPosition == tile.GridPosition) continue;

                if (neighborTile.GridPosition.y == tile.GridPosition.y)
                {
                    if (neighborTile.Type == tile.Type) horizontalNeighbors.Add(GetTileAt(neighborTileGridPosition));
                    continue;
                }
                if (neighborTile.GridPosition.x == tile.GridPosition.x)
                {
                    if (neighborTile.Type == tile.Type) verticalNeighbors.Add(GetTileAt(neighborTileGridPosition));
                    continue;
                }
                if (neighborTile.Type == tile.Type) cornerNeighbors.Add(GetTileAt(neighborTileGridPosition));
            }
        }
        NeighborTiles neighbors = new NeighborTiles(tile.GridPosition, horizontalNeighbors, verticalNeighbors, cornerNeighbors);
        return neighbors;
    }
}
