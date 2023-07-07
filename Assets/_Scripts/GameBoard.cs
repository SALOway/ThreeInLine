using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public TileGrid TileGrid;

    #region Combination functions
    public List<Combination> FindAllCombinations()
    {
        List<Combination> allCombinations = new List<Combination>();

        for (int columnIndex = 0; columnIndex < TileGrid.Width; columnIndex++)
        {
            allCombinations.AddRange(FindVerticalCombinationsAt(columnIndex));
        }

        for (int rowIndex = 0; rowIndex < TileGrid.Height; rowIndex++)
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
        for (int columnIndex = 0; columnIndex < TileGrid.Width; columnIndex++)
        {
            Vector3Int tilePosition = new Vector3Int(columnIndex, rowIndex, 0);
            Tile tile = TileGrid.GetTile(tilePosition);

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
        for (int rowIndex = 0; rowIndex < TileGrid.Height; rowIndex++)
        {
            Vector3Int tilePosition = new Vector3Int(columnIndex, rowIndex, 0);
            Tile tile = TileGrid.GetTile(tilePosition);
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

        for (int columnIndex = 0; columnIndex < TileGrid.Width; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < TileGrid.Height; rowIndex++)
            {
                Vector3Int tilePosition = new Vector3Int(columnIndex, rowIndex, 0);
                Tile tile = TileGrid.GetTile(tilePosition);
                if (tile == null) continue;
                if (CheckTileForMove(tile))
                {
                    tilesWithMoves.Add(tile);
                }
            }
        }

        return new List<Tile>(tilesWithMoves);
    }

    private bool CheckTileForMove(Tile baseTile)
    {
        #region Bottom
        List<Tile> bottomLeft = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.left),
            TileGrid.GetTile(baseTile.Position + Vector3Int.down * 2 + Vector3Int.left),
        };
        List<Tile> bottomCentral = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.down * 2),
            TileGrid.GetTile(baseTile.Position + Vector3Int.down * 3),
        };
        List<Tile> bottomRight = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.right),
            TileGrid.GetTile(baseTile.Position + Vector3Int.down * 2 + Vector3Int.right),
        };
        #endregion

        #region Top
        List<Tile> topLeft = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.left),
            TileGrid.GetTile(baseTile.Position + Vector3Int.up * 2 + Vector3Int.left),
        };
        List<Tile> topCentral = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.up * 2),
            TileGrid.GetTile(baseTile.Position + Vector3Int.up * 3),
        };
        List<Tile> topRight = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.right),
            TileGrid.GetTile(baseTile.Position + Vector3Int.up * 2 + Vector3Int.right),
        };
        #endregion

        #region Left
        List<Tile> leftBottom = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.left),
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.left * 2),
        };
        List<Tile> leftCentral = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.left * 2),
            TileGrid.GetTile(baseTile.Position + Vector3Int.left * 3),
        };
        List<Tile> leftTop = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.left),
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.left * 2),
        };
        #endregion

        #region Right
        List<Tile> rightBottom = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.right),
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.right * 2),
        };
        List<Tile> rightCentral = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.right * 2),
            TileGrid.GetTile(baseTile.Position + Vector3Int.right * 3),
        };
        List<Tile> rightTop = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.right),
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.right * 2),
        };
        #endregion

        #region Corners
        List<Tile> bottomCorners = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.left),
            TileGrid.GetTile(baseTile.Position + Vector3Int.down + Vector3Int.right),
        };

        List<Tile> topCorners = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.left),
            TileGrid.GetTile(baseTile.Position + Vector3Int.up + Vector3Int.right),
        };

        List<Tile> leftCorners = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.left + Vector3Int.up),
            TileGrid.GetTile(baseTile.Position + Vector3Int.left + Vector3Int.down),
        };

        List<Tile> rightCorners = new List<Tile>()
        {
            TileGrid.GetTile(baseTile.Position + Vector3Int.right + Vector3Int.up),
            TileGrid.GetTile(baseTile.Position + Vector3Int.right + Vector3Int.down),
        };
        #endregion

        List<List<Tile>> allTilesForCombination = new List<List<Tile>>()
        {
            bottomCentral,
            bottomLeft,
            bottomRight,
            topCentral,
            topLeft,
            topRight,
            rightCentral,
            rightBottom,
            rightTop,
            leftCentral,
            leftBottom,
            leftTop,
            bottomCorners,
            topCorners,
            leftCorners,
            rightCorners
        };

        foreach (var tilesForCombination in allTilesForCombination)
        {
            if (tilesForCombination[0] != null && tilesForCombination[1] != null)
            {
                if (baseTile.BaseType == tilesForCombination[0].BaseType && baseTile.BaseType == tilesForCombination[1].BaseType)
                {
                    return true;
                }
            }
            else
            {
                continue;
            }
        }

        return false;
    }

}
