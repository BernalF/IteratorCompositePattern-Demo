using System.Collections;

namespace IteratorCompositeDemo.Composite;

/// <summary>
/// Iterator implementation for the Composite pattern
/// Provides depth-first traversal of the menu hierarchy
/// From Head First Design Patterns - demonstrates how to iterate over composite structures
/// </summary>
public class CompositeIterator : IEnumerator<MenuComponent>
{
    private readonly Stack<MenuComponent> _stack = new();
    private MenuComponent? _current;

    public CompositeIterator(MenuComponent root)
    {
        _stack.Push(root);
    }

    public MenuComponent Current => _current ?? throw new InvalidOperationException();

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_stack.Count == 0)
            return false;

        _current = _stack.Pop();

        // If current is a Menu (composite), add its children to the stack in reverse order
        // so they are processed in the correct order (depth-first, left-to-right)
        if (_current is Menu menu)
        {
            // Push children in reverse order for proper traversal
            var children = GetDirectChildren(menu);
            for (int i = children.Count - 1; i >= 0; i--)
            {
                _stack.Push(children[i]);
            }
        }

        return true;
    }

    private List<MenuComponent> GetDirectChildren(Menu menu)
    {
        var children = new List<MenuComponent>();
        int index = 0;
        while (true)
        {
            try
            {
                children.Add(menu.GetChild(index));
                index++;
            }
            catch (ArgumentOutOfRangeException)
            {
                break;
            }
        }
        return children;
    }

    public void Reset()
    {
        throw new NotSupportedException("Reset is not supported");
    }

    public void Dispose()
    {
        // Nothing to dispose
    }
}
