// Command interface
public interface ICommand
{
    void Execute();
    void Undo();
}

// Receiver — the actual worker
public class Light
{
    public void TurnOn() => Console.WriteLine("Light is ON");
    public void TurnOff() => Console.WriteLine("Light is OFF");
}

// Concrete commands — wrap receiver actions
public class TurnOnCommand : ICommand
{
    private Light _light;
    public TurnOnCommand(Light light) => _light = light;
    public void Execute() => _light.TurnOn();
    public void Undo() => _light.TurnOff();
}

public class TurnOffCommand : ICommand
{
    private Light _light;
    public TurnOffCommand(Light light) => _light = light;
    public void Execute() => _light.TurnOff();
    public void Undo() => _light.TurnOn();
}

// Invoker — triggers commands, knows nothing about Light
public class RemoteControl
{
    private Stack<ICommand> _history = new();

    public void Press(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void PressUndo()
    {
        if (_history.Count > 0)
            _history.Pop().Undo();
    }
}


public class Program
{
    public static void Main()
    {
        // Usage
        var light = new Light();
        var remote = new RemoteControl();

        remote.Press(new TurnOnCommand(light));   // Light is ON
        remote.Press(new TurnOffCommand(light));  // Light is OFF
        remote.PressUndo();                       // Light is ON  (undid TurnOff)
        remote.PressUndo();                       // Light is OFF (undid TurnOn)

    }
}
