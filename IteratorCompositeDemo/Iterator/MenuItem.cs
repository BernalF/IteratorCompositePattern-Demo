namespace IteratorCompositeDemo.Iterator;

public class MenuItem(string name, string description, bool vegetarian, decimal price)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public bool Vegetarian { get; } = vegetarian;
    public decimal Price { get; } = price;
}
