namespace IteratorCompositeDemo.Iterator;

public class PancakeHouseIterator : IIterator<MenuItem>
{
    private readonly List<MenuItem> _items;
    private int _position;

    public PancakeHouseIterator(List<MenuItem> items) => _items = items;

    public bool HasNext() => _position < _items.Count;

    public MenuItem Next()
    {
        if (!HasNext()) throw new InvalidOperationException("No more elements.");
        return _items[_position++];
    }
}
