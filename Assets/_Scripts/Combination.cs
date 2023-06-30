using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination
{
    public IList<Tile> Tiles;
    public CombinationType Type;
    public int Count => Tiles.Count;

    public Combination(IList<Tile> tiles, CombinationType type)
    {
        Tiles = tiles;
        Type = type;
    }
}

public enum CombinationType
{
    Horizontal,
    Vertical,
}
