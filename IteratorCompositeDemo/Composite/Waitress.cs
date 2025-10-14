namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Waitress class - the client in the Composite pattern
/// From Head First Design Patterns: Demonstrates how client code can treat 
/// individual objects and compositions uniformly
/// </summary>
public class Waitress
{
    private readonly MenuComponent _allMenus;

    public Waitress(MenuComponent allMenus)
    {
        _allMenus = allMenus;
    }

    /// <summary>
    /// Prints the entire menu structure using the composite pattern
    /// </summary>
    public void PrintMenu()
    {
        _allMenus.Print();
    }

    /// <summary>
    /// Prints only vegetarian items using the iterator
    /// Demonstrates how the iterator pattern works with the composite pattern
    /// </summary>
    public void PrintVegetarianMenu()
    {
        Console.WriteLine("\nVEGETARIAN MENU\n----");
        
        foreach (var menuComponent in _allMenus.CreateIterator())
        {
            try
            {
                if (menuComponent.Vegetarian)
                {
                    Console.Write($"  {menuComponent.Name}");
                    Console.WriteLine($", ${menuComponent.Price:F2}");
                    Console.WriteLine($"     -- {menuComponent.Description}");
                }
            }
            catch (NotSupportedException)
            {
                // Skip menu headers (composites) that don't have vegetarian property
            }
        }
    }
}