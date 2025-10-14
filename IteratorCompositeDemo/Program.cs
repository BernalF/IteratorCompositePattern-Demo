using IteratorCompositeDemo.Composite;
using IteratorCompositeDemo.Iterator;
using MenuItem = IteratorCompositeDemo.Iterator.MenuItem;

namespace IteratorCompositeDemo;

/// <summary>
/// Interactive demo program showing both Iterator and Composite patterns
/// Based on Head First Design Patterns Chapter: "The Iterator and Composite Patterns: Well-Managed Collections"
/// </summary>
internal class Program
{
    static void Main()
    {
        Console.Clear();
        PrintWelcome();
        
        WaitForUser("Press ENTER to start the demo...");

        // Show the problem first
        Console.Clear();
        Console.WriteLine("=== PART 1: THE PROBLEM WITHOUT PATTERNS ===\n");
        ShowProblemWithoutPatterns();
        
        WaitForUser("\nPress ENTER to see how the Iterator Pattern solves this problem...");

        // Part 1: Iterator Pattern Demo
        Console.Clear();
        Console.WriteLine("=== PART 2: ITERATOR PATTERN SOLUTION ===\n");
        Console.WriteLine("🎯 GOAL: Provide uniform access to different collection types\n");
        
        IteratorPatternDemo();

        WaitForUser("\nPress ENTER to explore the Composite Pattern...");

        // Part 2: Composite Pattern Demo  
        Console.Clear();
        Console.WriteLine("=== PART 3: COMPOSITE PATTERN SOLUTION ===\n");
        Console.WriteLine("🎯 GOAL: Handle tree structures uniformly (menus with submenus)\n");
        
        CompositePatternDemo();

        // Final summary
        Console.WriteLine("\n" + new string('=', 60));
        WaitForUser("Press ENTER to see the final summary...");
        
        ShowFinalSummary();
        
        Console.WriteLine("\n🎉 Demo completed! Thank you!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static void PrintWelcome()
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          HEAD FIRST DESIGN PATTERNS - INTERACTIVE DEMO      ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║        Iterator and Composite Patterns Demo                 ║");
        Console.WriteLine("║        \"Well-Managed Collections\"                           ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("👋 Welcome! This interactive demo will show you:");
        Console.WriteLine("   • The problems these patterns solve");
        Console.WriteLine("   • How Iterator Pattern provides uniform collection access");
        Console.WriteLine("   • How Composite Pattern handles tree structures");
        Console.WriteLine("   • How both patterns work together beautifully");
        Console.WriteLine("   • ACTUAL CODE examples for both patterns!");
        Console.WriteLine();
        Console.WriteLine("💡 Tip: Take your time at each step to understand the concepts!");
    }

    /// <summary>
    /// Demonstrates what the code would look like without design patterns
    /// Shows why we need the Iterator and Composite patterns
    /// </summary>
    private static void ShowProblemWithoutPatterns()
    {
        Console.WriteLine("🚫 PROBLEM: Without patterns, we have tight coupling and code duplication\n");
        
        Console.WriteLine("Imagine you're a programmer at Objectville Diner...");
        WaitForUser("Press ENTER to see the problematic code...");
        
        Console.WriteLine("\n📄 THE PROBLEMATIC CODE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                    BAD CODE EXAMPLE                        │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        Console.WriteLine("   void PrintAllMenus(PancakeHouseMenu pancakes, DinerMenu diner) {");
        Console.WriteLine("       // Pancake House uses List - we need to know this!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("       for(int i = 0; i < pancakes.GetItems().Count; i++) {");
        Console.WriteLine("           var item = pancakes.GetItems()[i];");
        Console.WriteLine("           Console.WriteLine($\"{item.Name} - {item.Price}\");");
        Console.WriteLine("       }");
        Console.ResetColor();
        Console.WriteLine("       // Diner uses Array - different iteration logic!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("       for(int i = 0; i < diner.GetLength(); i++) {");
        Console.WriteLine("           var item = diner.GetItems()[i];");
        Console.WriteLine("           if (item != null) // Need to check for nulls in array!");
        Console.WriteLine("               Console.WriteLine($\"{item.Name} - {item.Price}\");");
        Console.WriteLine("       }");
        Console.ResetColor();
        Console.WriteLine("   }");
        
        WaitForUser("\nPress ENTER to see what's wrong with this approach...");
        
        Console.WriteLine("\n❌ PROBLEMS WITH THIS APPROACH:");
        Console.WriteLine("   • Client must know internal data structure of each menu");
        Console.WriteLine("   • Adding new menu types requires changing existing code");
        Console.WriteLine("   • Different iteration logic for each collection type");
        Console.WriteLine("   • No uniform way to handle nested menu structures");
        Console.WriteLine("   • Violates the Open/Closed Principle");
        
        Console.WriteLine("\n🤔 What if we had 10 different menu types? 20? The code becomes unmaintainable!");
    }

    /// <summary>
    /// Demonstrates the Iterator pattern with different menu implementations
    /// Shows how we can iterate over different data structures uniformly
    /// </summary>
    private static void IteratorPatternDemo()
    {
        Console.WriteLine("✨ ITERATOR PATTERN TO THE RESCUE!\n");
        Console.WriteLine("The Iterator Pattern provides a way to access elements sequentially");
        Console.WriteLine("without exposing the underlying representation.\n");
        
        WaitForUser("Press ENTER to see the Iterator Pattern structure...");

        ShowIteratorPatternCode();

        WaitForUser("Press ENTER to see the Iterator Pattern in action...");

        // Pancake House uses ArrayList (List<T> in C#)
        Console.WriteLine("\n🏗️  BUILDING THE MENUS:");
        Console.WriteLine("Creating Pancake House Menu (uses List<T> internally)...");
        var pancakeHouseMenu = new PancakeHouseMenu();
        pancakeHouseMenu.AddItem(new MenuItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs and toast", true, 2.99m));
        pancakeHouseMenu.AddItem(new MenuItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", false, 2.99m));
        pancakeHouseMenu.AddItem(new MenuItem("Blueberry Pancakes", "Pancakes made with fresh blueberries", true, 3.49m));
        pancakeHouseMenu.AddItem(new MenuItem("Waffles", "Waffles with your choice of blueberries or strawberries", true, 3.59m));

        Console.WriteLine("Creating Diner Menu (uses Array internally)...");
        // Diner uses Array with fixed size
        var dinerMenu = new DinerMenu(6);
        dinerMenu.AddItem(new MenuItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99m));
        dinerMenu.AddItem(new MenuItem("BLT", "Bacon with lettuce & tomato on whole wheat", false, 2.99m));
        dinerMenu.AddItem(new MenuItem("Soup of the day", "Soup of the day, with a side of potato salad", false, 3.29m));
        dinerMenu.AddItem(new MenuItem("Hotdog", "A hot dog, with sauerkraut, relish, onions, topped with cheese", false, 3.05m));
        dinerMenu.AddItem(new MenuItem("Steamed Veggies and Brown Rice", "Steamed vegetables over brown rice", true, 3.99m));

        WaitForUser("\nPress ENTER to print the Pancake House Menu...");

        Console.WriteLine("\n🥞 PANCAKE HOUSE MENU (using List<T> internally):");
        PrintIteratorMenu(pancakeHouseMenu.CreateIterator());

        WaitForUser("\nPress ENTER to print the Diner Menu...");

        Console.WriteLine("\n🍽️  DINER MENU (using Array internally):");
        PrintIteratorMenu(dinerMenu.CreateIterator());

        WaitForUser("\nPress ENTER to see the magic of the Iterator Pattern...");

        Console.WriteLine("\n✨ THE MAGIC: Notice how the same PrintIteratorMenu() method works for both!");
        
        ShowIteratorClientCode();

        Console.WriteLine("\n✅ ITERATOR PATTERN BENEFITS:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("   • Same PrintIteratorMenu() method works for both menus");
        Console.WriteLine("   • Client code doesn't know about internal data structures");
        Console.WriteLine("   • Easy to add new menu types without changing existing code");
        Console.WriteLine("   • Encapsulates the iteration logic within each collection");
        Console.ResetColor();
    }

    /// <summary>
    /// Demonstrates the Composite pattern with nested menu structure
    /// Shows how we can treat individual items and collections uniformly
    /// </summary>
    private static void CompositePatternDemo()
    {
        Console.WriteLine("🌳 COMPOSITE PATTERN: Building Tree Structures\n");
        Console.WriteLine("What if we want to create nested menus? Sub-menus within menus?");
        Console.WriteLine("The Composite Pattern lets us build tree structures and treat");
        Console.WriteLine("individual objects and compositions uniformly.\n");

        WaitForUser("Press ENTER to see the Composite Pattern structure...");

        ShowCompositePatternCode();

        WaitForUser("Press ENTER to build a complex menu hierarchy...");

        Console.WriteLine("\n🏗️  BUILDING THE MENU HIERARCHY:");
        
        // Create the main menu (root composite)
        Console.WriteLine("Creating main menu container...");
        var allMenus = new Menu("ALL MENUS", "All menus combined");

        // Create sub-menus (composites)
        Console.WriteLine("Creating breakfast, lunch, dinner, and dessert menus...");
        var pancakeHouseMenu = new Menu("PANCAKE HOUSE MENU", "Breakfast");
        var dinerMenu = new Menu("DINER MENU", "Lunch");
        var cafeMenu = new Menu("CAFE MENU", "Dinner");
        var dessertMenu = new Menu("DESSERT MENU", "Dessert of course!");

        // Add sub-menus to main menu
        allMenus.Add(pancakeHouseMenu);
        allMenus.Add(dinerMenu);
        allMenus.Add(cafeMenu);

        Console.WriteLine("Adding menu items to each section...");

        // Add menu items to Pancake House (leaves)
        pancakeHouseMenu.Add(new Composite.MenuItem("K&B's Pancake Breakfast", "Pancakes with scrambled eggs and toast", true, 2.99m));
        pancakeHouseMenu.Add(new Composite.MenuItem("Regular Pancake Breakfast", "Pancakes with fried eggs, sausage", false, 2.99m));
        pancakeHouseMenu.Add(new Composite.MenuItem("Blueberry Pancakes", "Pancakes made with fresh blueberries and blueberry syrup", true, 3.49m));
        pancakeHouseMenu.Add(new Composite.MenuItem("Waffles", "Waffles with your choice of blueberries or strawberries", true, 3.59m));

        // Add menu items to Diner (leaves)
        dinerMenu.Add(new Composite.MenuItem("Vegetarian BLT", "(Fakin') Bacon with lettuce & tomato on whole wheat", true, 2.99m));
        dinerMenu.Add(new Composite.MenuItem("BLT", "Bacon with lettuce & tomato on whole wheat", false, 2.99m));
        dinerMenu.Add(new Composite.MenuItem("Soup of the day", "Soup of the day, with a side of potato salad", false, 3.29m));
        dinerMenu.Add(new Composite.MenuItem("Hotdog", "A hot dog, with sauerkraut, relish, onions, topped with cheese", false, 3.05m));

        WaitForUser("\nPress ENTER to add a nested dessert menu within the diner menu...");
        
        Console.WriteLine("\n🍰 ADDING NESTED STRUCTURE:");
        Console.WriteLine("Adding dessert menu AS A SUBMENU of the diner menu...");
        // Add the dessert submenu to diner menu (composite within composite!)
        dinerMenu.Add(dessertMenu);

        // Add menu items to Cafe (leaves)
        cafeMenu.Add(new Composite.MenuItem("Veggie Burger and Air Fries", "Veggie burger on a whole wheat bun, lettuce, tomato, and fries", true, 3.99m));
        cafeMenu.Add(new Composite.MenuItem("Soup of the day", "A cup of the soup of the day, with a side salad", false, 3.69m));
        cafeMenu.Add(new Composite.MenuItem("Burrito", "A large burrito, with whole pinto beans, salsa, guacamole", true, 4.29m));

        // Add dessert items (leaves)
        dessertMenu.Add(new Composite.MenuItem("Apple Pie", "Apple pie with a flakey crust, topped with vanilla ice cream", true, 1.59m));
        dessertMenu.Add(new Composite.MenuItem("Cheesecake", "Creamy New York cheesecake, with a chocolate graham crust", true, 1.99m));
        dessertMenu.Add(new Composite.MenuItem("Sorbet", "A scoop of raspberry and a scoop of lime", true, 1.89m));

        WaitForUser("Press ENTER to see the complete menu structure...");

        // Create waitress (client)
        var waitress = new Waitress(allMenus);

        Console.WriteLine("\n🍽️  COMPLETE MENU STRUCTURE:");
        Console.WriteLine("Notice how the dessert menu appears nested under the diner menu!");
        waitress.PrintMenu();

        WaitForUser("\nPress ENTER to see Iterator + Composite working together...");

        Console.WriteLine("\n🌱 VEGETARIAN MENU (Iterator traversing Composite structure):");
        Console.WriteLine("Watch how we can iterate through the ENTIRE tree structure!");
        waitress.PrintVegetarianMenu();

        WaitForUser("\nPress ENTER to see how the client code stays simple...");

        ShowCompositeClientCode();

        WaitForUser("\nPress ENTER to see the Composite Pattern benefits...");

        Console.WriteLine("\n✅ COMPOSITE PATTERN BENEFITS:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("   • Builds tree structures of arbitrary complexity");
        Console.WriteLine("   • Uniform treatment of individual objects and compositions");
        Console.WriteLine("   • Iterator pattern works seamlessly with Composite pattern");
        Console.WriteLine("   • The waitress (client) doesn't know the difference between leaves and composites");
        Console.WriteLine("   • Easy to add new components without changing existing code");
        Console.ResetColor();
    }

    private static void ShowIteratorPatternCode()
    {
        Console.WriteLine("📋 ITERATOR PATTERN STRUCTURE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                 ITERATOR PATTERN CODE                       │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("// 1. Iterator Interface");
        Console.ResetColor();
        Console.WriteLine("   public interface IIterator<T> {");
        Console.WriteLine("       bool HasNext();");
        Console.WriteLine("       T Next();");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("// 2. Aggregate Interface");
        Console.ResetColor();
        Console.WriteLine("   public interface IAggregate<T> {");
        Console.WriteLine("       IIterator<T> CreateIterator();");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("// 3. Concrete Iterator (Example: for List)");
        Console.ResetColor();
        Console.WriteLine("   public class PancakeHouseIterator : IIterator<MenuItem> {");
        Console.WriteLine("       private List<MenuItem> _items;");
        Console.WriteLine("       private int _position = 0;");
        Console.WriteLine();
        Console.WriteLine("       public bool HasNext() => _position < _items.Count;");
        Console.WriteLine();
        Console.WriteLine("       public MenuItem Next() {");
        Console.WriteLine("           if (!HasNext()) throw new InvalidOperationException();");
        Console.WriteLine("           return _items[_position++];");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
    }

    private static void ShowIteratorClientCode()
    {
        Console.WriteLine("\n📋 ITERATOR PATTERN - CLIENT CODE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                  CLEAN CLIENT CODE                          │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// The same method works for ANY menu type!");
        Console.ResetColor();
        Console.WriteLine("   void PrintIteratorMenu(IIterator<MenuItem> iterator) {");
        Console.WriteLine("       while (iterator.HasNext()) {");
        Console.WriteLine("           var item = iterator.Next();");
        Console.WriteLine("           Console.WriteLine($\"{item.Name} - ${item.Price}\");");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// Usage - same method, different data structures!");
        Console.ResetColor();
        Console.WriteLine("   PrintIteratorMenu(pancakeMenu.CreateIterator()); // List<T>");
        Console.WriteLine("   PrintIteratorMenu(dinerMenu.CreateIterator());   // Array");
        Console.WriteLine("   PrintIteratorMenu(cafeMenu.CreateIterator());    // Any future type!");
    }

    private static void ShowCompositePatternCode()
    {
        Console.WriteLine("📋 COMPOSITE PATTERN STRUCTURE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│                COMPOSITE PATTERN CODE                       │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("// 1. Component (base class for all menu elements)");
        Console.ResetColor();
        Console.WriteLine("   public abstract class MenuComponent {");
        Console.WriteLine("       public virtual string Name => throw new NotSupportedException();");
        Console.WriteLine("       public virtual decimal Price => throw new NotSupportedException();");
        Console.WriteLine("       public virtual void Add(MenuComponent c) => throw new NotSupportedException();");
        Console.WriteLine("       public virtual void Print() => throw new NotSupportedException();");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("// 2. Leaf (individual menu items)");
        Console.ResetColor();
        Console.WriteLine("   public class MenuItem : MenuComponent {");
        Console.WriteLine("       public override string Name { get; }");
        Console.WriteLine("       public override decimal Price { get; }");
        Console.WriteLine("       public override void Print() {");
        Console.WriteLine("           Console.WriteLine($\"  {Name} - ${Price}\");");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("// 3. Composite (menu containers)");
        Console.ResetColor();
        Console.WriteLine("   public class Menu : MenuComponent {");
        Console.WriteLine("       private List<MenuComponent> _components = new();");
        Console.WriteLine();
        Console.WriteLine("       public override void Add(MenuComponent component) {");
        Console.WriteLine("           _components.Add(component);");
        Console.WriteLine("       }");
        Console.WriteLine();
        Console.WriteLine("       public override void Print() {");
        Console.WriteLine("           Console.WriteLine($\"\\n{Name}\");");
        Console.WriteLine("           foreach(var component in _components)");
        Console.WriteLine("               component.Print(); // Recursive!");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
    }

    private static void ShowCompositeClientCode()
    {
        Console.WriteLine("\n📋 COMPOSITE PATTERN - CLIENT CODE:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("┌─────────────────────────────────────────────────────────────┐");
        Console.WriteLine("│              UNIFORM CLIENT CODE                            │");
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// Client treats leaves and composites uniformly!");
        Console.ResetColor();
        Console.WriteLine("   public class Waitress {");
        Console.WriteLine("       private MenuComponent _allMenus;");
        Console.WriteLine();
        Console.WriteLine("       public void PrintMenu() {");
        Console.WriteLine("           _allMenus.Print(); // Works for entire tree!");
        Console.WriteLine("       }");
        Console.WriteLine();
        Console.WriteLine("       public void PrintVegetarianMenu() {");
        Console.WriteLine("           foreach(var component in _allMenus.CreateIterator()) {");
        Console.WriteLine("               if (component.Vegetarian)");
        Console.WriteLine("                   Console.WriteLine(component.Name);");
        Console.WriteLine("           }");
        Console.WriteLine("       }");
        Console.WriteLine("   }");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("// The magic: Same code handles simple items AND complex hierarchies!");
        Console.ResetColor();
    }

    private static void ShowFinalSummary()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        FINAL SUMMARY                        ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("🎓 WHAT YOU'VE LEARNED:");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🔄 ITERATOR PATTERN:");
        Console.ResetColor();
        Console.WriteLine("   • Provides uniform access to different collection types");
        Console.WriteLine("   • Encapsulates iteration logic within collections");
        Console.WriteLine("   • Makes client code independent of collection implementation");
        Console.WriteLine("   • Follows the Single Responsibility Principle");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("🌳 COMPOSITE PATTERN:");
        Console.ResetColor();
        Console.WriteLine("   • Composes objects into tree structures");
        Console.WriteLine("   • Treats individual objects and compositions uniformly");
        Console.WriteLine("   • Enables recursive operations on tree structures");
        Console.WriteLine("   • Simplifies client code when working with hierarchies");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🤝 PATTERNS WORKING TOGETHER:");
        Console.ResetColor();
        Console.WriteLine("   • Iterator can traverse Composite structures");
        Console.WriteLine("   • Both patterns promote loose coupling");
        Console.WriteLine("   • Both support the Open/Closed Principle");
        Console.WriteLine("   • Real-world applicability in many domains");
        Console.WriteLine();
        
        Console.WriteLine("🎯 KEY TAKEAWAY: Design patterns help us write flexible,");
        Console.WriteLine("   maintainable code that can evolve with changing requirements!");
    }

    /// <summary>
    /// Helper method to print menu using iterator pattern
    /// </summary>
    private static void PrintIteratorMenu(IIterator<MenuItem> iterator)
    {
        while (iterator.HasNext())
        {
            var item = iterator.Next();
            Console.Write($"  {item.Name}");
            if (item.Vegetarian)
                Console.Write("(v)");
            Console.WriteLine($" -- ${item.Price:F2}");
            Console.WriteLine($"     {item.Description}");
        }
    }

    /// <summary>
    /// Helper method to wait for user input with custom message
    /// </summary>
    private static void WaitForUser(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.ReadLine();
    }
}
