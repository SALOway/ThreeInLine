using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for controlling the game process
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] GameBoard _board;

    private void Start()
    {
        // InitializeGame();
    }

    public void InitializeGame()
    {
        _board.TileGrid = new TileGrid(5, 6);

        List<Combination> allCombinations = _board.FindAllCombinations();
        List<Tile> allTilesWithMove = _board.FindAllTilesWithMove();

        while (allCombinations.Count > 0 || allTilesWithMove.Count == 0)
        {
            _board.TileGrid = new TileGrid(5, 6);

            allCombinations = _board.FindAllCombinations();
            allTilesWithMove = _board.FindAllTilesWithMove();
        }
    }
}
