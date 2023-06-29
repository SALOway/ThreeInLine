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
        Vector3Int firstTilePosition;
        Vector3Int secondTilePosition;


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

            firstTilePosition = new Vector3Int(1, 1, 0);
            secondTilePosition = new Vector3Int(0, 1, 0);
        }

        [Test]
        public void SwapTiles_WhenCalled_SwapsTilesInArray()
        {
            Tile firstTile = _gameBoard.GetTile(firstTilePosition);
            Tile secondTile = _gameBoard.GetTile(secondTilePosition);

            _gameBoard.SwapTiles(firstTile, secondTile);

            Assert.AreEqual(secondTile, _gameBoard.GetTile(firstTilePosition));
            Assert.AreEqual(firstTile, _gameBoard.GetTile(secondTilePosition));
        }

        [Test]
        public void SwapTilesAt_WhenCalled_SwapsTilesInArray()
        {
            Tile firstTile = _gameBoard.GetTile(firstTilePosition);
            Tile secondTile = _gameBoard.GetTile(secondTilePosition);

            _gameBoard.SwapTilesAt(firstTilePosition, secondTilePosition);

            Assert.AreEqual(secondTile, _gameBoard.GetTile(firstTilePosition));
            Assert.AreEqual(firstTile, _gameBoard.GetTile(secondTilePosition));
        }
    }
}
