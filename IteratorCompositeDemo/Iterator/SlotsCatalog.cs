namespace IteratorCompositeDemo.Iterator;

/// <summary>
/// Slots games catalog that uses List internally
/// Demonstrates Iterator pattern with List-based storage
/// </summary>
public class SlotsCatalog : IAggregate<CasinoGame>
{
    private readonly List<CasinoGame> _games = [];

    public void AddGame(CasinoGame game) => _games.Add(game);

    public IIterator<CasinoGame> CreateIterator() => new SlotsIterator(_games);
}
