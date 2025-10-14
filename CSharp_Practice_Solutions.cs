// C# Practice — Iterator & Composite

// ============ A) Iterator para Dictionary<string, Product> ============
// Requerimiento: Recorrer los valores en orden de clave ascendente.
public interface IIterator<T> { bool HasNext(); T Next(); }
public interface IAggregate<T> { IIterator<T> CreateIterator(); }

public class Product { public string Name { get; set; } public decimal Price { get; set; } }

public class ProductCatalog : IAggregate<Product> {
    private readonly Dictionary<string, Product> _map = new();
    public void Add(string key, Product p) => _map[key] = p;
    public IIterator<Product> CreateIterator() => new CatalogIterator(_map);
}

public class CatalogIterator : IIterator<Product> {
    private readonly List<string> _keys;
    private readonly Dictionary<string, Product> _map;
    private int _index = 0;
    public CatalogIterator(Dictionary<string, Product> map) {
        _map = map;
        _keys = _map.Keys.OrderBy(k => k).ToList(); // define orden explícito
    }
    public bool HasNext() => _index < _keys.Count;
    public Product Next() {
        if (!HasNext()) throw new InvalidOperationException();
        return _map[_keys[_index++]];
    }
}

// ============ B) Composite para UI (Draw y FindById) ============
public abstract class View {
    public virtual string Id => throw new NotSupportedException();
    public virtual void Add(View c) => throw new NotSupportedException();
    public virtual void Remove(View c) => throw new NotSupportedException();
    public virtual void Draw() => throw new NotSupportedException();
    public virtual View? FindById(string id) => throw new NotSupportedException();
}

public class Label : View {
    private readonly string _id, _text;
    public Label(string id, string text) { _id = id; _text = text; }
    public override string Id => _id;
    public override void Draw() => Console.WriteLine($"Label({_id}): {_text}");
    public override View? FindById(string id) => _id == id ? this : null;
}

public class Panel : View {
    private readonly string _id;
    private readonly List<View> _children = new();
    public Panel(string id) { _id = id; }
    public override string Id => _id;
    public override void Add(View c) => _children.Add(c);
    public override void Draw() {
        Console.WriteLine($"Panel({_id}) {{");
        foreach (var c in _children) c.Draw();
        Console.WriteLine("}");
    }
    public override View? FindById(string id) {
        if (_id == id) return this;
        foreach (var c in _children) {
            var found = c.FindById(id);
            if (found != null) return found;
        }
        return null;
    }
}

// ============ C) Iterator + Composite para Sistema de Archivos ============
public abstract class Node {
    public virtual string Name => throw new NotSupportedException();
    public virtual void Add(Node n) => throw new NotSupportedException();
    public virtual IEnumerable<Node> Children() => throw new NotSupportedException();
}

public class FileNode : Node {
    private readonly string _name;
    public FileNode(string name) { _name = name; }
    public override string Name => _name;
}

public class FolderNode : Node {
    private readonly string _name;
    private readonly List<Node> _children = new();
    public FolderNode(string name) { _name = name; }
    public override string Name => _name;
    public override void Add(Node n) => _children.Add(n);
    public override IEnumerable<Node> Children() => _children;
}

// Iterador DFS (externo) sobre el árbol de Node
public class DfsIterator : IIterator<Node> {
    private readonly Stack<IEnumerator<Node>> _stack = new();
    private Node? _current;

    public DfsIterator(Node root) {
        _current = root;
        if (root is FolderNode f) _stack.Push(f.Children().GetEnumerator());
        else _stack.Push(Enumerable.Empty<Node>().GetEnumerator());
    }

    public bool HasNext() {
        return _current != null || _stack.Count > 0;
    }

    public Node Next() {
        if (_current != null) {
            var result = _current;
            _current = null;
            return result;
        }
        while (_stack.Count > 0) {
            var it = _stack.Peek();
            if (it.MoveNext()) {
                var node = it.Current;
                if (node is FolderNode folder) {
                    _stack.Push(folder.Children().GetEnumerator());
                }
                return node;
            } else {
                _stack.Pop();
            }
        }
        throw new InvalidOperationException();
    }
}

// Uso con filtro:
void PrintLogs(Node root) {
    var it = new DfsIterator(root);
    while (it.HasNext()) {
        var node = it.Next();
        if (node is FileNode f && f.Name.EndsWith(".log", StringComparison.OrdinalIgnoreCase)) {
            Console.WriteLine(f.Name);
        }
    }
}

