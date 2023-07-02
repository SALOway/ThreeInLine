using System.Collections.Generic;

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
            bool tilesEqual = true;
            if (countEqual)
            {
                for (int i = 0; i < other.Tiles.Count; i++)
                {
                    bool hasEqualTile = false;
                    for (int j = 0; j < other.Tiles.Count; j++)
                    {
                        hasEqualTile = ReferenceEquals(Tiles[i], other.Tiles[i]);
                        if (hasEqualTile) break;
                    }
                    if (!hasEqualTile) tilesEqual = false;
                }
            }
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
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + Type.GetHashCode();
            foreach (var tile in Tiles)
            {
                hash = hash * 31 + tile.GetHashCode();
            }
            return hash;
        }
    }
}

public enum CombinationType
{
    Horizontal,
    Vertical,
}
