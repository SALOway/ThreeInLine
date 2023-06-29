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
        Tile[,] _tiles;
        int[,] _tilesTypes;
        List<List<Tile>> _expectedAllCombination;

        [SetUp]
        public void Setup()
        {
            _tiles = TestingUtilites.CreateTiles(3, 3);
            _tilesTypes = new int[,]
            {
                { 2, 3, 5 },
                { 2, 4, 3 },
                { 3, 3, 3 }
            };
            TestingUtilites.SetTilesTypes(_tiles, _tilesTypes);
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.SetTiles(_tiles);

            _expectedAllCombination = new List<List<Tile>>();

            List<Tile> combination = new List<Tile>()
            {
                _tiles[0,2],
                _tiles[1,2],
                _tiles[2,2]
            };
            _expectedAllCombination.Add(combination);
        }


        [Test]
        public void FindAllCombinations_WhenCalled_GetAllCombinationsOnGameBoard()
        {
            List<List<Tile>> allCombinations = _gameBoard.FindAllCombinations();

            Assert.AreEqual(_expectedAllCombination.Count, allCombinations.Count, "Invalid number of combinations");

            for (int i = 0; i < allCombinations.Count; i++)
            {
                List<Tile> expectedCombination = _expectedAllCombination[i];
                List<Tile> combination = allCombinations[i];
                for (int j = 0; j < combination.Count; j++)
                {
                    Assert.AreEqual(expectedCombination[j], combination[j], "Tile mismatch at index " + i);
                }
            }
        }
    }
}
