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
                { TileBaseType.Red,     TileBaseType.Yellow,  TileBaseType.Yellow,  TileBaseType.Green,   TileBaseType.Magenta, TileBaseType.Yellow, TileBaseType.Green,   TileBaseType.Red,     TileBaseType.Yellow,  TileBaseType.Orange,  TileBaseType.Red,     TileBaseType.White,   TileBaseType.Red,     TileBaseType.Orange,  },
                { TileBaseType.Red,     TileBaseType.Yellow,  TileBaseType.Magenta, TileBaseType.White,   TileBaseType.Yellow,  TileBaseType.Yellow, TileBaseType.Yellow,  TileBaseType.Red,     TileBaseType.Orange,  TileBaseType.Blue,    TileBaseType.Red,     TileBaseType.White,   TileBaseType.Orange,  TileBaseType.Green,   },
                { TileBaseType.Magenta, TileBaseType.Red,     TileBaseType.Orange,  TileBaseType.Yellow,  TileBaseType.Yellow,  TileBaseType.Orange, TileBaseType.Red,     TileBaseType.Blue,    TileBaseType.Yellow,  TileBaseType.Green,   TileBaseType.Green,   TileBaseType.Yellow,  TileBaseType.Red,     TileBaseType.Yellow,  },
                { TileBaseType.Yellow,  TileBaseType.Green,   TileBaseType.Red,     TileBaseType.Orange,  TileBaseType.Magenta, TileBaseType.Red,    TileBaseType.Yellow,  TileBaseType.Yellow,  TileBaseType.Orange,  TileBaseType.Orange,  TileBaseType.Red,     TileBaseType.Magenta, TileBaseType.Red,     TileBaseType.Orange,  },
                { TileBaseType.Blue,    TileBaseType.Yellow,  TileBaseType.Red,     TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.Red,    TileBaseType.Magenta, TileBaseType.Blue,    TileBaseType.Magenta, TileBaseType.Blue,    TileBaseType.Green,   TileBaseType.White,   TileBaseType.Orange,  TileBaseType.White,   },
                { TileBaseType.Orange,  TileBaseType.Magenta, TileBaseType.Orange,  TileBaseType.Yellow,  TileBaseType.Blue,    TileBaseType.Yellow, TileBaseType.Green,   TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.White,   TileBaseType.Blue,    TileBaseType.Blue,    TileBaseType.Green,   TileBaseType.White,   },
                { TileBaseType.Yellow,  TileBaseType.Orange,  TileBaseType.Green,   TileBaseType.Red,     TileBaseType.Red,     TileBaseType.Yellow, TileBaseType.Magenta, TileBaseType.Yellow,  TileBaseType.Magenta, TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.Orange,  TileBaseType.Yellow,  TileBaseType.Orange,  },
                { TileBaseType.Green,   TileBaseType.White,   TileBaseType.Red,     TileBaseType.Magenta, TileBaseType.Magenta, TileBaseType.Red,    TileBaseType.Orange,  TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.Magenta, TileBaseType.White,   TileBaseType.Green,   TileBaseType.Magenta, TileBaseType.Magenta, },
                { TileBaseType.Red,     TileBaseType.Red,     TileBaseType.Green,   TileBaseType.Blue,    TileBaseType.Yellow,  TileBaseType.Blue,   TileBaseType.Red,     TileBaseType.Red,     TileBaseType.Yellow,  TileBaseType.Yellow,  TileBaseType.Red,     TileBaseType.Blue,    TileBaseType.Red,     TileBaseType.Red,     },
                { TileBaseType.Orange,  TileBaseType.White,   TileBaseType.Yellow,  TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.Blue,   TileBaseType.Yellow,  TileBaseType.Magenta, TileBaseType.Blue,    TileBaseType.Blue,    TileBaseType.Blue,    TileBaseType.White,   TileBaseType.Green,   TileBaseType.White,   },
                { TileBaseType.Red,     TileBaseType.Yellow,  TileBaseType.Red,     TileBaseType.Orange,  TileBaseType.Red,     TileBaseType.Yellow, TileBaseType.Magenta, TileBaseType.Green,   TileBaseType.Magenta, TileBaseType.Red,     TileBaseType.Red,     TileBaseType.Magenta, TileBaseType.Red,     TileBaseType.Blue,    },
                { TileBaseType.Magenta, TileBaseType.Red,     TileBaseType.Orange,  TileBaseType.Yellow,  TileBaseType.Green,   TileBaseType.Red,    TileBaseType.Blue,    TileBaseType.Yellow,  TileBaseType.Orange,  TileBaseType.Yellow,  TileBaseType.White,   TileBaseType.White,   TileBaseType.Yellow,  TileBaseType.Green,   },
                { TileBaseType.Yellow,  TileBaseType.White,   TileBaseType.Magenta, TileBaseType.White,   TileBaseType.Red,     TileBaseType.Orange, TileBaseType.Green,   TileBaseType.Red,     TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.Magenta, TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.Orange,  },
                { TileBaseType.Magenta, TileBaseType.Red,     TileBaseType.Yellow,  TileBaseType.White,   TileBaseType.White,   TileBaseType.Orange, TileBaseType.Red,     TileBaseType.White,   TileBaseType.Orange,  TileBaseType.Blue,    TileBaseType.White,   TileBaseType.Blue,    TileBaseType.Green,   TileBaseType.White,   },
                { TileBaseType.Red,     TileBaseType.White,   TileBaseType.Red,     TileBaseType.Orange,  TileBaseType.Green,   TileBaseType.Yellow, TileBaseType.Blue,    TileBaseType.Red,     TileBaseType.Magenta, TileBaseType.Green,   TileBaseType.Orange,  TileBaseType.White,   TileBaseType.Yellow,  TileBaseType.Blue,    },
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

        [Test]
        public void CountAvailableMoves_WhenCalled_ReturnCountOfPlayerMoves()
        {
            List<Tile> tilesWithMoves = _gameBoard.FindAllTilesWithMove();

            Assert.NotZero(tilesWithMoves.Count, "There is no moves");

            foreach (var expectedTile in _expectedTilesWithMoves)
            {
                // if (!tilesWithMoves.Contains(expectedTile)) Debug.Log($"Expect Tile At {expectedTile.Position} But Don't Find Any");
                Assert.Contains(expectedTile, tilesWithMoves, $"Expect Tile At {expectedTile.Position} But Don't Find Any");
            }

            foreach (var actualTile in tilesWithMoves)
            {
                // if (!_expectedTilesWithMoves.Contains(actualTile)) Debug.Log($"Actual Tile At {actualTile.Position} Aren't Expected");

                Assert.Contains(actualTile, _expectedTilesWithMoves, $"Actual Tile At {actualTile.Position} Aren't Expected");
            }

            Assert.AreEqual(_expectedTilesWithMoves.Count, tilesWithMoves.Count);
        }
    }
}
