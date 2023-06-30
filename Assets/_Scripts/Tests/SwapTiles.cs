using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SwapTiles
    {
        GameBoard _gameBoard;
        Vector3Int _firstTileGridPosition;
        Vector3Int _secondTileGridPosition;


        [SetUp]
        public void Setup()
        {
            int[,] tileLayout = new int[,]
            {
                { 2, 3, 5 },
                { 2, 4, 3 },
                { 3, 3, 3 }
            };
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.SetTiles(TestingUtilites.CreateTiles(tileLayout));

            _firstTileGridPosition = new Vector3Int(1, 1, 0);
            _secondTileGridPosition = new Vector3Int(0, 1, 0);
        }

        [Test]
        public void SwapTiles_WhenCalled_SwapsTilesInArray()
        {
            Tile firstTile = _gameBoard.GetTileAt(_firstTileGridPosition);
            Tile secondTile = _gameBoard.GetTileAt(_secondTileGridPosition);

            _gameBoard.SwapTiles(firstTile, secondTile);

            Assert.AreEqual(secondTile, _gameBoard.GetTileAt(_firstTileGridPosition));
            Assert.AreEqual(firstTile, _gameBoard.GetTileAt(_secondTileGridPosition));
        }

        [Test]
        public void SwapTilesAt_WhenCalled_SwapsTilesInArray()
        {
            Tile firstTile = _gameBoard.GetTileAt(_firstTileGridPosition);
            Tile secondTile = _gameBoard.GetTileAt(_secondTileGridPosition);

            _gameBoard.SwapTilesAt(_firstTileGridPosition, _secondTileGridPosition);

            Assert.AreEqual(secondTile, _gameBoard.GetTileAt(_firstTileGridPosition));
            Assert.AreEqual(firstTile, _gameBoard.GetTileAt(_secondTileGridPosition));
        }
    }
}
