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
        public static Tile[,] CreateTiles(int[,] tileLayout)
        {
            int width = tileLayout.GetLength(1);
            int height = tileLayout.GetLength(0);

            Tile[,] tiles = new Tile[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject tileObject = new GameObject();
                    Tile tile = tileObject.AddComponent<Tile>();
                    tiles[x, y] = tile;
                    tile.BaseType = tileLayout[height - y - 1, x];
                    tile.Position = new Vector3Int(x, y, 0);
                }
            }
            return tiles;
        }
    }
}
