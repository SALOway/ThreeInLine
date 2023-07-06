using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that storing information about the game board and its tiles. 
/// It provides functionality to perform operations over the tiles.
/// </summary>
public class GameBoard : MonoBehaviour
{
    private Tile[,] _tiles;
    public int Width => _tiles != null ? _tiles.GetLength(0) : 0;
    public int Height => _tiles != null ? _tiles.GetLength(1) : 0;


    /// <summary>
    /// Returns tile at the given position on grid
    /// </summary>
    /// <param name="tilePosition"></param>
    /// <returns></returns>
    public Tile GetTile(Vector3Int tilePosition)
    {
        Tile tile = null;
        bool XIsValid = tilePosition.x >= 0 && tilePosition.x < _tiles.GetLength(0);
        bool YIsValid = tilePosition.y >= 0 && tilePosition.y < _tiles.GetLength(1);
        if (_tiles != null && XIsValid && YIsValid)
        {
            tile = _tiles[tilePosition.x, tilePosition.y];
        }
        return tile;
    }
    /// <summary>
    /// Set current tiles on game board
    /// </summary>
    /// <param name="tiles"></param>
    public void SetTiles(Tile[,] tiles) => _tiles = tiles;
    /// <summary>
    /// Swaps tiles at given positions in grid
    /// </summary>
    /// <param name="firstTilePosition"></param>
    /// <param name="secondTilePosition"></param>
    public void SwapTiles(Tile firstTile, Tile secondTile)
    {
        Vector3Int firstTilePosition = firstTile.Position;
        Vector3Int secondTilePosition = secondTile.Position;
        
        // Literally swap
        (_tiles[firstTilePosition.x, firstTilePosition.y], _tiles[secondTilePosition.x, secondTilePosition.y]) = (_tiles[secondTilePosition.x, secondTilePosition.y], _tiles[firstTilePosition.x, firstTilePosition.y]);

        // Update position
        firstTile.Position = secondTilePosition;
        secondTile.Position = firstTilePosition;
    }

    #region Combinations
    public List<Combination> FindAllCombinations()
    {
        List<Combination> allCombinations = new List<Combination>();

        for (int columnIndex = 0; columnIndex < Width; columnIndex++)
        {
            allCombinations.AddRange(FindVerticalCombinationsAt(columnIndex));
        }

        for (int rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            allCombinations.AddRange(FindHorizontalCombinationsAt(rowIndex));
        }

        return allCombinations;
    }

    public List<Combination> FindCombinationsAt(Vector3Int tilePosition)
    {
        List<Combination> combinations = new List<Combination>();

        combinations.AddRange(FindVerticalCombinationsAt(tilePosition.x));
        combinations.AddRange(FindHorizontalCombinationsAt(tilePosition.y));

        return combinations;
    }

    private List<Combination> FindHorizontalCombinationsAt(int rowIndex)
    {
        List<Combination> horizontalCombinations = new List<Combination>();

        Stack<Tile> currentCombination = new Stack<Tile>();
        for (int columnIndex = 0; columnIndex < Width; columnIndex++)
        {
            Vector3Int tilePosition = new Vector3Int(columnIndex, rowIndex, 0);
            Tile tile = GetTile(tilePosition);

            if (currentCombination.Count < 1)
            {
                currentCombination.Push(tile);
            }
            else
            {
                if (tile.BaseType == currentCombination.Peek().BaseType)
                {
                    currentCombination.Push(tile);
                }
                else
                {
                    if (currentCombination.Count >= 3)
                    {
                        Combination combination = new Combination(currentCombination.ToArray(), CombinationType.Horizontal);
                        horizontalCombinations.Add(combination);
                    }
                    currentCombination.Clear();
                    currentCombination.Push(tile);
                }
            }

            if (currentCombination.Count >= 3)
            {
                Combination combination = new Combination(currentCombination.ToArray(), CombinationType.Horizontal);
                horizontalCombinations.Add(combination);
            }
        }
        return horizontalCombinations;
    }

    private List<Combination> FindVerticalCombinationsAt(int columnIndex)
    {
        List<Combination> verticalCombinations = new List<Combination>();

        Stack<Tile> currentCombination = new Stack<Tile>();
        for (int rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            Vector3Int tilePosition = new Vector3Int(columnIndex, rowIndex, 0);
            Tile tile = GetTile(tilePosition);
            if (currentCombination.Count < 1)
            {
                currentCombination.Push(tile);
            }
            else
            {
                if (tile.BaseType == currentCombination.Peek().BaseType)
                {
                    currentCombination.Push(tile);
                }
                else
                {
                    if (currentCombination.Count >= 3)
                    {
                        Combination combination = new Combination(currentCombination.ToArray(), CombinationType.Vertical);
                        verticalCombinations.Add(combination);
                    }
                    currentCombination.Clear();
                    currentCombination.Push(tile);
                }
            }

            if (currentCombination.Count >= 3)
            {
                Combination combination = new Combination(currentCombination.ToArray(), CombinationType.Vertical);
                verticalCombinations.Add(combination);
            }
        }

        return verticalCombinations;
    }
    #endregion

    public List<Tile> FindAllTilesWithAvailableMoves()
    {
        HashSet<Tile> tilesWithMoves = new HashSet<Tile>();

        for (int columnIndex = 0; columnIndex < Width; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                Vector3Int tilePosition = new Vector3Int(columnIndex, rowIndex, 0);
                Tile tile = GetTile(tilePosition);
                if (tile == null) continue;
                if (CheckTileForMove(tile))
                {
                    tilesWithMoves.Add(tile);
                }
            }
        }

        return new List<Tile>(tilesWithMoves);
    }

    private bool CheckTileForMove(Tile tile)
    {
        CornerNeighborTiles cornerNeighbors = GetCornerNeighborTiles(tile);

        List<Tile> leftTiles = new List<Tile>
        {
            GetTile(tile.Position + Vector3Int.left * 2),
            GetTile(tile.Position + Vector3Int.left * 3),
        };
        List<Tile> rightTiles = new List<Tile>
        {
            GetTile(tile.Position + Vector3Int.right * 2),
            GetTile(tile.Position + Vector3Int.right * 3),
        };
        List<Tile> topTiles = new List<Tile>
        {
            GetTile(tile.Position + Vector3Int.up * 2),
            GetTile(tile.Position + Vector3Int.up * 3),
        };
        List<Tile> bottomTiles = new List<Tile>
        {
            GetTile(tile.Position + Vector3Int.down * 2),
            GetTile(tile.Position + Vector3Int.down * 3),
        };
        List<List<Tile>> allTiles = new List<List<Tile>>() { leftTiles, rightTiles, topTiles, bottomTiles};
        foreach (List<Tile> tiles in allTiles)
        {
            if (tiles[0] == null || tiles[1] == null) continue;
            if (tile.BaseType == tiles[0].BaseType && tile.BaseType == tiles[1].BaseType)
            {
                return true;
            }
        }
        
        if (cornerNeighbors.HasCornerNeighbors)
        {
            if (cornerNeighbors.Tiles.Count > 1)
            {
                #region Try to find paar of corner tiles
                foreach (Tile cornerTile in cornerNeighbors.Tiles)
                {
                    foreach (Tile other in cornerNeighbors.Tiles)
                    {
                        if (other == cornerTile) continue;
                        bool equalX = other.Position.x == cornerTile.Position.x;
                        bool equalY = other.Position.y == cornerTile.Position.y;
                        if (equalX || equalY) return true;
                    }
                }
                #endregion
            }
            #region Try to find at least one tile near corner tile, that creates combination
            foreach (Tile cornerTile in cornerNeighbors.Tiles)
            {
                int cornerTileX = cornerTile.Position.x;
                int cornerTileY = cornerTile.Position.y;

                int relativeX = cornerTileX - tile.Position.x;
                int relativeY = cornerTileY - tile.Position.y;

                Vector3Int horizontalCornerNeighborPosition = new Vector3Int(cornerTileX + relativeX, cornerTileY, 0);
                Vector3Int verticalCornerNeighborPosition = new Vector3Int(cornerTileX, cornerTileY + relativeY, 0);

                Tile horizontalCornerNeighborTile = GetTile(horizontalCornerNeighborPosition);
                Tile verticalCornerNeighborTile = GetTile(verticalCornerNeighborPosition);

                if (horizontalCornerNeighborTile == null && verticalCornerNeighborTile == null) continue;
                return true;
            }
            #endregion
        }

        return false;
    }

    public CornerNeighborTiles GetCornerNeighborTiles(Tile tile)
    {
        List<Tile> cornerNeighbors = new List<Tile>();
        for (int x = tile.Position.x - 1; x <= tile.Position.x + 1; x++)
        {
            for (int y = tile.Position.y - 1; y <= tile.Position.y + 1; y++)
            {
                Vector3Int neighborTileGridPosition = new Vector3Int(x, y, 0);
                Tile neighborTile = GetTile(neighborTileGridPosition);

                if (neighborTile == null) continue;
                if (neighborTile.Position == tile.Position) continue;
                if (neighborTile.Position.x == tile.Position.x || neighborTile.Position.y == tile.Position.y) continue;
                if (neighborTile.BaseType == tile.BaseType) cornerNeighbors.Add(GetTile(neighborTileGridPosition));
            }
        }
        CornerNeighborTiles neighbors = new CornerNeighborTiles(tile.Position, cornerNeighbors);
        return neighbors;
    }
}
