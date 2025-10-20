using IteratorCompositeDemo.Composite;
using Xunit;

namespace IteratorCompositeDemo.Tests;

public class CompositeIteratorTests
{
    [Fact(DisplayName = "CompositeIterator should traverse game hierarchy in depth-first order")]
    public void CreateIterator_GameHierarchyWithMultipleLevels_TraversesInDepthFirstOrder()
    {
        var root = new GameCategory("VIRTUAL CASINO", "All games");
        var c1 = new GameCategory("SLOTS", "Slot machines");
        var c2 = new GameCategory("TABLE GAMES", "Card and table games");
        root.Add(c1);
        root.Add(c2);
        c1.Add(new CasinoGame("Book of Dead", "Egyptian slot", "Slots", 96.21m, 0.10m));
        c2.Add(new CasinoGame("Blackjack", "21 card game", "Table", 99.28m, 1.0m));

        var names = root.CreateIterator().Select(c => c.Name).ToList();

        Assert.Equal(new[] { "VIRTUAL CASINO", "SLOTS", "Book of Dead", "TABLE GAMES", "Blackjack" }, names);
    }

    [Fact(DisplayName = "CompositeIterator should traverse nested game categories correctly maintaining depth-first order")]
    public void CreateIterator_GameCategoryWithNestedSubcategories_TraversesCorrectly()
    {
        var root = new GameCategory("VIRTUAL CASINO", "All casino games");
        var slots = new GameCategory("SLOT MACHINES", "Video slot games");
        var tableGames = new GameCategory("TABLE GAMES", "Card and table games");
        var promoGames = new GameCategory("PROMOTIONAL GAMES", "Special bonus games");

        root.Add(slots);
        root.Add(tableGames);
        slots.Add(promoGames); // Nest promotional games under slots

        slots.Add(new CasinoGame("Starburst", "Space-themed slot", "Slots", 96.09m, 0.20m));
        tableGames.Add(new CasinoGame("European Roulette", "Single zero roulette", "Table", 97.30m, 0.50m));
        promoGames.Add(new CasinoGame("Lucky Spin Bonus", "Daily free spins", "Promotional", 96.50m, 0.01m));

        var names = root.CreateIterator().Select(c => c.Name).ToArray();

        // Depth-first traversal: ROOT -> SLOTS -> PROMOTIONAL GAMES -> Lucky Spin Bonus -> Starburst -> TABLE GAMES -> European Roulette
        // The promotional games category gets processed before Starburst because it was added first to slots
        Assert.Equal(new[] { "VIRTUAL CASINO", "SLOT MACHINES", "PROMOTIONAL GAMES", "Lucky Spin Bonus", "Starburst", "TABLE GAMES", "European Roulette" }, names);
    }

    [Fact(DisplayName = "CompositeIterator should return only category name when category has no games")]
    public void CreateIterator_EmptyGameCategory_ReturnsOnlyCategoryName()
    {
        var emptyCategory = new GameCategory("EMPTY CATEGORY", "No games");
        
        var names = emptyCategory.CreateIterator().Select(c => c.Name).ToArray();
        
        Assert.Equal(new[] { "EMPTY CATEGORY" }, names);
    }

    [Fact(DisplayName = "CompositeIterator should return both category and game when category has single game")]
    public void CreateIterator_CategoryWithSingleGame_ReturnsBothCategoryAndGame()
    {
        var category = new GameCategory("SINGLE GAME CATEGORY", "One game category");
        category.Add(new CasinoGame("Lone Game", "The only game", "Slots", 95.0m, 0.10m));
        
        var names = category.CreateIterator().Select(c => c.Name).ToArray();
        
        Assert.Equal(new[] { "SINGLE GAME CATEGORY", "Lone Game" }, names);
    }

    [Fact(DisplayName = "CompositeIterator should handle complex nested structures with multiple levels")]
    public void CreateIterator_ComplexNestedStructure_TraversesCorrectly()
    {
        var casino = new GameCategory("MEGA CASINO", "Complete casino");
        var mainFloor = new GameCategory("MAIN FLOOR", "Primary gaming area");
        var vipSection = new GameCategory("VIP SECTION", "High roller area");
        var vipSlots = new GameCategory("VIP SLOTS", "Exclusive slots");

        casino.Add(mainFloor);
        casino.Add(vipSection);
        vipSection.Add(vipSlots);

        mainFloor.Add(new CasinoGame("Regular Blackjack", "Standard blackjack", "Table", 99.28m, 1.0m));
        vipSlots.Add(new CasinoGame("VIP Mega Jackpot", "Exclusive high stakes slot", "Slots", 97.80m, 100.0m));
        vipSection.Add(new CasinoGame("VIP Baccarat", "High limit baccarat", "Table", 98.94m, 50.0m));

        var names = casino.CreateIterator().Select(c => c.Name).ToArray();

        Assert.Equal(new[] { 
            "MEGA CASINO", 
            "MAIN FLOOR", 
            "Regular Blackjack", 
            "VIP SECTION", 
            "VIP SLOTS", 
            "VIP Mega Jackpot", 
            "VIP Baccarat" 
        }, names);
    }
}
