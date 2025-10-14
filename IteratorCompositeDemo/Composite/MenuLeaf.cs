namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Leaf class in the Composite pattern - represents individual menu items
/// From Head First Design Patterns: "The Leaf defines the behavior for the elements in the composition"
/// </summary>
public class MenuItem : MenuComponent
{
    private readonly string _name;
    private readonly string _description;
    private readonly bool _vegetarian;
    private readonly decimal _price;

    public MenuItem(string name, string description, bool vegetarian, decimal price)
    {
        _name = name;
        _description = description;
        _vegetarian = vegetarian;
        _price = price;
    }

    public override string Name => _name;
    public override string Description => _description;
    public override bool Vegetarian => _vegetarian;
    public override decimal Price => _price;

    /// <summary>
    /// Print implementation for leaf nodes
    /// </summary>
    public override void Print()
    {
        Console.Write($"  {Name}");
        if (Vegetarian)
            Console.Write("(v)");
        Console.WriteLine($", ${Price:F2}");
        Console.WriteLine($"     -- {Description}");
    }
}
