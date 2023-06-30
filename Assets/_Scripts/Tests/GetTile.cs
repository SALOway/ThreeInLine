using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class GetTile
    {
        GameBoard _gameBoard;
        Tile[,] _tiles;
        Vector3Int _tileGridPosition;

        [SetUp]
        public void Setup()
        {
            int[,] tileLayout = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
                { 10, 11, 12 },
            };
            _tiles = TestingUtilites.CreateTiles(tileLayout);
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.SetTiles(_tiles);
            _tileGridPosition = new Vector3Int(2, 1, 0);
        }

        [Test]
        public void GetTileAt_WhenCalled_GetTileAtGivenPositionInGrid()
        {
            Tile expectedTile = _tiles[_tileGridPosition.x, _tileGridPosition.y];
            Tile actualTile = _gameBoard.GetTileAt(_tileGridPosition);

            Assert.AreEqual(expectedTile.Type, actualTile.Type, $"Tyle types aren't equal. {expectedTile.Type} != {actualTile.Type}");

            Assert.AreEqual(expectedTile, actualTile, "Tiles aren't the same objects");
        }
    }
}