using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class GetTile
    {
        GameBoard _gameBoard;
        Vector3Int _tileGridPosition;

        [SetUp]
        public void Setup()
        {
            TileBaseType[,] tileLayout = new TileBaseType[,]
            {
                { TileBaseType.Orange,  TileBaseType.Red    },
                { TileBaseType.Magenta, TileBaseType.Yellow },
                { TileBaseType.Blue,    TileBaseType.Green  },
            };
            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.TileGrid = new TileGrid(tileLayout);
            _tileGridPosition = new Vector3Int(1, 1, 0);
        }

        [Test]
        public void GetTileAt_WhenCalled_GetTileAtGivenPositionInGrid()
        {
            TileBaseType expectedTileBaseType = TileBaseType.Yellow;
            TileBaseType actualTileBaseType = _gameBoard.TileGrid.GetTile(_tileGridPosition).BaseType;

            Assert.AreEqual(expectedTileBaseType, actualTileBaseType, $"Tyle types aren't equal. {expectedTileBaseType} != {actualTileBaseType}");
        }
    }
}