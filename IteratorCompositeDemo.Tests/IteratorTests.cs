using IteratorCompositeDemo.Iterator;
using Xunit;

namespace IteratorCompositeDemo.Tests;

public class IteratorTests
{
    [Fact(DisplayName = "PancakeHouseIterator should iterate through items in order when menu has multiple items")]
    public void CreateIterator_MenuWithMultipleItems_IteratesInOrder()
    {
        var menu = new PancakeHouseMenu();
        menu.AddItem(new MenuItem("A", "desc", true, 1m));
        menu.AddItem(new MenuItem("B", "desc", false, 2m));

        var it = menu.CreateIterator();
        Assert.True(it.HasNext());
        var first = it.Next();
        Assert.Equal("A", first.Name);

        Assert.True(it.HasNext());
        var second = it.Next();
        Assert.Equal("B", second.Name);

        Assert.False(it.HasNext());
        Assert.Throws<InvalidOperationException>(() => it.Next());
    }

    [Fact(DisplayName = "DinerMenuIterator should respect count and throw when attempting to read beyond capacity")]
    public void CreateIterator_MenuWithLimitedCapacity_RespectsCountAndThrowsWhenDone()
    {
        var menu = new DinerMenu(2);
        menu.AddItem(new MenuItem("Only", "one", true, 1m));

        var it = menu.CreateIterator();
        Assert.True(it.HasNext());
        _ = it.Next();
        Assert.False(it.HasNext());
        Assert.Throws<InvalidOperationException>(() => it.Next());
    }

    [Fact(DisplayName = "PancakeHouseIterator should return no items when menu is empty")]
    public void CreateIterator_EmptyPancakeHouseMenu_ReturnsNoItems()
    {
        var menu = new PancakeHouseMenu();
        var iterator = menu.CreateIterator();

        Assert.False(iterator.HasNext());
        Assert.Throws<InvalidOperationException>(() => iterator.Next());
    }

    [Fact(DisplayName = "DinerMenuIterator should return no items when menu is empty")]
    public void CreateIterator_EmptyDinerMenu_ReturnsNoItems()
    {
        var menu = new DinerMenu(5);
        var iterator = menu.CreateIterator();

        Assert.False(iterator.HasNext());
        Assert.Throws<InvalidOperationException>(() => iterator.Next());
    }

    [Fact(DisplayName = "DinerMenu should throw exception when attempting to add item beyond capacity")]
    public void AddItem_MenuAtFullCapacity_ThrowsException()
    {
        var menu = new DinerMenu(1);
        menu.AddItem(new MenuItem("First", "desc", true, 1m));
        
        Assert.Throws<InvalidOperationException>(() => 
            menu.AddItem(new MenuItem("Second", "desc", true, 2m)));
    }

    [Fact(DisplayName = "Multiple iterators on same PancakeHouseMenu should work independently")]
    public void CreateIterator_MultipleIteratorsOnSameMenu_WorkIndependently()
    {
        var menu = new PancakeHouseMenu();
        menu.AddItem(new MenuItem("Item1", "desc", true, 1m));
        menu.AddItem(new MenuItem("Item2", "desc", false, 2m));

        var iterator1 = menu.CreateIterator();
        var iterator2 = menu.CreateIterator();

        // Both iterators should work independently
        Assert.True(iterator1.HasNext());
        Assert.True(iterator2.HasNext());

        var item1_from_iter1 = iterator1.Next();
        Assert.Equal("Item1", item1_from_iter1.Name);

        // iterator2 should still be at the beginning
        Assert.True(iterator2.HasNext());
        var item1_from_iter2 = iterator2.Next();
        Assert.Equal("Item1", item1_from_iter2.Name);
    }
}
