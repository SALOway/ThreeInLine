using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FindCombinations
    {
        GameBoard _gameBoard;
        List<List<int>> _expectedAllCombinations;

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

            _expectedAllCombinations = new List<List<int>>()
            {
                new List<int>()
                {
                    tileLayout[2,0], tileLayout[2,1], tileLayout[2,2]
                }
            };
        }


        [Test]
        public void FindAllCombinations_WhenCalled_GetAllCombinationsOnGameBoard()
        {
            List<List<Tile>> allCombinations = _gameBoard.FindAllCombinations();

            Assert.AreEqual(_expectedAllCombinations.Count, allCombinations.Count, "Invalid number of combinations");

            for (int i = 0; i < allCombinations.Count; i++)
            {
                List<int> expectedCombination = _expectedAllCombinations[i];
                List<Tile> combination = allCombinations[i];

                Assert.AreEqual(expectedCombination.Count, combination.Count, $"Invalid combination length at index {i}");
                
                for (int j = 0; j < combination.Count; j++)
                {
                    int expectedType = expectedCombination[j];
                    int type = combination[j].Type;
                    Assert.AreEqual(expectedType, type, "Tile mismatch at index " + j);
                }
            }
        }
    }
}
