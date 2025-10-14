namespace IteratorCompositeDemo.Iterator;

public class DinerMenu : IAggregate<MenuItem>
{
    private readonly MenuItem?[] _items;
    private int _count;

    public DinerMenu(int capacity) => _items = new MenuItem?[capacity];

    public void AddItem(MenuItem item)
    {
        if (_count >= _items.Length) throw new InvalidOperationException("Menu is full.");
        _items[_count++] = item;
    }

    public IIterator<MenuItem> CreateIterator() => new DinerMenuIterator(_items, _count);
}
