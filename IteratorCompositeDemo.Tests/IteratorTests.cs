using IteratorCompositeDemo.Iterator;
using Xunit;

namespace IteratorCompositeDemo.Tests;

public class IteratorTests
{
    [Fact(DisplayName = "SlotsIterator should iterate through games in order when catalog has multiple games")]
    public void CreateIterator_CatalogWithMultipleGames_IteratesInOrder()
    {
        var catalog = new SlotsCatalog();
        catalog.AddGame(new CasinoGame("Book of Dead", "Egyptian slot", "Slots", 96.21m, 0.10m));
        catalog.AddGame(new CasinoGame("Starburst", "Space slot", "Slots", 96.09m, 0.20m));

        var it = catalog.CreateIterator();
        Assert.True(it.HasNext());
        var first = it.Next();
        Assert.Equal("Book of Dead", first.Name);

        Assert.True(it.HasNext());
        var second = it.Next();
        Assert.Equal("Starburst", second.Name);

        Assert.False(it.HasNext());
        Assert.Throws<InvalidOperationException>(() => it.Next());
    }

    [Fact(DisplayName = "TableGamesIterator should respect count and throw when attempting to read beyond capacity")]
    public void CreateIterator_CatalogWithLimitedCapacity_RespectsCountAndThrowsWhenDone()
    {
        var catalog = new TableGamesCatalog(2);
        catalog.AddGame(new CasinoGame("Blackjack", "21 card game", "Table", 99.28m, 1.0m));

        var it = catalog.CreateIterator();
        Assert.True(it.HasNext());
        _ = it.Next();
        Assert.False(it.HasNext());
        Assert.Throws<InvalidOperationException>(() => it.Next());
    }

    [Fact(DisplayName = "SlotsIterator should return no games when catalog is empty")]
    public void CreateIterator_EmptySlotsCatalog_ReturnsNoGames()
    {
        var catalog = new SlotsCatalog();
        var iterator = catalog.CreateIterator();

        Assert.False(iterator.HasNext());
        Assert.Throws<InvalidOperationException>(() => iterator.Next());
    }

    [Fact(DisplayName = "TableGamesIterator should return no games when catalog is empty")]
    public void CreateIterator_EmptyTableGamesCatalog_ReturnsNoGames()
    {
        var catalog = new TableGamesCatalog(5);
        var iterator = catalog.CreateIterator();

        Assert.False(iterator.HasNext());
        Assert.Throws<InvalidOperationException>(() => iterator.Next());
    }

    [Fact(DisplayName = "TableGamesCatalog should throw exception when attempting to add game beyond capacity")]
    public void AddGame_CatalogAtFullCapacity_ThrowsException()
    {
        var catalog = new TableGamesCatalog(1);
        catalog.AddGame(new CasinoGame("Blackjack", "21 card game", "Table", 99.28m, 1.0m));
        
        Assert.Throws<InvalidOperationException>(() => 
            catalog.AddGame(new CasinoGame("Roulette", "Wheel game", "Table", 97.30m, 0.50m)));
    }

    [Fact(DisplayName = "Multiple iterators on same SlotsCatalog should work independently")]
    public void CreateIterator_MultipleIteratorsOnSameCatalog_WorkIndependently()
    {
        var catalog = new SlotsCatalog();
        catalog.AddGame(new CasinoGame("Game1", "desc", "Slots", 96.0m, 0.10m));
        catalog.AddGame(new CasinoGame("Game2", "desc", "Slots", 97.0m, 0.20m));

        var iterator1 = catalog.CreateIterator();
        var iterator2 = catalog.CreateIterator();

        // Both iterators should work independently
        Assert.True(iterator1.HasNext());
        Assert.True(iterator2.HasNext());

        var game1FromIterator1 = iterator1.Next();
        Assert.Equal("Game1", game1FromIterator1.Name);

        // iterator2 should still be at the beginning
        Assert.True(iterator2.HasNext());
        var game1FromIterator2 = iterator2.Next();
        Assert.Equal("Game1", game1FromIterator2.Name);
    }
}
