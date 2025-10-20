using IteratorCompositeDemo.Composite;
using IteratorCompositeDemo.Iterator;
using System.Text;
using CasinoGame = IteratorCompositeDemo.Iterator.CasinoGame;

namespace IteratorCompositeDemo;

/// <summary>
/// Interactive demo program showing both Iterator and Composite patterns
/// Based on Head First Design Patterns Chapter: "The Iterator and Composite Patterns: Well-Managed Collections"
/// Applied to Online Casino Gaming Industry
/// </summary>
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        
        Console.Clear();
        PrintWelcome();
        
        WaitForUser("Press ENTER to start the demo...");

        // Show the problem first
        Console.Clear();
        Console.WriteLine("=== PART 1: THE PROBLEM WITHOUT PATTERNS ===\n");
        ShowProblemWithoutPatterns();
        
        WaitForUser("\nPress ENTER to see how the Iterator Pattern solves this problem...");

        // Part 1: Iterator Pattern Demo
        Console.Clear();
        Console.WriteLine("=== PART 2: ITERATOR PATTERN SOLUTION ===\n");
        Console.WriteLine("🎯 GOAL: Provide uniform access to different types of game collections\n");
        
        IteratorPatternDemo();

        WaitForUser("\nPress ENTER to explore the Composite Pattern...");

        // Part 2: Composite Pattern Demo  
        Console.Clear();
        Console.WriteLine("=== PART 3: COMPOSITE PATTERN SOLUTION ===\n");
        Console.WriteLine("🎯 GOAL: Handle tree structures uniformly (categories with subcategories)\n");
        
        CompositePatternDemo();

        // Final summary
        Console.WriteLine("\n" + new string('=', 60));
        WaitForUser("Press ENTER to see the final summary...");
        
        ShowFinalSummary();
        
        Console.WriteLine("\n🎉 Demo completed! Thank you!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static void PrintWelcome()
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          HEAD FIRST DESIGN PATTERNS - INTERACTIVE DEMO       ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║        Iterator and Composite Patterns Demo                  ║");
        Console.WriteLine("║        \"Casino Game Catalog Management\"                    ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("🎰 Welcome to the Virtual Casino! This interactive demo will show you:");
        Console.WriteLine("   • The problems these patterns solve");
        Console.WriteLine("   • How Iterator Pattern provides uniform collection access");
        Console.WriteLine("   • How Composite Pattern handles tree structures");
        Console.WriteLine("   • How both patterns work together beautifully");
        Console.WriteLine("   • ACTUAL CODE examples for both patterns!");
        Console.WriteLine();
        Console.WriteLine("💡 Tip: Take your time at each step to understand the concepts!");
    }

    /// <summary>
    /// Demonstrates what the code would look like without design patterns
    /// Shows why we need the Iterator and Composite patterns
    /// </summary>
    private static void ShowProblemWithoutPatterns()
    {
        Console.WriteLine("🚫 PROBLEM: Without patterns, we have tight coupling and code duplication\n");
        
        Console.WriteLine("Imagine you're a programmer at an online casino...");
        WaitForUser("Press ENTER to see the problematic code...");
        
        Console.WriteLine("\n📄 THE PROBLEMATIC CODE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                    BAD CODE EXAMPLE                         │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        Console.WriteLine("   void DisplayAllGames(SlotsCatalog slots, TableGamesCatalog table) {");
        Console.WriteLine("       // Slots uses List - we need to know this!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("       for(int i = 0; i < slots.GetGames().Count; i++) {");
        Console.WriteLine("           var game = slots.GetGames()[i];");
        Console.WriteLine("           Console.WriteLine($\"{game.Name} - RTP: {game.RTP}%\");");
        Console.WriteLine("       }");
        Console.ResetColor();
        Console.WriteLine("       // Table games uses Array - different iteration logic!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("       for(int i = 0; i < table.GetLength(); i++) {");
        Console.WriteLine("           var game = table.GetGames()[i];");
        Console.WriteLine("           if (game != null) // Need to check for nulls in array!");
        Console.WriteLine("               Console.WriteLine($\"{game.Name} - RTP: {game.RTP}%\");");
        Console.WriteLine("       }");
        Console.ResetColor();
        Console.WriteLine("   }");
        
        WaitForUser("\nPress ENTER to see what's wrong with this approach...");
        
        Console.WriteLine("\n❌ PROBLEMS WITH THIS APPROACH:");
        Console.WriteLine("   • Client must know internal data structure of each catalog");
        Console.WriteLine("   • Adding new game types requires changing existing code");
        Console.WriteLine("   • Different iteration logic for each collection type");
        Console.WriteLine("   • No uniform way to handle nested game categories");
        Console.WriteLine("   • Violates the Open/Closed Principle");
        
        Console.WriteLine("\n🤔 What if we had 10 different game families? 20? The code becomes unmaintainable!");
    }

    /// <summary>
    /// Demonstrates the Iterator pattern with different game catalog implementations
    /// Shows how we can iterate over different data structures uniformly
    /// </summary>
    private static void IteratorPatternDemo()
    {
        Console.WriteLine("✨ ITERATOR PATTERN TO THE RESCUE!\n");
        Console.WriteLine("The Iterator Pattern provides a way to access elements sequentially");
        Console.WriteLine("without exposing the underlying representation.\n");
        
        WaitForUser("Press ENTER to see the Iterator Pattern structure...");

        ShowIteratorPatternCode();

        WaitForUser("Press ENTER to see the Iterator Pattern in action...");

        // Slots catalog uses List<T>
        Console.WriteLine("\n🏗️  BUILDING THE GAME CATALOGS:");
        Console.WriteLine("Creating Slots Catalog (uses List<T> internally)...");
        var slotsCatalog = new SlotsCatalog();
        slotsCatalog.AddGame(new CasinoGame("Doragon's Gems", "Features: Cascading Wins, Free Games With Gamble Option, Buy Feature, Bonus Bets", "Slots", 96.21m, 10.0m));
        slotsCatalog.AddGame(new CasinoGame("Whispers of Seasons", "Japanese-themed slot with expanding wilds", "Slots", 96.09m, 0.10m));
        slotsCatalog.AddGame(new CasinoGame("Plentiful Treasure", "Asian treasure slot", "Slots", 95.97m, 0.20m));
        slotsCatalog.AddGame(new CasinoGame("Spirit of the Inca", "Progressive slot with millionaire jackpot", "Slots", 88.12m, 0.25m));

        Console.WriteLine("Creating Table Games Catalog (uses Array internally)...");
        // Table games uses Array with fixed size
        var tableGamesCatalog = new TableGamesCatalog(6);
        tableGamesCatalog.AddGame(new CasinoGame("Blackjack", "21 against the house", "Table", 99.28m, 1.0m));
        tableGamesCatalog.AddGame(new CasinoGame("European Roulette", "Roulette with single zero", "Table", 97.30m, 0.50m));
        tableGamesCatalog.AddGame(new CasinoGame("Baccarat", "High-class card game", "Table", 98.94m, 5.0m));
        tableGamesCatalog.AddGame(new CasinoGame("Texas Hold'em Poker", "The king of card games", "Table", 97.82m, 2.0m));
        tableGamesCatalog.AddGame(new CasinoGame("Craps", "Exciting dice game", "Table", 98.64m, 1.0m));

        WaitForUser("\nPress ENTER to display the Slots Catalog...");

        Console.WriteLine("\n🎰 SLOTS CATALOG (using List<T> internally):");
        PrintGameCatalog(slotsCatalog.CreateIterator());

        WaitForUser("\nPress ENTER to display the Table Games Catalog...");

        Console.WriteLine("\n🃏 TABLE GAMES CATALOG (using Array internally):");
        PrintGameCatalog(tableGamesCatalog.CreateIterator());

        WaitForUser("\nPress ENTER to see the magic of the Iterator Pattern...");

        Console.WriteLine("\n✨ THE MAGIC: Notice how the same PrintGameCatalog() method works for both!");
        
        ShowIteratorClientCode();

        Console.WriteLine("\n✅ ITERATOR PATTERN BENEFITS:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("   • Same PrintGameCatalog() method works for both catalogs");
        Console.WriteLine("   • Client code doesn't know about internal data structures");
        Console.WriteLine("   • Easy to add new catalog types without changing existing code");
        Console.WriteLine("   • Encapsulates the iteration logic within each collection");
        Console.ResetColor();
    }

    /// <summary>
    /// Demonstrates the Composite pattern with nested game category structure
    /// Shows how we can treat individual items and collections uniformly
    /// </summary>
    private static void CompositePatternDemo()
    {
        Console.WriteLine("🌳 COMPOSITE PATTERN: Building Tree Structures\n");
        Console.WriteLine("What if we want to create nested categories? Subcategories within categories?");
        Console.WriteLine("The Composite Pattern lets us build tree structures and treat");
        Console.WriteLine("individual objects and compositions uniformly.\n");

        WaitForUser("Press ENTER to see the Composite Pattern structure...");

        ShowCompositePatternCode();

        WaitForUser("Press ENTER to build a complex game hierarchy...");

        Console.WriteLine("\n🏗️  BUILDING THE GAME HIERARCHY:");
        
        // Create the main catalog (root composite)
        Console.WriteLine("Creating main casino container...");
        var allGames = new GameCategory("VIRTUAL CASINO", "All casino games");

        // Create sub-categories (composites)
        Console.WriteLine("Creating slots, table games, live casino, and promotional categories...");
        var slotsCategory = new GameCategory("SLOT GAMES", "Real Series Video Slot games");
        var tableGamesCategory = new GameCategory("TABLE GAMES", "Card and Table Games");
        var liveCasinoCategory = new GameCategory("LIVE CASINO", "Games with Real Dealers");
        var promoGamesCategory = new GameCategory("PROMOTIONAL GAMES", "Games with special bonuses");

        // Add sub-categories to main catalog
        allGames.Add(slotsCategory);
        allGames.Add(tableGamesCategory);
        allGames.Add(liveCasinoCategory);

        Console.WriteLine("Adding games to each category...");

        // Add games to Slots (leaves)
        slotsCategory.Add(new Composite.CasinoGame("Doragon's Gems", "Features: Cascading Wins, Free Games With Gamble Option, Buy Feature, Bonus Bets", "Slots", 96.21m, 10.0m));
        slotsCategory.Add(new Composite.CasinoGame("Whispers of Seasons", "Japanese-themed slot with expanding wilds", "Slots", 96.09m, 0.10m));
        slotsCategory.Add(new Composite.CasinoGame("Plentiful Treasure", "Asian treasure slot", "Slots", 95.97m, 0.20m));
        slotsCategory.Add(new Composite.CasinoGame("Spirit of the Inca", "Progressive slot with millionaire jackpot", "Slots", 88.12m, 0.25m));

        // Add games to Table Games (leaves)
        tableGamesCategory.Add(new Composite.CasinoGame("Blackjack", "21 against the house", "Table", 99.28m, 1.0m));
        tableGamesCategory.Add(new Composite.CasinoGame("European Roulette", "Roulette with single zero", "Table", 97.30m, 0.50m));
        tableGamesCategory.Add(new Composite.CasinoGame("Baccarat", "High-class card game", "Table", 98.94m, 5.0m));
        tableGamesCategory.Add(new Composite.CasinoGame("Texas Hold'em Poker", "The king of card games", "Table", 97.82m, 2.0m));

        WaitForUser("\nPress ENTER to add a nested promotional games category...");
        
        Console.WriteLine("\n🎁 ADDING NESTED STRUCTURE:");
        Console.WriteLine("Adding promotional games category AS A SUBCATEGORY of slots...");
        // Add the promo subcategory to slots category (composite within composite!)
        slotsCategory.Add(promoGamesCategory);

        // Add games to Live Casino (leaves)
        liveCasinoCategory.Add(new Composite.CasinoGame("Live VIP Blackjack", "Blackjack with real dealer", "Live", 99.28m, 5.0m));
        liveCasinoCategory.Add(new Composite.CasinoGame("Live Roulette", "Live roulette with multiple cameras", "Live", 97.30m, 1.0m));
        liveCasinoCategory.Add(new Composite.CasinoGame("Live Baccarat", "Live baccarat with card squeezing", "Live", 98.94m, 10.0m));

        // Add promotional games (leaves)
        promoGamesCategory.Add(new Composite.CasinoGame("Alien Wins", "Slot with daily free spins", "Promotional", 96.50m, 0.01m));
        promoGamesCategory.Add(new Composite.CasinoGame("Horseman Prize", "The Haunted Ride of Free Games", "Promotional", 97.00m, 0.10m));
        promoGamesCategory.Add(new Composite.CasinoGame("Fu Long Plinko", "Bonus Drops for free tokens, and multiply your winnings with every bounce", "Promotional", 97.80m, 1.0m));

        WaitForUser("Press ENTER to see the complete casino structure...");

        // Create game manager (client)
        var gameManager = new GameManager(allGames);

        Console.WriteLine("\n🎰 COMPLETE CASINO STRUCTURE:");
        Console.WriteLine("Notice how the promotional category appears nested under slots!");
        gameManager.ShowAllGames();

        WaitForUser("\nPress ENTER to see Iterator + Composite working together...");

        Console.WriteLine("\n🎯 HIGH RTP GAMES (Iterator traversing Composite structure):");
        Console.WriteLine("Watch how we can iterate through the ENTIRE tree structure!");
        gameManager.ShowHighRtpGames();

        WaitForUser("\nPress ENTER to see how client code stays simple...");

        ShowCompositeClientCode();

        WaitForUser("\nPress ENTER to see the Composite Pattern benefits...");

        Console.WriteLine("\n✅ COMPOSITE PATTERN BENEFITS:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("   • Builds tree structures of arbitrary complexity");
        Console.WriteLine("   • Uniform treatment of individual objects and compositions");
        Console.WriteLine("   • Iterator pattern works seamlessly with Composite pattern");
        Console.WriteLine("   • The game manager (client) doesn't distinguish between leaves and composites");
        Console.WriteLine("   • Easy to add new components without changing existing code");
        Console.ResetColor();
    }

    private static void ShowIteratorPatternCode()
    {
        Console.WriteLine("📋 ITERATOR PATTERN STRUCTURE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                 ITERATOR PATTERN CODE                       │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("// 1. Iterator Interface");
        Console.ResetColor();
        Console.WriteLine("   public interface IIterator<T> {");
        Console.WriteLine("       bool HasNext();");
        Console.WriteLine("       T Next();");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("// 2. Aggregate Interface");
        Console.ResetColor();
        Console.WriteLine("   public interface IAggregate<T> {");
        Console.WriteLine("       IIterator<T> CreateIterator();");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("// 3. Concrete Iterator (Example: for List)");
        Console.ResetColor();
        Console.WriteLine("   public class SlotsIterator : IIterator<CasinoGame> {");
        Console.WriteLine("       private List<CasinoGame> _games;");
        Console.WriteLine("       private int _position = 0;");
        Console.WriteLine();
        Console.WriteLine("       public bool HasNext() => _position < _games.Count;");
        Console.WriteLine();
        Console.WriteLine("       public CasinoGame Next() {");
        Console.WriteLine("           if (!HasNext()) throw new InvalidOperationException();");
        Console.WriteLine("           return _games[_position++];");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
    }

    private static void ShowIteratorClientCode()
    {
        Console.WriteLine("\n📋 ITERATOR PATTERN - CLIENT CODE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                  CLEAN CLIENT CODE                          │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// The same method works for ANY catalog type!");
        Console.ResetColor();
        Console.WriteLine("   void PrintGameCatalog(IIterator<CasinoGame> iterator) {");
        Console.WriteLine("       while (iterator.HasNext()) {");
        Console.WriteLine("           var game = iterator.Next();");
        Console.WriteLine("           Console.WriteLine($\"{game.Name} - RTP: {game.RTP}%\");");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// Usage - same method, different data structures!");
        Console.ResetColor();
        Console.WriteLine("   PrintGameCatalog(slotsCatalog.CreateIterator());      // List<T>");
        Console.WriteLine("   PrintGameCatalog(tableGamesCatalog.CreateIterator()); // Array");
        Console.WriteLine("   PrintGameCatalog(PlinkoCatalog.CreateIterator());       // Any future type!");
    }

    private static void ShowCompositePatternCode()
    {
        Console.WriteLine("📋 COMPOSITE PATTERN STRUCTURE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                COMPOSITE PATTERN CODE                       │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("// 1. Component (base class for all game elements)");
        Console.ResetColor();
        Console.WriteLine("   public abstract class GameComponent {");
        Console.WriteLine("       public virtual string Name => throw new NotSupportedException();");
        Console.WriteLine("       public virtual decimal RTP => throw new NotSupportedException();");
        Console.WriteLine("       public virtual void Add(GameComponent c) => throw new NotSupportedException();");
        Console.WriteLine("       public virtual void Display() => throw new NotSupportedException();");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("// 2. Leaf (individual casino games)");
        Console.ResetColor();
        Console.WriteLine("   public class CasinoGame : GameComponent {");
        Console.WriteLine("       public override string Name { get; }");
        Console.WriteLine("       public override decimal RTP { get; }");
        Console.WriteLine("       public override void Display() {");
        Console.WriteLine("           Console.WriteLine($\"  {Name} - RTP: {RTP}%\");");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("// 3. Composite (game category containers)");
        Console.ResetColor();
        Console.WriteLine("   public class GameCategory : GameComponent {");
        Console.WriteLine("       private List<GameComponent> _components = new();");
        Console.WriteLine();
        Console.WriteLine("       public override void Add(GameComponent component) {");
        Console.WriteLine("           _components.Add(component);");
        Console.WriteLine("       }");
        Console.WriteLine();
        Console.WriteLine("       public override void Display() {");
        Console.WriteLine("           Console.WriteLine($\"\\n{Name}\");");
        Console.WriteLine("           foreach(var component in _components)");
        Console.WriteLine("               component.Display(); // Recursive!");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
    }

    private static void ShowCompositeClientCode()
    {
        Console.WriteLine("\n📋 COMPOSITE PATTERN - CLIENT CODE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│              UNIFORM CLIENT CODE                            │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// Client treats leaves and composites uniformly!");
        Console.ResetColor();
        Console.WriteLine("   public class GameManager {");
        Console.WriteLine("       private GameComponent _allGames;");
        Console.WriteLine();
        Console.WriteLine("       public void ShowAllGames() {");
        Console.WriteLine("           _allGames.Display(); // Works for entire tree!");
        Console.WriteLine("       }");
        Console.WriteLine();
        Console.WriteLine("       public void ShowHighRTPGames() {");
        Console.WriteLine("           foreach(var component in _allGames.CreateIterator()) {");
        Console.WriteLine("               if (component.RTP > 97.0m)");
        Console.WriteLine("                   Console.WriteLine(component.Name);");
        Console.WriteLine("           }");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// The magic: Same code handles simple games AND complex hierarchies!");
        Console.ResetColor();
    }

    private static void ShowFinalSummary()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        FINAL SUMMARY                         ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("🎓 WHAT YOU'VE LEARNED:");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🔄 ITERATOR PATTERN:");
        Console.ResetColor();
        Console.WriteLine("   • Provides uniform access to different collection types");
        Console.WriteLine("   • Encapsulates iteration logic within collections");
        Console.WriteLine("   • Makes client code independent of collection implementation");
        Console.WriteLine("   • Follows the Single Responsibility Principle");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("🌳 COMPOSITE PATTERN:");
        Console.ResetColor();
        Console.WriteLine("   • Composes objects into tree structures");
        Console.WriteLine("   • Treats individual objects and compositions uniformly");
        Console.WriteLine("   • Enables recursive operations on tree structures");
        Console.WriteLine("   • Simplifies client code when working with hierarchies");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🤝 PATTERNS WORKING TOGETHER:");
        Console.ResetColor();
        Console.WriteLine("   • Iterator can traverse Composite structures");
        Console.WriteLine("   • Both patterns promote loose coupling");
        Console.WriteLine("   • Both support the Open/Closed Principle");
        Console.WriteLine("   • Real-world applicability in gaming industry");
        Console.WriteLine();
        
        Console.WriteLine("🎯 KEY TAKEAWAY: Design patterns help us write flexible,");
        Console.WriteLine("   maintainable code that can evolve with changing requirements!");
    }

    /// <summary>
    /// Helper method to print game catalog using iterator pattern
    /// </summary>
    private static void PrintGameCatalog(IIterator<CasinoGame> iterator)
    {
        while (iterator.HasNext())
        {
            var game = iterator.Next();
            Console.Write($"  🎮 {game.Name}");
            if (game.Category == "Promotional")
                Console.Write(" 🎁");
            Console.WriteLine($" -- RTP: {game.Rtp:F2}% | Min Bet: ${game.MinBet:F2}");
            Console.WriteLine($"       {game.Description}");
        }
    }

    /// <summary>
    /// Helper method to wait for user input with custom message
    /// </summary>
    private static void WaitForUser(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.ReadLine();
    }
}
