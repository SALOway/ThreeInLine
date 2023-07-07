using NUnit.Framework;
using UnityEngine;

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
            TileBaseType[,] tileLayout = new TileBaseType[,]
            {
                { TileBaseType.Red,   TileBaseType.Green, TileBaseType.Blue  },
                { TileBaseType.Red,   TileBaseType.Blue,  TileBaseType.Green },
                { TileBaseType.Green, TileBaseType.Green, TileBaseType.Green }
            };
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.TileGrid = new TileGrid(tileLayout);

            _firstTileGridPosition = new Vector3Int(1, 1, 0);
            _secondTileGridPosition = new Vector3Int(0, 1, 0);
        }

        [Test]
        public void SwapTiles_WhenCalled_SwapsTilesInArray()
        {
            Tile firstTile = _gameBoard.TileGrid.GetTile(_firstTileGridPosition);
            Tile secondTile = _gameBoard.TileGrid.GetTile(_secondTileGridPosition);

            _gameBoard.TileGrid.SwapTiles(firstTile, secondTile);

            Assert.AreEqual(secondTile, _gameBoard.TileGrid.GetTile(_firstTileGridPosition));
            Assert.AreEqual(firstTile, _gameBoard.TileGrid.GetTile(_secondTileGridPosition));
        }
    }
}
