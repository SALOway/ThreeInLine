using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CountAvailableMoves
    {
        GameBoard _gameBoard;
        List<Tile> _expectedTilesWithMoves;


        [SetUp]
        public void Setup()
        {
            int[,] tileLayout = new int[,]
            {
                { 1,  2,  3,  4,  5,  6,  7,  1,  8,  9,  1,  10, 1,  11 },
                { 1,  12, 13, 14, 15, 16, 17, 1,  18, 19, 1,  20, 21, 22 },
                { 23, 1,  24, 25, 26, 27, 1,  28, 29, 30, 31, 32, 1,  33 },
                { 34, 35, 1,  36, 37, 1,  38, 39, 40, 41, 1,  42, 1,  43 },
                { 44, 45, 1,  46, 47, 1,  48, 49, 50, 51, 52, 53, 54, 55 },
                { 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 },
                { 70, 71, 72, 1,  1,  73, 74, 75, 76, 77, 78, 79, 80, 81 },
                { 82, 83, 1,  84, 85, 1,  86, 87, 88, 89, 90, 91, 92, 93 },
                { 1,  1,  95, 96, 97, 98, 1,  1,  99, 2,  1,  3,  1,  1  },
                { 4,  5,  6,  7,  8,  9,  10, 11, 12, 13, 14, 15, 16, 17 },
                { 1,  18, 1,  19, 1,  20, 21, 22, 23, 1,  1,  24, 1,  25 },
                { 26, 1,  27, 28, 29, 1,  30, 31, 32, 33, 34, 35, 36, 37 },
                { 38, 39, 40, 41, 1,  42, 43, 1,  44, 45, 46, 47, 48, 49 },
                { 50, 1,  51, 52, 53, 54, 1,  55, 56, 57, 58, 59, 60, 61 },
                { 1,  62, 1,  63, 64, 65, 66, 1,  67, 68, 69, 70, 71, 72 },
            };

            Tile[,] tiles = TestingUtilites.CreateTiles(tileLayout);
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.SetTiles(tiles);

            _expectedTilesWithMoves = new List<Tile>()
            {
                _gameBoard.GetTile(new Vector3Int(1, 1, 0)),
                _gameBoard.GetTile(new Vector3Int(6, 1, 0)),
                _gameBoard.GetTile(new Vector3Int(1, 3, 0)),
                _gameBoard.GetTile(new Vector3Int(5, 3, 0)),
                _gameBoard.GetTile(new Vector3Int(12, 4, 0)),
                _gameBoard.GetTile(new Vector3Int(10, 6, 0)),
                _gameBoard.GetTile(new Vector3Int(2, 7, 0)),
                _gameBoard.GetTile(new Vector3Int(5, 7, 0)),
                _gameBoard.GetTile(new Vector3Int(10, 11, 0)),
                _gameBoard.GetTile(new Vector3Int(1, 12, 0)),
                _gameBoard.GetTile(new Vector3Int(6, 12, 0)),
                _gameBoard.GetTile(new Vector3Int(12, 14, 0)),
            };
        }

        [Test]
        public void CountAvailableMoves_WhenCalled_ReturnCountOfPlayerMoves()
        {
            List<Tile> tilesWithMoves = _gameBoard.FindAllTilesWithAvailableMoves();

            Assert.NotZero(tilesWithMoves.Count, "There is no moves");

            foreach (var expectedTile in _expectedTilesWithMoves)
            {
                // if (!tilesWithMoves.Contains(expectedTile)) Debug.Log($"Expect Tile At {expectedTile.Position} But Don't Find Any");
                Assert.Contains(expectedTile, tilesWithMoves, $"Expect Tile At {expectedTile.Position} But Don't Find Any");
            }

            foreach (var actualTile in tilesWithMoves)
            {
                if (!_expectedTilesWithMoves.Contains(actualTile)) Debug.Log($"Actual Tile At {actualTile.Position} Aren't Expected");
                /*
                Output:
                Actual Tile At (0, 0, 0) Aren't Expected
                Actual Tile At (0, 4, 0) Aren't Expected
                Actual Tile At (0, 13, 0) Aren't Expected
                Actual Tile At (1, 6, 0) Aren't Expected
                Actual Tile At (2, 0, 0) Aren't Expected
                Actual Tile At (2, 4, 0) Aren't Expected
                Actual Tile At (2, 11, 0) Aren't Expected
                Actual Tile At (3, 8, 0) Aren't Expected
                Actual Tile At (4, 2, 0) Aren't Expected
                Actual Tile At (4, 4, 0) Aren't Expected
                Actual Tile At (4, 8, 0) Aren't Expected
                Actual Tile At (5, 11, 0) Aren't Expected
                Actual Tile At (6, 6, 0) Aren't Expected
                Actual Tile At (7, 0, 0) Aren't Expected
                Actual Tile At (7, 2, 0) Aren't Expected
                Actual Tile At (7, 13, 0) Aren't Expected
                */

                // Assert.Contains(actualTile, _expectedTilesWithMoves, $"Actual Tile At {actualTile.Position} Aren't Expected");
            }



            Assert.AreEqual(_expectedTilesWithMoves.Count, tilesWithMoves.Count);
        }
    }
}
