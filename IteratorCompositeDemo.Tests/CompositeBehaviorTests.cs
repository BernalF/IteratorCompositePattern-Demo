using IteratorCompositeDemo.Composite;
using Xunit;

namespace IteratorCompositeDemo.Tests;

public class CompositeBehaviorTests
{
    [Fact(DisplayName = "CasinoGame Add operation should throw NotSupportedException when called on leaf node")]
    public void Add_CasinoGameLeafNode_ThrowsNotSupported()
    {
        var leaf = new CasinoGame("Blackjack", "Card game", "Table", 99.28m, 1.0m);
        Assert.Throws<NotSupportedException>(() => leaf.Add(new CasinoGame("Roulette", "Wheel game", "Table", 97.30m, 0.50m)));
    }

    [Fact(DisplayName = "Iterator should filter high RTP games correctly when traversing game structure")]
    public void CreateIterator_GameCategoryWithMixedRTPGames_FiltersHighRTPCorrectly()
    {
        var root = new GameCategory("VIRTUAL CASINO", "All games");
        var slots = new GameCategory("SLOT MACHINES", "Video slots");
        root.Add(slots);
        slots.Add(new CasinoGame("High RTP Slot", "Great returns", "Slots", 98.5m, 0.10m));
        slots.Add(new CasinoGame("Low RTP Slot", "Standard returns", "Slots", 92.0m, 0.20m));

        var highRtpNames = root.CreateIterator()
            .Where(game => {
                try 
                { 
                    return game.Rtp > 97.0m; 
                }
                catch (NotSupportedException) 
                { 
                    return false; // Skip category headers that don't have RTP property
                }
            })
            .Select(g => g.Name)
            .ToArray();

        Assert.Equal(new[] { "High RTP Slot" }, highRtpNames);
    }

    [Fact(DisplayName = "GameCategory should allow adding and removing game components")]
    public void AddAndRemove_GameCategory_ManagesComponentsCorrectly()
    {
        var category = new GameCategory("TABLE GAMES", "Card and table games");
        var game1 = new CasinoGame("Blackjack", "21 card game", "Table", 99.28m, 1.0m);
        var game2 = new CasinoGame("Roulette", "Wheel game", "Table", 97.30m, 0.50m);

        // Add games
        category.Add(game1);
        category.Add(game2);

        // Verify they were added
        Assert.Equal(game1, category.GetChild(0));
        Assert.Equal(game2, category.GetChild(1));

        // Remove one game
        category.Remove(game1);

        // Verify removal
        Assert.Equal(game2, category.GetChild(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => category.GetChild(1));
    }

    [Fact(DisplayName = "GameCategory GetChild should throw ArgumentOutOfRangeException for invalid index")]
    public void GetChild_InvalidIndex_ThrowsArgumentOutOfRangeException()
    {
        var category = new GameCategory("EMPTY CATEGORY", "No games");
        
        Assert.Throws<ArgumentOutOfRangeException>(() => category.GetChild(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => category.GetChild(-1));
    }
}
