namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Leaf class in the Composite pattern - represents individual casino games
/// From Head First Design Patterns: "The Leaf defines the behavior for the elements in the composition"
/// </summary>
public class CasinoGame : GameComponent
{
    public CasinoGame(string name, string description, string category, decimal rtp, decimal minBet)
    {
        Name = name;
        Description = description;
        Category = category;
        Rtp = rtp;
        MinBet = minBet;
    }

    public override string Name { get; }
    public override string Description { get; }
    public override string Category { get; }
    public override decimal Rtp { get; }
    public override decimal MinBet { get; }

    /// <summary>
    /// Display implementation for leaf nodes
    /// </summary>
    public override void Display()
    {
        Console.Write($"  🎮 {Name}");
        if (Category == "Promotional")
            Console.Write(" 🎁");
        Console.WriteLine($" - RTP: {Rtp:F2}% | Min Bet: ${MinBet:F2}");
        Console.WriteLine($"       {Description}");
    }
}
