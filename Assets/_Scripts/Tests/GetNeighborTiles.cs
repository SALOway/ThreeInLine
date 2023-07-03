using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GetNeighborTiles
    {
        GameBoard _gameBoard;
        Tile[,] _tiles;
        NeighborTiles _expectedNeighbors;
        Tile _baseTile;

        [SetUp]
        public void Setup()
        {
            int[,] tileLayout = new int[,]
            {
                { 3, 5, 2 },
                { 4, 3, 3 },
                { 5, 3, 3 },
            };

            Tile[,] tiles = TestingUtilites.CreateTiles(tileLayout);
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.SetTiles(tiles);

            _baseTile = _gameBoard.GetTileAt(new Vector3Int(1, 1, 0));
            List<Tile> horizontalTiles = new List<Tile>() 
            { 
                _gameBoard.GetTileAt(new Vector3Int(2, 1, 0)) 
            };
            List<Tile> verticalTiles = new List<Tile>()
            {
                _gameBoard.GetTileAt(new Vector3Int(1, 0, 0)),
            };
            List<Tile> cornerTiles = new List<Tile>()
            {
                _gameBoard.GetTileAt(new Vector3Int(2, 0, 0)),
                _gameBoard.GetTileAt(new Vector3Int(0, 2, 0)),
            };
            _expectedNeighbors = new NeighborTiles(_baseTile.GridPosition, horizontalTiles, verticalTiles, cornerTiles);
        }

        [Test]
        public void GetNeighborTilesAt_WhenCalled_ReturnNeighborTilesAt()
        {
            NeighborTiles neighborTiles = _gameBoard.GetNeighborTilesAt(_baseTile);

            HashSet<Tile> expectedHorizontalNeighbors = new HashSet<Tile>(_expectedNeighbors.Horizontal);
            HashSet<Tile> actualHorizontalNeighbors = new HashSet<Tile>(neighborTiles.Horizontal);
            Assert.IsTrue(expectedHorizontalNeighbors.SetEquals(actualHorizontalNeighbors), "Horizontal neighbor tiles aren't the same");

            HashSet<Tile> expectedVerticalNeighbors = new HashSet<Tile>(_expectedNeighbors.Vertical);
            HashSet<Tile> actualVerticalNeighbors = new HashSet<Tile>(neighborTiles.Vertical);
            Assert.IsTrue(expectedVerticalNeighbors.SetEquals(actualVerticalNeighbors), "Vertical neighbor tiles aren't the same");

            HashSet<Tile> expectedCornerNeighbors = new HashSet<Tile>(_expectedNeighbors.Corner);
            HashSet<Tile> actualCornerNeighbors = new HashSet<Tile>(neighborTiles.Corner);
            Assert.IsTrue(expectedCornerNeighbors.SetEquals(actualCornerNeighbors), "Corner neighbor tiles aren't the same");
        }
    }
}
