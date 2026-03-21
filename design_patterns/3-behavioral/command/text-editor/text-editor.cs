public interface ICommand
{
    void Execute();
    void Undo();
}

public class WriteCommand : ICommand
{
    private readonly ITextEditor _editor;
    private readonly string _t;
    public WriteCommand(ITextEditor editor, string t)
    {
        _editor = editor;
        _t = t;
    }
    public void Execute() => _editor.Write(_t);
    public void Undo() => _editor.Delete(_t.Length);

}

public class DeleteCommand : ICommand
{
    private readonly ITextEditor _editor;
    private readonly int _count;
    private string _deletedText;

    public DeleteCommand(ITextEditor editor, int count)
    {
        _editor = editor;
        _count = count;
    }

    public void Execute()
    {
        // Capture what is about to be deleted before executing
        _deletedText = _editor.Content.Substring(_editor.Content.Length - _count);
        _editor.Delete(_count);
    }

    // To undo a delete, we write the captured text back
    public void Undo() => _editor.Write(_deletedText);
}

public interface ITextEditor
{
    string Content { get; }
    void Write(string text);
    void Delete(int count);
}
public class TextEditor : ITextEditor
{
    private string _content = "";
    public void Write(string text)
    {
        _content += text;
    }
    public void Delete(int count)
    {
        if (_content.Length < count)
        {
            throw new Exception("Cannot delete count more than the actual string");
        }
        _content = _content.Remove(_content.Length - count, count);
    }

    public string Content => _content;
}

public class Invoker
{
    private Stack<ICommand> _commands = new();


    public void Execute(ICommand command)
    {
        command.Execute();
        _commands.Push(command);

    }
    public void Undo()
    {
        if (_commands.Count > 0)
        {
            ICommand lastCommand = _commands.Pop();
            lastCommand.Undo();
        }

    }

}

public class Program
{
    public static void Main()
    {
        TextEditor editor = new();
        Invoker invoker = new();
        invoker.Execute(new WriteCommand(editor, "Hello"));
        invoker.Execute(new WriteCommand(editor, " World"));
        invoker.Execute(new DeleteCommand(editor, 6));

        Console.WriteLine(editor.Content); // Hello

        invoker.Undo();
        Console.WriteLine(editor.Content); // Hello World

        invoker.Undo();
        Console.WriteLine(editor.Content); // Hello
    }
}