using IteratorCompositeDemo.Composite;
using Xunit;

namespace IteratorCompositeDemo.Tests;

/// <summary>
/// Integration tests demonstrating the GameManager class usage
/// Tests the client code that uses both Iterator and Composite patterns
/// Applied to casino game management
/// </summary>
public class GameManagerTests
{
    [Fact(DisplayName = "GameManager ShowAllGames should execute without throwing exceptions when given valid game structure")]
    public void ShowAllGames_ValidGameStructure_DoesNotThrow()
    {
        // Arrange
        var allGames = CreateSampleGameStructure();
        var gameManager = new GameManager(allGames);

        // Act & Assert - Should not throw any exceptions
        var exception = Record.Exception(() => gameManager.ShowAllGames());
        Assert.Null(exception);
    }

    [Fact(DisplayName = "GameManager ShowHighRTPGames should execute without throwing exceptions when given valid game structure")]
    public void ShowHighRTPGames_ValidGameStructure_DoesNotThrow()
    {
        // Arrange  
        var allGames = CreateSampleGameStructure();
        var gameManager = new GameManager(allGames);

        // Act & Assert - Should not throw any exceptions
        var exception = Record.Exception(() => gameManager.ShowHighRtpGames());
        Assert.Null(exception);
    }

    [Fact(DisplayName = "GameManager ShowGamesByCategory should execute without throwing exceptions when given valid game structure")]
    public void ShowGamesByCategory_ValidGameStructure_DoesNotThrow()
    {
        // Arrange  
        var allGames = CreateSampleGameStructure();
        var gameManager = new GameManager(allGames);

        // Act & Assert - Should not throw any exceptions
        var exception = Record.Exception(() => gameManager.ShowGamesByCategory("Slots"));
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Game category iterator should correctly traverse structure with mixed category and game components")]
    public void CreateIterator_GameStructureWithMixedItems_IteratesCorrectly()
    {
        // Arrange
        var allGames = CreateSampleGameStructure();

        // Act
        var allItems = allGames.CreateIterator().ToList();

        // Assert
        Assert.True(allItems.Count > 0);
        Assert.Contains(allItems, item => item.Name == "VIRTUAL CASINO");
        Assert.Contains(allItems, item => 
        {
            try 
            { 
                return item.Rtp > 90m; 
            } 
            catch 
            { 
                return false; 
            }
        });
    }

    private static GameCategory CreateSampleGameStructure()
    {
        var allGames = new GameCategory("VIRTUAL CASINO", "All casino games");
        var slots = new GameCategory("SLOT MACHINES", "Video slot games");
        var tableGames = new GameCategory("TABLE GAMES", "Card and table games");

        allGames.Add(slots);
        allGames.Add(tableGames);

        slots.Add(new CasinoGame("Book of Dead", "Egyptian slot with free spins", "Slots", 96.21m, 0.10m));
        slots.Add(new CasinoGame("Starburst", "Space-themed slot with expanding wilds", "Slots", 96.09m, 0.20m));

        tableGames.Add(new CasinoGame("Classic Blackjack", "21 against the house", "Table", 99.28m, 1.0m));
        tableGames.Add(new CasinoGame("European Roulette", "Roulette with single zero", "Table", 97.30m, 0.50m));

        return allGames;
    }
}