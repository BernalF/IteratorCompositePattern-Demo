namespace IteratorCompositeDemo.Iterator;

public class PancakeHouseMenu : IAggregate<MenuItem>
{
    private readonly List<MenuItem> _items = [];

    public void AddItem(MenuItem item) => _items.Add(item);

    public IIterator<MenuItem> CreateIterator() => new PancakeHouseIterator(_items);
}
