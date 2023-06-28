using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestingUtilites
    {
        public static GameBoard CreateGameBoard()
        {
            GameObject gameBoardObject = new GameObject();
            GameBoard gameBoard = gameBoardObject.AddComponent<GameBoard>();
            return gameBoard;
        }
        public static Tile[,] CreateTiles(int width, int height)
        {
            Tile[,] tiles = new Tile[width, height];

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    GameObject tileObject = new GameObject();
                    Tile tile = tileObject.AddComponent<Tile>();
                    tile.GridPosition = new Vector3Int(x, y, 0);
                    tiles[x, y] = tile;
                }
            }
            return tiles;
        }
    }
}
