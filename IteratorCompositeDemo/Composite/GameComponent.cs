namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Base component class for the Composite pattern - represents both leaves and composites
/// From Head First Design Patterns: "The Component defines the interface for objects in the composition"
/// Applied to casino game hierarchy
/// </summary>
public abstract class GameComponent
{
    public virtual string Name => throw new NotSupportedException("Operation not supported for this game component");
    public virtual string Description => throw new NotSupportedException("Operation not supported for this game component");
    public virtual string Category => throw new NotSupportedException("Operation not supported for this game component");
    public virtual decimal Rtp => throw new NotSupportedException("Operation not supported for this game component");
    public virtual decimal MinBet => throw new NotSupportedException("Operation not supported for this game component");

    public virtual void Add(GameComponent gameComponent) => throw new NotSupportedException("Operation not supported for this game component");
    public virtual void Remove(GameComponent gameComponent) => throw new NotSupportedException("Operation not supported for this game component");
    public virtual GameComponent GetChild(int i) => throw new NotSupportedException("Operation not supported for this game component");

    /// <summary>
    /// Display operation - allows uniform treatment of individual games and game categories
    /// </summary>
    public virtual void Display() => throw new NotSupportedException("Operation not supported for this game component");

    public virtual IEnumerable<GameComponent> CreateIterator() => throw new NotSupportedException("Operation not supported for this game component");
}
