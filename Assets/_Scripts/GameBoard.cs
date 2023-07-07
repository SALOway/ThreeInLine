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
/*
    public List<Tile> FindAllTilesWithAvailableMoves()
    {

    }

    private bool CheckTileForMove(Tile tile)
    {
        
    }
*/
}
