using System.Collections;

namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Composite class in the Composite pattern - represents menu collections that can contain other menus and menu items
/// From Head First Design Patterns: "The Composite defines behavior for components having children"
/// </summary>
public class Menu : MenuComponent
{
    private readonly List<MenuComponent> _menuComponents = new();
    private readonly string _name;
    private readonly string _description;

    public Menu(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public override string Name => _name;
    public override string Description => _description;

    public override void Add(MenuComponent menuComponent)
    {
        _menuComponents.Add(menuComponent);
    }

    public override void Remove(MenuComponent menuComponent)
    {
        _menuComponents.Remove(menuComponent);
    }

    public override MenuComponent GetChild(int i)
    {
        if (i < 0 || i >= _menuComponents.Count)
            throw new ArgumentOutOfRangeException(nameof(i));
        return _menuComponents[i];
    }

    /// <summary>
    /// Print implementation for composite nodes - recursively prints all children
    /// </summary>
    public override void Print()
    {
        Console.Write($"\n{Name}");
        Console.WriteLine($", {Description}");
        Console.WriteLine("---------------------");

        // Iterate through all menu components and call their print method
        foreach (var menuComponent in _menuComponents)
        {
            menuComponent.Print();
        }
    }

    /// <summary>
    /// Creates an iterator that traverses the entire tree structure
    /// </summary>
    public override IEnumerable<MenuComponent> CreateIterator()
    {
        return new EnumerableWrapper(new CompositeIterator(this));
    }

    /// <summary>
    /// Helper class to wrap IEnumerator as IEnumerable
    /// </summary>
    private class EnumerableWrapper : IEnumerable<MenuComponent>
    {
        private readonly IEnumerator<MenuComponent> _enumerator;

        public EnumerableWrapper(IEnumerator<MenuComponent> enumerator)
        {
            _enumerator = enumerator;
        }

        public IEnumerator<MenuComponent> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerator;
        }
    }
}
