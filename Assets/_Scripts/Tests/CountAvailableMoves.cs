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
        Tile[,] _tiles;
        int _expectedtilesWithMovesCount;


        [SetUp]
        public void Setup()
        {
            int[,] tileLayout = new int[,]
            {
                { 2, 3, 5, 3 },
                { 2, 5, 3, 2 },
                { 5, 2, 5, 5 },
                { 1, 3, 1, 2 },
                { 1, 1, 2, 2 },
            };

            Tile[,] tiles = TestingUtilites.CreateTiles(tileLayout);
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.SetTiles(tiles);

            _expectedtilesWithMovesCount = 7;
        }

        [Test]
        public void CountAvailableMoves_WhenCalled_ReturnCountOfPlayerMoves()
        {
            int tilesWithMovesCount = _gameBoard.CountAllTilesWithAvailableMoves();

            Assert.AreEqual(_expectedtilesWithMovesCount, tilesWithMovesCount);
        }
    }
}
