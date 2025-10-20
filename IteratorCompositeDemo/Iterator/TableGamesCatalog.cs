namespace IteratorCompositeDemo.Iterator;

/// <summary>
/// Table games catalog that uses Array internally
/// Demonstrates Iterator pattern with Array-based storage
/// </summary>
public class TableGamesCatalog : IAggregate<CasinoGame>
{
    private readonly CasinoGame?[] _games;
    private int _count;

    public TableGamesCatalog(int capacity) => _games = new CasinoGame?[capacity];

    public void AddGame(CasinoGame game)
    {
        if (_count >= _games.Length) throw new InvalidOperationException("Catalog is full.");
        _games[_count++] = game;
    }

    public IIterator<CasinoGame> CreateIterator() => new TableGamesIterator(_games, _count);
}
