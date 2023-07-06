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
        CornerNeighborTiles _expectedNeighbors;
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

            _baseTile = _gameBoard.GetTile(new Vector3Int(1, 1, 0));
            List<Tile> horizontalTiles = new List<Tile>() 
            { 
                _gameBoard.GetTile(new Vector3Int(2, 1, 0)) 
            };
            List<Tile> verticalTiles = new List<Tile>()
            {
                _gameBoard.GetTile(new Vector3Int(1, 0, 0)),
            };
            List<Tile> cornerTiles = new List<Tile>()
            {
                _gameBoard.GetTile(new Vector3Int(2, 0, 0)),
                _gameBoard.GetTile(new Vector3Int(0, 2, 0)),
            };
            _expectedNeighbors = new CornerNeighborTiles(_baseTile.Position, cornerTiles);
        }

        [Test]
        public void GetCornerNeighborTiles_WhenCalled_ReturnNeighborTilesAt()
        {
            CornerNeighborTiles neighborTiles = _gameBoard.GetCornerNeighborTiles(_baseTile);

            HashSet<Tile> expectedCornerNeighbors = new HashSet<Tile>(_expectedNeighbors.Tiles);
            HashSet<Tile> actualCornerNeighbors = new HashSet<Tile>(neighborTiles.Tiles);

            foreach (var expectedTile in _expectedNeighbors.Tiles)
            {
                Debug.Log($"[E]Tile({expectedTile.Position.x},{expectedTile.Position.y}) Type: {expectedTile.BaseType}");
            }
            foreach (var actualTile in neighborTiles.Tiles)
            {
                Debug.Log($"[A]Tile({actualTile.Position.x},{actualTile.Position.y}) Type: {actualTile.BaseType}");
            }

            Assert.IsTrue(expectedCornerNeighbors.SetEquals(actualCornerNeighbors), "Corner neighbor tiles aren't the same");
        }
    }
}
