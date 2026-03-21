// Flyweight — stores intrinsic (shared) state
public class TreeType(string name, string texture)
{
    public string Name { get; } = name;
    public string Texture { get; } = texture;

    public void Render(int x, int y) =>  // x,y are extrinsic
        Console.WriteLine($"Rendering {Name} [{Texture}] at ({x},{y})");
}

// Flyweight Factory — reuses existing TreeTypes
public class TreeTypeFactory
{
    private Dictionary<string, TreeType> _types = new();

    public TreeType Get(string name, string texture)
    {
        string key = $"{name}_{texture}";
        if (!_types.ContainsKey(key))
        {
            _types[key] = new TreeType(name, texture);
            Console.WriteLine($"[Factory] Created new TreeType: {name}");
        }
        return _types[key];
    }
}

// Context — holds extrinsic state + reference to flyweight
public class Tree
{
    private int _x, _y;
    private TreeType _type; // shared reference

    public Tree(int x, int y, TreeType type)
    {
        _x = x; _y = y; _type = type;
    }

    public void Render() => _type.Render(_x, _y);
}

public class Program
{

    public static void Main(string[] args)
    {
        var factory = new TreeTypeFactory();

        var trees = new List<Tree>();
        for (int i = 0; i < 5; i++)
            trees.Add(new Tree(i * 10, i * 5, factory.Get("Oak", "oak_texture")));

        trees.Add(new Tree(99, 99, factory.Get("Pine", "pine_texture")));
        trees.Add(new Tree(50, 50, factory.Get("Oak", "oak_texture"))); // reuses Oak

        foreach (var t in trees) t.Render();

    }
}

// Usage

/*
Output:
[Factory] Created new TreeType: Oak      // only once!
[Factory] Created new TreeType: Pine     // only once!
Rendering Oak [oak_texture] at (0,0)
Rendering Oak [oak_texture] at (10,5)
...
Rendering Pine [pine_texture] at (99,99)
Rendering Oak [oak_texture] at (50,50)   // reused, no new object
*/