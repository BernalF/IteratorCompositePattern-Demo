// ============================================================================
// C# Practice Solutions — Iterator & Composite Patterns
// RTG Casino Gaming Industry Examples
// ============================================================================
//
// 🎓 LEARNING RESOURCES:
// 📋 Interactive Demo Guide: IteratorCompositeDemo/INTERACTIVE_DEMO_GUIDE.md  
// 📘 Study Guide (Spanish): Guia_Estudio_Iterator_Composite_Gaming_ES.md
// 🎮 Run Interactive Demo: dotnet run (from IteratorCompositeDemo folder)
//
// 🎯 PURPOSE:
// This file contains ready-to-run C# implementations of Iterator and Composite 
// patterns applied to RTG casino gaming scenarios. Use these examples to:
// - Practice hands-on coding with both patterns
// - Understand real-world gaming industry applications  
// - Reference complete, working implementations
// - Experiment with RTG casino-specific features (RTP, providers, categories)
//
// 📚 LEARNING PATH:
// 1. Study theory → Guia_Estudio_Iterator_Composite_Gaming_ES.md
// 2. See patterns in action → dotnet run (interactive demo)  
// 3. Practice coding → This file (CSharp_Practice_Solutions.cs)
// 4. Validate understanding → dotnet test (unit tests)
//
// ============================================================================

// C# Practice — Iterator & Composite - RTG Casino Gaming Industry

// ============ A) Iterator para Dictionary<string, CasinoGame> ============
// Requerimiento: Recorrer los juegos ordenados por RTP descendente.
public interface IIterator<T> { bool HasNext(); T Next(); }
public interface IAggregate<T> { IIterator<T> CreateIterator(); }

public class CasinoGame { 
    public string Name { get; set; } 
    public decimal RTP { get; set; }  // Return to Player percentage
    public decimal MinBet { get; set; }  // Minimum bet amount
    public string Provider { get; set; }  // Game provider (RTG, NetEnt, Microgaming, etc.)
    public string Category { get; set; }  // Slots, Table, Live, etc.
}

public class GameCatalog : IAggregate<CasinoGame> {
    private readonly Dictionary<string, CasinoGame> _games = new();
    public void Add(string gameId, CasinoGame game) => _games[gameId] = game;
    public IIterator<CasinoGame> CreateIterator() => new GameCatalogIterator(_games);
}

public class GameCatalogIterator : IIterator<CasinoGame> {
    private readonly List<string> _gameIds;
    private readonly Dictionary<string, CasinoGame> _games;
    private int _index = 0;
    
    public GameCatalogIterator(Dictionary<string, CasinoGame> games) {
        _games = games;
        // Ordenar por RTP descendente, luego por nombre para desempate
        _gameIds = _games.Keys.OrderByDescending(id => _games[id].RTP)
                             .ThenBy(id => _games[id].Name)
                             .ToList();
    }
    
    public bool HasNext() => _index < _gameIds.Count;
    
    public CasinoGame Next() {
        if (!HasNext()) throw new InvalidOperationException("No more games available");
        return _games[_gameIds[_index++]];
    }
}

// ============ B) Composite para Categorías de Casino RTG (Display y FindGamesByRTP) ============
public abstract class GameComponent {
    public virtual string Name => throw new NotSupportedException("Operation not supported for this component");
    public virtual string Description => throw new NotSupportedException("Operation not supported for this component");
    public virtual decimal RTP => throw new NotSupportedException("Operation not supported for this component");
    public virtual decimal MinBet => throw new NotSupportedException("Operation not supported for this component");
    public virtual string Provider => throw new NotSupportedException("Operation not supported for this component");
    public virtual void Add(GameComponent component) => throw new NotSupportedException("Operation not supported for this component");
    public virtual void Remove(GameComponent component) => throw new NotSupportedException("Operation not supported for this component");
    public virtual void Display() => throw new NotSupportedException("Operation not supported for this component");
    public virtual List<CasinoGame> FindGamesByRTP(decimal minRTP) => throw new NotSupportedException("Operation not supported for this component");
}

public class CasinoGameLeaf : GameComponent {
    private readonly string _name, _description, _provider;
    private readonly decimal _rtp, _minBet;
    
    public CasinoGameLeaf(string name, string description, decimal rtp, decimal minBet, string provider) {
        _name = name; _description = description; _rtp = rtp; _minBet = minBet; _provider = provider;
    }
    
    public override string Name => _name;
    public override string Description => _description;
    public override decimal RTP => _rtp;
    public override decimal MinBet => _minBet;
    public override string Provider => _provider;
    
    public override void Display() => 
        Console.WriteLine($"  🎮 {_name} (RTP: {_rtp}%, Min: ${_minBet}, Provider: {_provider})");
    
    public override List<CasinoGame> FindGamesByRTP(decimal minRTP) {
        var games = new List<CasinoGame>();
        if (_rtp >= minRTP) {
            games.Add(new CasinoGame { Name = _name, RTP = _rtp, MinBet = _minBet, Provider = _provider });
        }
        return games;
    }
}

public class GameCategory : GameComponent {
    private readonly string _name, _description;
    private readonly List<GameComponent> _components = new();
    
    public GameCategory(string name, string description) { _name = name; _description = description; }
    
    public override string Name => _name;
    public override string Description => _description;
    public override void Add(GameComponent component) => _components.Add(component);
    public override void Remove(GameComponent component) => _components.Remove(component);
    
    public override void Display() {
        Console.WriteLine($"🎯 {_name} - {_description}");
        Console.WriteLine("═══════════════════════════════════════");
        foreach (var component in _components) {
            component.Display();
        }
        Console.WriteLine();
    }
    
    public override List<CasinoGame> FindGamesByRTP(decimal minRTP) {
        var result = new List<CasinoGame>();
        foreach (var component in _components) {
            result.AddRange(component.FindGamesByRTP(minRTP));
        }
        return result;
    }
}

// ============ C) Iterator + Composite para Sistema de Casino RTG Completo ============
public abstract class CasinoNode {
    public virtual string Name => throw new NotSupportedException();
    public virtual string Category => throw new NotSupportedException();
    public virtual string Provider => throw new NotSupportedException();
    public virtual void Add(CasinoNode node) => throw new NotSupportedException();
    public virtual IEnumerable<CasinoNode> GetChildren() => throw new NotSupportedException();
}

public class GameNode : CasinoNode {
    private readonly string _name, _category, _provider;
    
    public GameNode(string name, string category, string provider) {
        _name = name; _category = category; _provider = provider;
    }
    
    public override string Name => _name;
    public override string Category => _category;
    public override string Provider => _provider;
}

public class CategoryNode : CasinoNode {
    private readonly string _name, _category;
    private readonly List<CasinoNode> _children = new();
    
    public CategoryNode(string name, string category) { _name = name; _category = category; }
    
    public override string Name => _name;
    public override string Category => _category;
    public override void Add(CasinoNode node) => _children.Add(node);
    public override IEnumerable<CasinoNode> GetChildren() => _children;
}

// Iterador DFS (Depth-First Search) para recorrer toda la jerarquía del casino RTG
public class CasinoDfsIterator : IIterator<CasinoNode> {
    private readonly Stack<IEnumerator<CasinoNode>> _stack = new();
    private CasinoNode? _current;

    public CasinoDfsIterator(CasinoNode root) {
        _current = root;
        try {
            if (root is CategoryNode category) {
                _stack.Push(category.GetChildren().GetEnumerator());
            } else {
                _stack.Push(Enumerable.Empty<CasinoNode>().GetEnumerator());
            }
        } catch (NotSupportedException) {
            _stack.Push(Enumerable.Empty<CasinoNode>().GetEnumerator());
        }
    }

    public bool HasNext() {
        return _current != null || _stack.Count > 0;
    }

    public CasinoNode Next() {
        if (_current != null) {
            var result = _current;
            _current = null;
            return result;
        }
        
        while (_stack.Count > 0) {
            var iterator = _stack.Peek();
            if (iterator.MoveNext()) {
                var node = iterator.Current;
                try {
                    if (node is CategoryNode category) {
                        _stack.Push(category.GetChildren().GetEnumerator());
                    }
                } catch (NotSupportedException) {
                    // Node doesn't support children, continue
                }
                return node;
            } else {
                _stack.Pop();
                iterator.Dispose();
            }
        }
        throw new InvalidOperationException("No more casino nodes available");
    }
}

// Uso con filtro específico para gaming RTG:
void PrintGamesByProvider(CasinoNode casino, string targetProvider) {
    var iterator = new CasinoDfsIterator(casino);
    Console.WriteLine($"🎰 Games by {targetProvider}:");
    Console.WriteLine("═══════════════════════════════════════");
    
    while (iterator.HasNext()) {
        var node = iterator.Next();
        try {
            if (node is GameNode game && 
                game.Provider.Equals(targetProvider, StringComparison.OrdinalIgnoreCase)) {
                Console.WriteLine($"  🎮 {game.Name} ({game.Category})");
            }
        } catch (NotSupportedException) {
            // Skip nodes that don't support provider property
        }
    }
}

void PrintHighRTPSlots(CasinoNode casino, decimal minRTP = 96.0m) {
    var iterator = new CasinoDfsIterator(casino);
    Console.WriteLine($"🎰 High RTP Slots (≥{minRTP}%):");
    Console.WriteLine("═══════════════════════════════════════");
    
    while (iterator.HasNext()) {
        var node = iterator.Next();
        try {
            if (node is GameNode game && 
                game.Category.Equals("Slots", StringComparison.OrdinalIgnoreCase)) {
                // En un caso real, tendrías acceso al RTP aquí
                Console.WriteLine($"  🎮 {game.Name} (Provider: {game.Provider})");
            }
        } catch (NotSupportedException) {
            // Skip nodes that don't support the required properties
        }
    }
}

// ============================================================================
// EJEMPLO DE USO COMPLETO - DEMO DE TODOS LOS PATRONES CON JUEGOS RTG REALES
// ============================================================================
void DemoCasinoPatterns() {
    // ===== Iterator Pattern Demo =====
    Console.WriteLine("🎯 ITERATOR PATTERN - RTG Game Catalog by RTP");
    Console.WriteLine("═══════════════════════════════════════");
    
    var catalog = new GameCatalog();
    catalog.Add("doragons-gems", new CasinoGame { Name = "Doragon's Gems", RTP = 96.21m, MinBet = 10.0m, Provider = "RTG", Category = "Slots" });
    catalog.Add("whispers-seasons", new CasinoGame { Name = "Whispers of Seasons", RTP = 96.09m, MinBet = 0.10m, Provider = "RTG", Category = "Slots" });
    catalog.Add("plentiful-treasure", new CasinoGame { Name = "Plentiful Treasure", RTP = 95.97m, MinBet = 0.20m, Provider = "RTG", Category = "Slots" });
    catalog.Add("spirit-inca", new CasinoGame { Name = "Spirit of the Inca", RTP = 88.12m, MinBet = 0.25m, Provider = "RTG", Category = "Slots" });
    catalog.Add("blackjack", new CasinoGame { Name = "Blackjack", RTP = 99.28m, MinBet = 1.00m, Provider = "RTG", Category = "Table" });
    catalog.Add("european-roulette", new CasinoGame { Name = "European Roulette", RTP = 97.30m, MinBet = 0.50m, Provider = "RTG", Category = "Table" });
    
    var iterator = catalog.CreateIterator();
    while (iterator.HasNext()) {
        var game = iterator.Next();
        Console.WriteLine($"🎮 {game.Name} - RTP: {game.RTP}% | Min: ${game.MinBet} | {game.Provider}");
    }
    
    Console.WriteLine("\n🎯 COMPOSITE PATTERN - RTG Casino Hierarchy");
    Console.WriteLine("═══════════════════════════════════════");
    
    // ===== Composite Pattern Demo =====
    var casino = new GameCategory("RTG CASINO", "Complete RTG gaming platform");
    var slots = new GameCategory("SLOT GAMES", "Real Series Video Slot games");
    var tableGames = new GameCategory("TABLE GAMES", "Card and Table Games");
    var liveGames = new GameCategory("LIVE CASINO", "Games with Real Dealers");
    
    casino.Add(slots);
    casino.Add(tableGames);
    casino.Add(liveGames);
    
    // RTG Slot Games
    slots.Add(new CasinoGameLeaf("Doragon's Gems", "Features: Cascading Wins, Free Games With Gamble Option, Buy Feature, Bonus Bets", 96.21m, 10.0m, "RTG"));
    slots.Add(new CasinoGameLeaf("Whispers of Seasons", "Japanese-themed slot with expanding wilds", 96.09m, 0.10m, "RTG"));
    slots.Add(new CasinoGameLeaf("Plentiful Treasure", "Asian treasure slot", 95.97m, 0.20m, "RTG"));
    slots.Add(new CasinoGameLeaf("Spirit of the Inca", "Progressive slot with millionaire jackpot", 88.12m, 0.25m, "RTG"));
    
    // RTG Table Games
    tableGames.Add(new CasinoGameLeaf("Blackjack", "21 against the house", 99.28m, 1.00m, "RTG"));
    tableGames.Add(new CasinoGameLeaf("European Roulette", "Roulette with single zero", 97.30m, 0.50m, "RTG"));
    tableGames.Add(new CasinoGameLeaf("Baccarat", "High-class card game", 98.94m, 5.00m, "RTG"));
    tableGames.Add(new CasinoGameLeaf("Texas Hold'em Poker", "The king of card games", 97.82m, 2.00m, "RTG"));
    tableGames.Add(new CasinoGameLeaf("Craps", "Exciting dice game", 98.64m, 1.00m, "RTG"));
    
    // RTG Live Games
    liveGames.Add(new CasinoGameLeaf("Live VIP Blackjack", "Blackjack with real dealer", 99.28m, 5.00m, "RTG"));
    liveGames.Add(new CasinoGameLeaf("Live Roulette", "Live roulette with multiple cameras", 97.30m, 1.00m, "RTG"));
    liveGames.Add(new CasinoGameLeaf("Live Baccarat", "Live baccarat with card squeezing", 98.94m, 10.00m, "RTG"));
    
    casino.Display();
    
    // ===== Combined Iterator + Composite Demo =====
    Console.WriteLine("🎯 HIGH RTP GAMES FILTER (≥97%)");
    Console.WriteLine("═══════════════════════════════════════");
    
    var highRTPGames = casino.FindGamesByRTP(97.0m);
    foreach (var game in highRTPGames) {
        Console.WriteLine($"⭐ {game.Name} - {game.RTP}% (Provider: {game.Provider})");
    }
    
    // ===== Promotional Games Demo =====
    Console.WriteLine("\n🎁 PROMOTIONAL GAMES");
    Console.WriteLine("═══════════════════════════════════════");
    
    var promoGames = new GameCategory("PROMOTIONAL GAMES", "Games with special bonuses");
    promoGames.Add(new CasinoGameLeaf("Alien Wins", "Slot with daily free spins", 96.50m, 0.01m, "RTG"));
    promoGames.Add(new CasinoGameLeaf("Horseman Prize", "The Haunted Ride of Free Games", 97.00m, 0.10m, "RTG"));
    promoGames.Add(new CasinoGameLeaf("Fu Long Plinko", "Bonus Drops for free tokens, and multiply your winnings with every bounce", 97.80m, 1.00m, "RTG"));
    
    // Add promotional games as subcategory to slots (composite within composite!)
    slots.Add(promoGames);
    
    Console.WriteLine("\n🎰 UPDATED RTG CASINO STRUCTURE WITH PROMOTIONAL GAMES:");
    casino.Display();
}

// ============================================================================
// 🎓 LEARNING NOTES - RTG CASINO SPECIFIC:
//
// 1. Iterator Pattern Benefits in RTG Gaming:
//    - Uniform access to different RTG game catalogs and providers
//    - Encapsulation of complex sorting logic (RTP, popularity, release date)
//    - Easy addition of new RTG game series without changing client code
//
// 2. Composite Pattern Benefits in RTG Gaming:
//    - Hierarchical organization of RTG games (Casino → Categories → Games)
//    - Uniform operations on individual games and entire categories  
//    - Recursive operations (display, search, filter) work seamlessly
//    - Support for promotional game subcategories
//
// 3. Combined Power with RTG Games:
//    - Iterator can traverse RTG Composite structures safely
//    - Client code stays simple regardless of hierarchy complexity
//    - Perfect for real-world RTG casino management systems
//    - Supports both regular and promotional game organization
//
// 📚 For more learning materials, see:
// - Interactive Demo Guide: IteratorCompositeDemo/INTERACTIVE_DEMO_GUIDE.md
// - Study Guide: Guia_Estudio_Iterator_Composite_Gaming_ES.md  
// - Live Demo: dotnet run (from IteratorCompositeDemo folder)
// ============================================================================

