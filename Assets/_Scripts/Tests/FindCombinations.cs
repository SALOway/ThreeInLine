using System.Collections.Generic;
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
            int[,] tileLayout = new int[,]
            {
                { 2, 3, 5 },
                { 2, 4, 3 },
                { 3, 3, 3 },
                { 5, 6, 2 },
            };

            Tile[,] tiles = TestingUtilites.CreateTiles(tileLayout);
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.SetTiles(tiles);

            #region Horizontal
            Tile[] tilesInHorizontalCombination = { tiles[0, 1], tiles[1, 1], tiles[2, 1] };
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

                Assert.AreEqual(expectedCombination.Count, combination.Count, $"Invalid horizontal combination length at index {i}");

                for (int j = 0; j < combination.Count; j++)
                {
                    int expectedType = expectedCombination.Tiles[j].Type;
                    int type = combination.Tiles[j].Type;
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

                Assert.AreEqual(expectedCombination.Count, combination.Count, $"Invalid vertical combination length at index {i}");

                for (int j = 0; j < combination.Count; j++)
                {
                    int expectedType = expectedCombination.Tiles[j].Type;
                    int type = combination.Tiles[j].Type;
                    Assert.AreEqual(expectedType, type, "Tile mismatch in vertical combination at index " + j);
                }
            }
            #endregion

        }
    }
}
