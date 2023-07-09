using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameBoard _board;

    private void Start()
    {
        // InitializeGame();
    }

    public void InitializeGame()
    {
        int gridWidth = 5;
        int gridHeigth = 6;

        _board.CreateTileGrid(gridWidth, gridHeigth);

        List<Combination> allCombinations = _board.FindAllCombinations();
        List<Tile> allTilesWithMove = _board.FindAllTilesWithMove();

        while (allCombinations.Count > 0 || allTilesWithMove.Count == 0)
        {

            _board.CreateTileGrid(gridWidth, gridHeigth);

            allCombinations = _board.FindAllCombinations();
            allTilesWithMove = _board.FindAllTilesWithMove();
        }
    }
}
