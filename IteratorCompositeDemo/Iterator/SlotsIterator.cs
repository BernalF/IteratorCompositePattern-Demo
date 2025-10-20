namespace IteratorCompositeDemo.Iterator;

/// <summary>
/// Iterator for slots games catalog (List-based)
/// Demonstrates Iterator pattern implementation for List collections
/// </summary>
public class SlotsIterator : IIterator<CasinoGame>
{
    private readonly List<CasinoGame> _games;
    private int _position;

    public SlotsIterator(List<CasinoGame> games) => _games = games;

    public bool HasNext() => _position < _games.Count;

    public CasinoGame Next()
    {
        if (!HasNext()) throw new InvalidOperationException("No more games available.");
        return _games[_position++];
    }
}
