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
    }
}
