using IteratorCompositeDemo.Composite;
using Xunit;
using System.Linq;

namespace IteratorCompositeDemo.Tests;

/// <summary>
/// Integration tests demonstrating the Waitress class usage
/// Tests the client code that uses both Iterator and Composite patterns
/// </summary>
public class WaitressTests
{
    [Fact(DisplayName = "Waitress PrintMenu should execute without throwing exceptions when given valid menu structure")]
    public void PrintMenu_ValidMenuStructure_DoesNotThrow()
    {
        // Arrange
        var allMenus = CreateSampleMenuStructure();
        var waitress = new Waitress(allMenus);

        // Act & Assert - Should not throw any exceptions
        var exception = Record.Exception(() => waitress.PrintMenu());
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Waitress PrintVegetarianMenu should execute without throwing exceptions when given valid menu structure")]
    public void PrintVegetarianMenu_ValidMenuStructure_DoesNotThrow()
    {
        // Arrange  
        var allMenus = CreateSampleMenuStructure();
        var waitress = new Waitress(allMenus);

        // Act & Assert - Should not throw any exceptions
        var exception = Record.Exception(() => waitress.PrintVegetarianMenu());
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Menu iterator should correctly traverse structure with mixed menu and item components")]
    public void CreateIterator_MenuStructureWithMixedItems_IteratesCorrectly()
    {
        // Arrange
        var allMenus = CreateSampleMenuStructure();

        // Act
        var allItems = allMenus.CreateIterator().ToList();

        // Assert
        Assert.True(allItems.Count > 0);
        Assert.Contains(allItems, item => item.Name == "ALL MENUS");
        Assert.Contains(allItems, item => 
        {
            try 
            { 
                return item.Vegetarian; 
            } 
            catch 
            { 
                return false; 
            }
        });
    }

    private static Menu CreateSampleMenuStructure()
    {
        var allMenus = new Menu("ALL MENUS", "All menus combined");
        var breakfast = new Menu("BREAKFAST", "Morning meals");
        var lunch = new Menu("LUNCH", "Midday meals");

        allMenus.Add(breakfast);
        allMenus.Add(lunch);

        breakfast.Add(new MenuItem("Pancakes", "Fluffy pancakes", true, 5.99m));
        breakfast.Add(new MenuItem("Eggs Benedict", "Poached eggs on English muffin", false, 8.99m));

        lunch.Add(new MenuItem("Veggie Burger", "Plant-based burger", true, 9.99m));
        lunch.Add(new MenuItem("Club Sandwich", "Triple decker with bacon", false, 12.99m));

        return allMenus;
    }
}