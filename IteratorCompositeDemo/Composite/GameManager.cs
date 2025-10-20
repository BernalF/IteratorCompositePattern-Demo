using System.Text;

namespace IteratorCompositeDemo.Composite;

/// <summary>
/// GameManager class - the client in the Composite pattern
/// From Head First Design Patterns: Demonstrates how client code can treat 
/// individual objects and compositions uniformly
/// Applied to casino game management
/// </summary>
public class GameManager
{
    private readonly GameComponent _allGames;

    public GameManager(GameComponent allGames)
    {
        _allGames = allGames;
    }

    /// <summary>
    /// Displays the entire game catalog using the composite pattern
    /// </summary>
    public void ShowAllGames()
    {
        _allGames.Display();
    }

    /// <summary>
    /// Shows only high RTP games using the iterator
    /// Demonstrates how the iterator pattern works with the composite pattern
    /// </summary>
    public void ShowHighRtpGames()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("\n🎯 HIGH RTP GAMES (>97%)\n══════════════════════════════════");
        
        foreach (var gameComponent in _allGames.CreateIterator())
        {
            try
            {
                if (gameComponent.Rtp > 97.0m)
                {
                    Console.Write($"  ⭐ {gameComponent.Name}");
                    Console.WriteLine($" - RTP: {gameComponent.Rtp:F2}% | Min: ${gameComponent.MinBet:F2}");
                    Console.WriteLine($"       {gameComponent.Description}");
                }
            }
            catch (NotSupportedException)
            {
                // Skip category headers (composites) that don't have RTP property
            }
        }
    }

    /// <summary>
    /// Shows games by category
    /// </summary>
    public void ShowGamesByCategory(string category)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine($"\n🎮 GAMES BY CATEGORY: {category.ToUpper()}\n══════════════════════════════════");
        
        foreach (var gameComponent in _allGames.CreateIterator())
        {
            try
            {
                if (gameComponent.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write($"  🎯 {gameComponent.Name}");
                    Console.WriteLine($" - RTP: {gameComponent.Rtp:F2}% | Min: ${gameComponent.MinBet:F2}");
                    Console.WriteLine($"       {gameComponent.Description}");
                }
            }
            catch (NotSupportedException)
            {
                // Skip category headers (composites) that don't have Category property
            }
        }
    }
}