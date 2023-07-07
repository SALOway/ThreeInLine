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
            TileBaseType[,] tileLayout = new TileBaseType[,]
            {
                { TileBaseType.Red,     TileBaseType.Yellow,    TileBaseType.Gray,      TileBaseType.Green,     TileBaseType.Magenta,   TileBaseType.Black,     TileBaseType.Green,     TileBaseType.Red,       TileBaseType.Yellow,    TileBaseType.Orange,    TileBaseType.Red,       TileBaseType.White,     TileBaseType.Red,       TileBaseType.Orange,  },
                { TileBaseType.Red,     TileBaseType.Black,     TileBaseType.Magenta,   TileBaseType.White,     TileBaseType.Black,     TileBaseType.Black,     TileBaseType.Gray,      TileBaseType.Red,       TileBaseType.Orange,    TileBaseType.Black,     TileBaseType.Red,       TileBaseType.White,     TileBaseType.Black,     TileBaseType.Black,   },
                { TileBaseType.Gray,    TileBaseType.Red,       TileBaseType.Gray,      TileBaseType.Yellow,    TileBaseType.Gray,      TileBaseType.Orange,    TileBaseType.Red,       TileBaseType.Blue,      TileBaseType.Black,     TileBaseType.Green,     TileBaseType.Green,     TileBaseType.Yellow,    TileBaseType.Red,       TileBaseType.Gray,    },
                { TileBaseType.Yellow,  TileBaseType.Green,     TileBaseType.Red,       TileBaseType.Orange,    TileBaseType.Magenta,   TileBaseType.Red,       TileBaseType.Gray,      TileBaseType.Yellow,    TileBaseType.Orange,    TileBaseType.Orange,    TileBaseType.Red,       TileBaseType.Magenta,   TileBaseType.Red,       TileBaseType.Gray,    },
                { TileBaseType.Blue,    TileBaseType.Black,     TileBaseType.Red,       TileBaseType.Green,     TileBaseType.Orange,    TileBaseType.Red,       TileBaseType.Magenta,   TileBaseType.Blue,      TileBaseType.Magenta,   TileBaseType.Blue,      TileBaseType.Green,     TileBaseType.Black,     TileBaseType.Black,     TileBaseType.White,   },
                { TileBaseType.Gray,    TileBaseType.Magenta,   TileBaseType.Orange,    TileBaseType.Black,     TileBaseType.Blue,      TileBaseType.Yellow,    TileBaseType.Green,     TileBaseType.Black,     TileBaseType.Orange,    TileBaseType.White,     TileBaseType.Gray,      TileBaseType.Blue,      TileBaseType.Gray,      TileBaseType.White,   },
                { TileBaseType.Black,   TileBaseType.Orange,    TileBaseType.Green,     TileBaseType.Red,       TileBaseType.Red,       TileBaseType.Black,     TileBaseType.Magenta,   TileBaseType.Gray,      TileBaseType.Magenta,   TileBaseType.Green,     TileBaseType.Black,     TileBaseType.Orange,    TileBaseType.Yellow,    TileBaseType.Black,   },
                { TileBaseType.Green,   TileBaseType.White,     TileBaseType.Red,       TileBaseType.Magenta,   TileBaseType.Magenta,   TileBaseType.Red,       TileBaseType.Orange,    TileBaseType.Black,     TileBaseType.Orange,    TileBaseType.Magenta,   TileBaseType.White,     TileBaseType.Green,     TileBaseType.Magenta,   TileBaseType.Magenta, },
                { TileBaseType.Red,     TileBaseType.Red,       TileBaseType.Green,     TileBaseType.Blue,      TileBaseType.Gray,      TileBaseType.Blue,      TileBaseType.Red,       TileBaseType.Red,       TileBaseType.Yellow,    TileBaseType.Black,     TileBaseType.Red,       TileBaseType.Blue,      TileBaseType.Red,       TileBaseType.Red,     },
                { TileBaseType.Orange,  TileBaseType.White,     TileBaseType.Yellow,    TileBaseType.Green,     TileBaseType.Orange,    TileBaseType.Blue,      TileBaseType.Yellow,    TileBaseType.Magenta,   TileBaseType.Blue,      TileBaseType.Black,     TileBaseType.Gray,      TileBaseType.White,     TileBaseType.Green,     TileBaseType.White,   },
                { TileBaseType.Red,     TileBaseType.Gray,      TileBaseType.Red,       TileBaseType.Black,     TileBaseType.Red,       TileBaseType.Yellow,    TileBaseType.Magenta,   TileBaseType.Green,     TileBaseType.Gray,      TileBaseType.Red,       TileBaseType.Red,       TileBaseType.Magenta,   TileBaseType.Red,       TileBaseType.Blue,    },
                { TileBaseType.Black,   TileBaseType.Red,       TileBaseType.Orange,    TileBaseType.Gray,      TileBaseType.Green,     TileBaseType.Red,       TileBaseType.Blue,      TileBaseType.Black,     TileBaseType.Black,     TileBaseType.Yellow,    TileBaseType.Gray,      TileBaseType.White,     TileBaseType.Black,     TileBaseType.Black,   },
                { TileBaseType.Yellow,  TileBaseType.Yellow,    TileBaseType.Magenta,   TileBaseType.White,     TileBaseType.Red,       TileBaseType.Orange,    TileBaseType.Gray,      TileBaseType.Red,       TileBaseType.Green,     TileBaseType.Orange,    TileBaseType.Magenta,   TileBaseType.Green,     TileBaseType.Orange,    TileBaseType.Black,   },
                { TileBaseType.Magenta, TileBaseType.Red,       TileBaseType.Yellow,    TileBaseType.White,     TileBaseType.White,     TileBaseType.Orange,    TileBaseType.Red,       TileBaseType.White,     TileBaseType.Gray,      TileBaseType.Blue,      TileBaseType.White,     TileBaseType.Black,     TileBaseType.Gray,      TileBaseType.White,   },
                { TileBaseType.Red,     TileBaseType.White,     TileBaseType.Red,       TileBaseType.Orange,    TileBaseType.Green,     TileBaseType.Black,     TileBaseType.Blue,      TileBaseType.Red,       TileBaseType.Magenta,   TileBaseType.Green,     TileBaseType.Orange,    TileBaseType.Black,     TileBaseType.Yellow,    TileBaseType.Blue,    },
            };

            _gameBoard = TestingUtilites.CreateGameBoard();
            _gameBoard.TileGrid = new TileGrid(tileLayout);

            _expectedTilesWithMoves = new List<Tile>()
            {
                _gameBoard.TileGrid.GetTile(new Vector3Int(1, 1, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(6, 1, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(1, 3, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(5, 3, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(12, 4, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(10, 6, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(2, 7, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(5, 7, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(10, 11, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(1, 12, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(6, 12, 0)),
                _gameBoard.TileGrid.GetTile(new Vector3Int(12, 14, 0)),
            };
        }
/*
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

                // Assert.Contains(actualTile, _expectedTilesWithMoves, $"Actual Tile At {actualTile.Position} Aren't Expected");
            }



            Assert.AreEqual(_expectedTilesWithMoves.Count, tilesWithMoves.Count);
        }
*/
    }
}
