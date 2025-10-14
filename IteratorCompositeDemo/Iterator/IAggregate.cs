namespace IteratorCompositeDemo.Iterator;

public interface IAggregate<T>
{
    IIterator<T> CreateIterator();
}
