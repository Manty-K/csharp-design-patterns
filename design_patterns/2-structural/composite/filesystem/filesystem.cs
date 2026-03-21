
using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        var root = new Folder("root");
var src = new Folder("src");
src.Add(new File("Program.cs"));
src.Add(new File("Utils.cs"));

var assets = new Folder("assets");
assets.Add(new File("logo.png"));

root.Add(src);
root.Add(assets);
root.Add(new File("README.md"));

root.Print();
    }
}
// Common interface for both files and folders
public interface IFileSystemItem {
    string Name { get; }
    void Print(string indent = "");
}

// Leaf — no children
public class File : IFileSystemItem {
    public string Name { get; }
    public File(string name) => Name = name;
    public void Print(string indent = "") =>
        Console.WriteLine($"{indent}📄 {Name}");
}

// Composite — has children (can be files or folders)
public class Folder : IFileSystemItem {
    public string Name { get; }
    private List<IFileSystemItem> _children = new List<IFileSystemItem>();

    public Folder(string name) => Name = name;

    public void Add(IFileSystemItem item) => _children.Add(item);

    public void Print(string indent = "") {
        Console.WriteLine($"{indent}📁 {Name}");
        foreach (var child in _children)
            child.Print(indent + "  ");
    }
}
