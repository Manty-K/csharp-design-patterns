public class EditorMemento
{
    public string Content { get; }
    public EditorMemento(string content) => Content = content;
}

// Originator — creates and restores from mementos
public class TextEditor
{
    private string _content = "";

    public void Write(string text)
    {
        _content += text;
        Console.WriteLine($"Content: {_content}");
    }

    public EditorMemento Save() => new EditorMemento(_content);

    public void Restore(EditorMemento memento)
    {
        _content = memento.Content;
        Console.WriteLine($"Restored: {_content}");
    }
}

// Caretaker — manages save history
public class History
{
    private Stack<EditorMemento> _snapshots = new();

    public void Push(EditorMemento memento) => _snapshots.Push(memento);

    public EditorMemento Pop()
    {
        if (_snapshots.Count == 0)
            throw new InvalidOperationException("Nothing to undo.");
        return _snapshots.Pop();
    }
}

public class Program
{
    public static void Main()
    {
        var editor = new TextEditor();
        var history = new History();

        history.Push(editor.Save());      // save empty state
        editor.Write("Hello");            // Content: Hello
        history.Push(editor.Save());      // save "Hello"
        editor.Write(" World");           // Content: Hello World
        history.Push(editor.Save());      // save "Hello World"
        editor.Write(" !!!");             // Content: Hello World !!!

        editor.Restore(history.Pop());    // Restored: Hello World
        editor.Restore(history.Pop());    // Restored: Hello
        editor.Restore(history.Pop());
    }
}