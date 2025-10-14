using IteratorCompositeDemo.Composite;
using Xunit;
using System.Linq;

namespace IteratorCompositeDemo.Tests;

public class CompositeBehaviorTests
{
    [Fact(DisplayName = "MenuItem Add operation should throw NotSupportedException when called on leaf node")]
    public void Add_MenuItemLeafNode_ThrowsNotSupported()
    {
        var leaf = new MenuItem("L", "", true, 1m);
        Assert.Throws<NotSupportedException>(() => leaf.Add(new MenuItem("X", "", true, 1m)));
    }

    [Fact(DisplayName = "Iterator should filter vegetarian items correctly when traversing menu structure")]
    public void CreateIterator_MenuWithMixedVegetarianItems_FiltersVegetarianCorrectly()
    {
        var root = new Menu("ROOT", "root");
        var dessert = new Menu("Dessert", "sweet");
        root.Add(dessert);
        dessert.Add(new MenuItem("Apple Pie", "", true, 1.5m));
        dessert.Add(new MenuItem("Steak Pie", "", false, 2m));

        var vegNames = root.CreateIterator()
            .Where(item => {
                try 
                { 
                    return item.Vegetarian; 
                }
                catch (NotSupportedException) 
                { 
                    return false; // Skip menu headers that don't have vegetarian property
                }
            })
            .Select(d => d.Name)
            .ToArray();

        Assert.Equal(new[] { "Apple Pie" }, vegNames);
    }
}
