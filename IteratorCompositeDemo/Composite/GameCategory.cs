using System.Collections;
using System.Text;

namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Composite class in the Composite pattern - represents game categories that can contain other categories and casino games
/// From Head First Design Patterns: "The Composite defines behavior for components having children"
/// </summary>
public class GameCategory : GameComponent
{
    private readonly List<GameComponent> _gameComponents = new();

    public GameCategory(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string Name { get; }
    public override string Description { get; }

    public override void Add(GameComponent gameComponent)
    {
        _gameComponents.Add(gameComponent);
    }

    public override void Remove(GameComponent gameComponent)
    {
        _gameComponents.Remove(gameComponent);
    }

    public override GameComponent GetChild(int i)
    {
        if (i < 0 || i >= _gameComponents.Count)
            throw new ArgumentOutOfRangeException(nameof(i));
        return _gameComponents[i]; 
    }

    /// <summary>
    /// Display implementation for composite nodes - recursively displays all children
    /// </summary>
    public override void Display()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write($"\n🎯 {Name}");
        Console.WriteLine($" - {Description}");
        Console.WriteLine("═══════════════════════════════════════");

        // Iterate through all game components and call their display method
        foreach (var gameComponent in _gameComponents)
        {
            gameComponent.Display();
        }
    }

    /// <summary>
    /// Creates an iterator that traverses the entire tree structure
    /// </summary>
    public override IEnumerable<GameComponent> CreateIterator()
    {
        return new EnumerableWrapper(new CompositeIterator(this));
    }

    /// <summary>
    /// Helper class to wrap IEnumerator as IEnumerable
    /// </summary>
    private class EnumerableWrapper : IEnumerable<GameComponent>
    {
        private readonly IEnumerator<GameComponent> _enumerator;

        public EnumerableWrapper(IEnumerator<GameComponent> enumerator)
        {
            _enumerator = enumerator;
        }

        public IEnumerator<GameComponent> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerator;
        }
    }
}
