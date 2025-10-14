namespace IteratorCompositeDemo.Iterator;

public class DinerMenuIterator : IIterator<MenuItem>
{
    private readonly MenuItem?[] _items;
    private readonly int _count;
    private int _position;

    public DinerMenuIterator(MenuItem?[] items, int count)
    {
        _items = items;
        _count = count;
    }

    public bool HasNext() => _position < _count;

    public MenuItem Next()
    {
        if (!HasNext()) throw new InvalidOperationException("No more elements.");
        return _items[_position++]!;
    }
}
