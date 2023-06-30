using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class FindCombinations
    {
        GameBoard _gameBoard;
        List<Combination> _expectedCombinations;

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

            Tile[] tilesInHorizontalCombination = { tiles[0, 1], tiles[1, 1], tiles[2, 1] };
            Combination expectedHorizontalCombination = new Combination(tilesInHorizontalCombination , CombinationType.Horizontal);
            _expectedCombinations = new List<Combination>() { expectedHorizontalCombination };
        }


        [Test]
        public void FindAllCombinations_WhenCalled_GetAllCombinationsOnGameBoard()
        {
            List<Combination> allCombinations = _gameBoard.FindAllCombinations();

            Assert.AreEqual(_expectedCombinations.Count, allCombinations.Count, "Invalid number of combinations");

            for (int i = 0; i < allCombinations.Count; i++)
            {
                Combination expectedCombination = _expectedCombinations[i];
                Combination combination = allCombinations[i];

                Assert.AreEqual(expectedCombination.Count, combination.Count, $"Invalid combination length at index {i}");
                
                for (int j = 0; j < combination.Count; j++)
                {
                    int expectedType = expectedCombination.Tiles[j].Type;
                    int type = combination.Tiles[j].Type;
                    Assert.AreEqual(expectedType, type, "Tile mismatch at index " + j);
                }
            }
        }
    }
}
