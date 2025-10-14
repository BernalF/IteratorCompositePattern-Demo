using IteratorCompositeDemo.Composite;
using Xunit;
using System.Linq;

namespace IteratorCompositeDemo.Tests;

public class CompositeIteratorTests
{
    [Fact(DisplayName = "CompositeIterator should traverse menu hierarchy in depth-first order")]
    public void CreateIterator_MenuHierarchyWithMultipleLevels_TraversesInDepthFirstOrder()
    {
        var root = new Menu("ROOT", "root");
        var c1 = new Menu("C1", "child 1");
        var c2 = new Menu("C2", "child 2");
        root.Add(c1);
        root.Add(c2);
        c1.Add(new MenuItem("L1", "", true, 1m));
        c2.Add(new MenuItem("L2", "", false, 2m));

        var names = root.CreateIterator().Select(c => c.Name).ToList();

        Assert.Equal(new[] { "ROOT", "C1", "L1", "C2", "L2" }, names);
    }

    [Fact(DisplayName = "CompositeIterator should traverse nested menus correctly maintaining depth-first order")]
    public void CreateIterator_MenuWithNestedSubmenus_TraversesCorrectly()
    {
        var root = new Menu("ALL MENUS", "All menus combined");
        var breakfast = new Menu("BREAKFAST", "Morning meals");
        var lunch = new Menu("LUNCH", "Midday meals");
        var dessert = new Menu("DESSERT", "Sweet endings");

        root.Add(breakfast);
        root.Add(lunch);
        lunch.Add(dessert); // Nest dessert under lunch

        breakfast.Add(new MenuItem("Pancakes", "Fluffy pancakes", true, 5.99m));
        lunch.Add(new MenuItem("Sandwich", "Club sandwich", false, 7.99m));
        dessert.Add(new MenuItem("Ice Cream", "Vanilla ice cream", true, 3.99m));

        var names = root.CreateIterator().Select(c => c.Name).ToArray();

        // Depth-first traversal: ROOT -> BREAKFAST -> Pancakes -> LUNCH -> DESSERT -> Ice Cream -> Sandwich
        // The dessert menu gets processed before the sandwich because it was added first to lunch
        Assert.Equal(new[] { "ALL MENUS", "BREAKFAST", "Pancakes", "LUNCH", "DESSERT", "Ice Cream", "Sandwich" }, names);
    }

    [Fact(DisplayName = "CompositeIterator should return only menu name when menu has no items")]
    public void CreateIterator_EmptyMenu_ReturnsOnlyMenuName()
    {
        var emptyMenu = new Menu("EMPTY", "No items");
        
        var names = emptyMenu.CreateIterator().Select(c => c.Name).ToArray();
        
        Assert.Equal(new[] { "EMPTY" }, names);
    }

    [Fact(DisplayName = "CompositeIterator should return both menu and item when menu has single item")]
    public void CreateIterator_MenuWithSingleItem_ReturnsBothMenuAndItem()
    {
        var menu = new Menu("SINGLE", "One item menu");
        menu.Add(new MenuItem("Lone Item", "The only item", true, 1.99m));
        
        var names = menu.CreateIterator().Select(c => c.Name).ToArray();
        
        Assert.Equal(new[] { "SINGLE", "Lone Item" }, names);
    }
}
