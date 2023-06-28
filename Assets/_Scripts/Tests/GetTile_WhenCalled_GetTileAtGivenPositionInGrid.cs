using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Tests
{
    public class GetTile_WhenCalled_GetTileAtGivenPositionInGrid
    {
        (int width, int height) _boardSize;
        GameBoard _gameBoard;
        Tile[,] _tiles;
        Vector3Int _tileGridPosition;

        [SetUp]
        public void Setup()
        {
            _boardSize = (3, 3);
            _gameBoard = CreateGameBoard();
            _tiles = CreateTiles(_boardSize.width, _boardSize.height);
            _tileGridPosition = new Vector3Int(2, 1, 0);
        }

        private GameBoard CreateGameBoard()
        {
            GameObject gameBoardObject = new GameObject();
            GameBoard gameBoard = gameBoardObject.AddComponent<GameBoard>();
            return gameBoard;
        }
        private Tile[,] CreateTiles(int width, int height)
        {
            Tile[,] tiles = new Tile[width, height];

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    GameObject tileObject = new GameObject();
                    Tile tile = tileObject.AddComponent<Tile>();
                    tiles[x, y] = tile;
                }
            }
            return tiles;
        }

        [Test]
        public void GetTile_WhenCalled_GetTileAtGivenPositionInGridSimplePasses()
        {
            _gameBoard.SetTiles(_tiles);

            Assert.AreEqual(_gameBoard.GetTile(_tileGridPosition), _tiles[_tileGridPosition.x, _tileGridPosition.y]);
        }
    }
}