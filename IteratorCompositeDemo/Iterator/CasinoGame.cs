namespace IteratorCompositeDemo.Iterator;

/// <summary>
/// Represents an individual casino game with all its properties
/// Used in the Iterator pattern as the element being iterated
/// </summary>
public class CasinoGame(string name, string description, string category, decimal rtp, decimal minBet)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string Category { get; } = category;
    public decimal Rtp { get; } = rtp; // Return to Player percentage
    public decimal MinBet { get; } = minBet; // Minimum bet amount
}
