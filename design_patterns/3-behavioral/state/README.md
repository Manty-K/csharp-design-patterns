# State

Allow an object to alter its behavior when its internal state changes. The object will appear to change its class.

## When to use it

- An object's behavior depends on its state and must change at runtime.
- Operations have large conditional statements that depend on the object's state — extract each branch into its own state class.
- State transitions need to be explicit and centralized rather than scattered across conditionals.

## How it works

1. Define a **State** interface with methods for every state-dependent action.
2. Implement **concrete states** — each class handles the actions valid in that state and transitions the context to the next state.
3. The **Context** holds a reference to the current state and delegates all state-dependent calls to it.

```csharp
var machine = new VendingMachine();    // starts in IdleState
machine.PressButton();  // [Idle] Insert coin first.
machine.InsertCoin();   // [Idle] Coin inserted. Ready to select.  → transitions to HasCoinState
machine.PressButton();  // [HasCoin] Button pressed. Dispensing... → transitions to DispensingState
machine.Dispense();     // [Dispensing] Item dispensed. Back to idle. → transitions to IdleState
```

## Examples

| File | What it shows |
|---|---|
| `traffic/traffic.cs` | `TrafficLight` (context) cycles through `RedState`, `GreenState`, `YellowState`; each state's `Change()` prints a message and returns the next state |
| `vending-machine/vending-machine.cs` | `VendingMachine` (context) moves through `IdleState`, `HasCoinState`, `DispensingState`; invalid actions in a state print warnings instead of crashing |
