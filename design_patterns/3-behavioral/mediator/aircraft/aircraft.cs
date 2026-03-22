
public interface IATCMediator
{
    void Register(Aircraft aircraft);
    void RequestLanding(Aircraft aircraft);
    void NotifyLanded(Aircraft aircraft);
}

// Concrete mediator
public class AirTrafficControl : IATCMediator
{
    private bool _runwayBusy = false;
    private readonly Queue<Aircraft> _waitingQueue = new();

    public void Register(Aircraft aircraft)
    {
        aircraft.SetMediator(this);
    }

    public void RequestLanding(Aircraft aircraft)
    {
        if (!_runwayBusy)
        {
            _runwayBusy = true;
            Console.WriteLine($"[ATC] Runway free  → {aircraft.FlightId} cleared to land.");
            aircraft.Land();
        }
        else
        {
            Console.WriteLine($"[ATC] Runway busy  → {aircraft.FlightId} please hold.");
            _waitingQueue.Enqueue(aircraft);
        }
    }

    public void NotifyLanded(Aircraft aircraft)
    {
        Console.WriteLine($"[ATC] {aircraft.FlightId} has landed. Runway is now free.");

        if (_waitingQueue.Count > 0)
        {
            var next = _waitingQueue.Dequeue();
            Console.WriteLine($"[ATC] Runway free  → {next.FlightId} cleared to land.");
            next.Land();
        }
        else
        {
            _runwayBusy = false;
        }
    }
}

// Colleague — communicates only through the mediator
public class Aircraft(string flightId)
{
    public string FlightId { get; } = flightId;
    private IATCMediator _mediator;

    public void SetMediator(IATCMediator mediator)
    {
        _mediator = mediator;
    }

    public void RequestLanding()
    {
        Console.WriteLine($"[{FlightId}] Requesting landing...");
        _mediator.RequestLanding(this);
    }

    public void Land()
    {
        Console.WriteLine($"[{FlightId}] Landing...");
        Thread.Sleep(1000); // Simulate landing time
        _mediator.NotifyLanded(this);
    }
}

public class Program
{
    public static void Main()
    {
        var atc = new AirTrafficControl();

        var ai101 = new Aircraft("AI101");
        var ai202 = new Aircraft("AI202");
        var ai303 = new Aircraft("AI303");

        atc.Register(ai101);
        atc.Register(ai202);
        atc.Register(ai303);

        ai101.RequestLanding(); // Runway free → lands, triggers queue processing
        Console.WriteLine();
        ai202.RequestLanding(); // Runway free (queue empty after AI101) → lands
        Console.WriteLine();
        ai303.RequestLanding(); // Runway free → lands
    }
}