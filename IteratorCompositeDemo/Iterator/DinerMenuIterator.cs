namespace IteratorCompositeDemo.Iterator;

/// <summary>
/// Iterator for table games catalog (Array-based)
/// Demonstrates Iterator pattern implementation for Array collections
/// </summary>
public class TableGamesIterator : IIterator<CasinoGame>
{
    private readonly CasinoGame?[] _games;
    private readonly int _count;
    private int _position;

    public TableGamesIterator(CasinoGame?[] games, int count)
    {
        _games = games;
        _count = count;
    }

    public bool HasNext() => _position < _count;

    public CasinoGame Next()
    {
        if (!HasNext()) throw new InvalidOperationException("No more games available.");
        return _games[_position++]!;
    }
}
