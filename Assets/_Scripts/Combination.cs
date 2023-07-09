using System.Collections.Generic;
using System.Linq;

public class Combination
{
    public IReadOnlyList<Tile> Tiles;
    public readonly CombinationType Type;

    public Combination(IReadOnlyList<Tile> tiles, CombinationType type)
    {
        Tiles = tiles;
        Type = type;
    }

    public override bool Equals(object obj)
    {
        if (obj is Combination other)
        {
            bool typesEqual = Type == other.Type;
            bool countEqual = Tiles.Count == other.Tiles.Count;
            bool tilesEqual = Tiles.SequenceEqual(other.Tiles);
            return typesEqual && countEqual && tilesEqual;
        }

        return false;
    }
    public static bool operator ==(Combination left, Combination right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(Combination left, Combination right)
    {
        return !Equals(left, right);
    }
}

public enum CombinationType
{
    Horizontal,
    Vertical,
}
