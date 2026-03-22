// State interface
public interface IVendingMachineState
{
    void InsertCoin(VendingMachine machine);
    void PressButton(VendingMachine machine);
    void Dispense(VendingMachine machine);
}

// Context — holds current state
public class VendingMachine
{
    public IVendingMachineState CurrentState { get; set; }

    public VendingMachine()
    {
        CurrentState = new IdleState(); // initial state
    }

    public void InsertCoin() => CurrentState.InsertCoin(this);
    public void PressButton() => CurrentState.PressButton(this);
    public void Dispense() => CurrentState.Dispense(this);
}

// Concrete states
public class IdleState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine)
    {
        Console.WriteLine("[Idle] Coin inserted. Ready to select.");
        machine.CurrentState = new HasCoinState();
    }
    public void PressButton(VendingMachine machine) =>
        Console.WriteLine("[Idle] Insert coin first.");
    public void Dispense(VendingMachine machine) =>
        Console.WriteLine("[Idle] No coin inserted.");
}

public class HasCoinState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine) =>
        Console.WriteLine("[HasCoin] Coin already inserted.");
    public void PressButton(VendingMachine machine)
    {
        Console.WriteLine("[HasCoin] Button pressed. Dispensing...");
        machine.CurrentState = new DispensingState();
    }
    public void Dispense(VendingMachine machine) =>
        Console.WriteLine("[HasCoin] Press button to dispense.");
}

public class DispensingState : IVendingMachineState
{
    public void InsertCoin(VendingMachine machine) =>
        Console.WriteLine("[Dispensing] Please wait.");
    public void PressButton(VendingMachine machine) =>
        Console.WriteLine("[Dispensing] Already dispensing.");
    public void Dispense(VendingMachine machine)
    {
        Console.WriteLine("[Dispensing] Item dispensed. Back to idle.");
        machine.CurrentState = new IdleState();
    }
}

public class Program
{
    public static void Main()
    {
        var machine = new VendingMachine();
        machine.PressButton(); // [Idle] Insert coin first.
        machine.InsertCoin();  // [Idle] Coin inserted. Ready to select.
        machine.PressButton(); // [HasCoin] Button pressed. Dispensing...
        machine.Dispense();
    }
}
