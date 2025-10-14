namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Base component class for the Composite pattern - represents both leaves and composites
/// From Head First Design Patterns: "The Component defines the interface for objects in the composition"
/// </summary>
public abstract class MenuComponent
{
    public virtual string Name => throw new NotSupportedException("Operation not supported for this menu component");
    public virtual string Description => throw new NotSupportedException("Operation not supported for this menu component");
    public virtual decimal Price => throw new NotSupportedException("Operation not supported for this menu component");
    public virtual bool Vegetarian => throw new NotSupportedException("Operation not supported for this menu component");

    public virtual void Add(MenuComponent menuComponent) => throw new NotSupportedException("Operation not supported for this menu component");
    public virtual void Remove(MenuComponent menuComponent) => throw new NotSupportedException("Operation not supported for this menu component");
    public virtual MenuComponent GetChild(int i) => throw new NotSupportedException("Operation not supported for this menu component");

    /// <summary>
    /// Print operation - allows uniform treatment of individual objects and compositions
    /// </summary>
    public virtual void Print() => throw new NotSupportedException("Operation not supported for this menu component");

    public virtual IEnumerable<MenuComponent> CreateIterator() => throw new NotSupportedException("Operation not supported for this menu component");
}
