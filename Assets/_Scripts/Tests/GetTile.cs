﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Tests
{
    public class GetTile
    {
        GameBoard _gameBoard;
        Tile[,] _tilesReference;
        Vector3Int _tileGridPosition;

        [SetUp]
        public void Setup()
        {
            _gameBoard = TestingUtilites.CreateGameBoard();
            _tilesReference = TestingUtilites.CreateTiles(3, 3);
            _tileGridPosition = new Vector3Int(2, 1, 0);
        }

        [Test]
        public void GetTile_WhenCalled_GetTileAtGivenPositionInGridSimplePasses()
        {
            _gameBoard.SetTiles(_tilesReference);

            Tile referenceTile = _tilesReference[_tileGridPosition.x, _tileGridPosition.y];
            Tile actualTile = _gameBoard.GetTile(_tileGridPosition);

            Assert.AreEqual(actualTile, referenceTile);
        }
    }
}