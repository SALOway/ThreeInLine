using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameBoard _board;
    [SerializeField] private Camera _mainCamera;
    List<Tile> _selectedTiles = new List<Tile>();

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (_selectedTiles.Count < 2)
        {
            Tile selectedTile = SelectTile();
            if (selectedTile != null)
            {
                Debug.Log("Select Tile");
                if (_selectedTiles.Contains(selectedTile))
                {
                    Debug.Log("Same Tile Selected. Reset Selection");
                    _selectedTiles.Clear();
                }
                else
                {
                    _selectedTiles.Add(selectedTile);
                }
            }
        }
        else
        {
            Debug.Log("Selected 2 Tiles");

            bool selectedTilesIsNeighbors = (_selectedTiles[0].Position - _selectedTiles[1].Position).magnitude != 1;
            if (selectedTilesIsNeighbors)
            {
                Debug.Log("Tiles Is Neighbors");

                _board.TileGrid.SwapTiles(_selectedTiles[0], _selectedTiles[1]);
                _selectedTiles[0].transform.position = _board.Grid.CellToWorld(_selectedTiles[0].Position);
                _selectedTiles[1].transform.position = _board.Grid.CellToWorld(_selectedTiles[1].Position);
                if (_board.FindAllCombinations().Count == 0)
                {
                    _board.TileGrid.SwapTiles(_selectedTiles[0], _selectedTiles[1]);
                    _selectedTiles[0].transform.position = _board.Grid.CellToWorld(_selectedTiles[0].Position);
                    _selectedTiles[1].transform.position = _board.Grid.CellToWorld(_selectedTiles[1].Position);
                }
                else
                {
                    Debug.Log("RemoveTiles");
                }
            }
            _selectedTiles.Clear();
        }
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

    private bool GetMouseClick(out Vector3 clickPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            return true;
        }
        clickPosition = Vector3.zero;
        return false;
    }

    private Tile SelectTile()
    {
        Tile selected = null;
        if (GetMouseClick(out Vector3 clickPosition))
        {
            Vector3Int gridClickPosition = _board.Grid.WorldToCell(clickPosition);
            selected = _board.TileGrid.GetTile(gridClickPosition);
        }
        return selected;
    }
}
