using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    public class FindCombinations
    {
        GameBoard _gameBoard;
        List<Combination> _expectedHorizontalCombinations;
        List<Combination> _expectedVerticalCombinations;

        [SetUp]
        public void Setup()
        {
            TileBaseType[,] tileLayout = new TileBaseType[,]
            {
                { TileBaseType.Red,   TileBaseType.Green, TileBaseType.Blue },
                { TileBaseType.Red,   TileBaseType.Blue,  TileBaseType.Green },
                { TileBaseType.Green, TileBaseType.Green, TileBaseType.Green },
                { TileBaseType.Blue,  TileBaseType.Red,   TileBaseType.Blue },
            };
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.CreateTileGrid(tileLayout);

            #region Horizontal
            Tile[] tilesInHorizontalCombination = new Tile[] 
            {
                _gameBoard.TileGrid.GetTile(new Vector3Int(0, 1, 0)), 
                _gameBoard.TileGrid.GetTile(new Vector3Int(1, 1, 0)), 
                _gameBoard.TileGrid.GetTile(new Vector3Int(2, 1, 0)),
            };
            Combination expectedHorizontalCombination = new Combination(tilesInHorizontalCombination, CombinationType.Horizontal);
            _expectedHorizontalCombinations = new List<Combination>() { expectedHorizontalCombination };
            #endregion

            #region Vertical
            _expectedVerticalCombinations = new List<Combination>();
            #endregion

        }

        [Test]
        public void FindAllCombinations_WhenCalled_GetAllCombinationsOnGameBoard()
        {
            List<Combination> allCombinations = _gameBoard.FindAllCombinations();

            #region Horizontal Combinations
            List<Combination> horizontalCombinations = new List<Combination>();
            foreach (var combination in allCombinations)
            {
                if (combination.Type == CombinationType.Horizontal) horizontalCombinations.Add(combination);
            }

            Assert.AreEqual(_expectedHorizontalCombinations.Count, horizontalCombinations.Count, "Invalid number of horizontal combinations");

            for (int i = 0; i < horizontalCombinations.Count; i++)
            {
                Combination expectedCombination = _expectedHorizontalCombinations[i];
                Combination combination = horizontalCombinations[i];

                Assert.AreEqual(expectedCombination.Tiles.Count, combination.Tiles.Count, $"Invalid horizontal combination length at index {i}");

                for (int j = 0; j < combination.Tiles.Count; j++)
                {
                    TileBaseType expectedType = expectedCombination.Tiles[j].BaseType;
                    TileBaseType type = combination.Tiles[j].BaseType;
                    Assert.AreEqual(expectedType, type, "Tile mismatch in horizontal combination at index " + j);
                }
            }
            #endregion

            #region Vertical Combinations
            List<Combination> verticalCombinations = new List<Combination>();
            foreach (var combination in allCombinations)
            {
                if (combination.Type == CombinationType.Vertical) verticalCombinations.Add(combination);
            }

            Assert.AreEqual(_expectedVerticalCombinations.Count, verticalCombinations.Count, "Invalid number of vertical combinations");

            for (int i = 0; i < verticalCombinations.Count; i++)
            {
                Combination expectedCombination = _expectedHorizontalCombinations[i];
                Combination combination = verticalCombinations[i];

                Assert.AreEqual(expectedCombination.Tiles.Count, combination.Tiles.Count, $"Invalid vertical combination length at index {i}");

                for (int j = 0; j < combination.Tiles.Count; j++)
                {
                    TileBaseType expectedType = expectedCombination.Tiles[j].BaseType;
                    TileBaseType type = combination.Tiles[j].BaseType;
                    Assert.AreEqual(expectedType, type, "Tile mismatch in vertical combination at index " + j);
                }
            }
            #endregion

        }
    }
}
