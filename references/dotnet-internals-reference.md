# .NET Internals — Reference Guide

> Personal reference for interview preparation.  
> Covers: Delegates, Multicast Delegates, Events, Action & Func, Threading, Tasks, async/await, TPL & PLINQ

---

## Table of Contents
1. [Delegate](#1-delegate)
2. [Multicast Delegate](#2-multicast-delegate)
3. [Event](#3-event)
4. [Action & Func](#4-action--func)
5. [Threading](#5-threading)
6. [Task & async/await](#6-task--asyncawait)
7. [TPL & PLINQ](#7-tpl--plinq)

---

## 1. Delegate

### Concept
A delegate is a **type-safe function pointer**. It stores a reference to a method and allows you to call it later. The delegate type must match the method signature exactly (same return type + same parameters).

### Code Example
```csharp
delegate int MathOperation(int a, int b);

public static int Multiply(int a, int b) => a * b;
public static int Add(int a, int b) => a + b;

MathOperation op = Multiply;
Console.WriteLine(op(4, 5)); // 20

op = Add;
Console.WriteLine(op(4, 5)); // 9
```

### Problem Statement
> Create a delegate `MathOperation` that takes two `int`s and returns an `int`.
> Write two methods — `Add` and `Multiply`.
> Assign each to the delegate and print the result for inputs `4` and `5`.

### Interview Tips
- A delegate is a **type** — you declare it like a class
- Return type is part of the signature — `void` vs `int` are different delegates
- Foundation for `Action`, `Func`, and `event` — everything builds on this
- Common question: *"What's the difference between a delegate and an interface?"* — Delegate = single method reference. Interface = contract with multiple members.

---

## 2. Multicast Delegate

### Concept
A delegate that holds **multiple method references** and invokes them all in order when called. Use `+=` to add methods and `-=` to remove them.

### Code Example
```csharp
delegate void Logger(string message);

public static void LogToConsole(string msg) => Console.WriteLine($"[Console]: {msg}");
public static void LogToFile(string msg) => Console.WriteLine($"[File]: {msg}");
public static void LogToDatabase(string msg) => Console.WriteLine($"[Database]: {msg}");

Logger logger = LogToConsole;
logger += LogToFile;
logger += LogToDatabase;

logger("User logged in");
// [Console]: User logged in
// [File]: User logged in
// [Database]: User logged in

logger -= LogToFile;

logger("User logged out");
// [Console]: User logged out
// [Database]: User logged out
```

### Problem Statement
> Create a multicast delegate `Logger` that takes a `string`.
> Wire up 3 methods: `LogToConsole`, `LogToFile`, `LogToDatabase`.
> Invoke with `"User logged in"`, then remove `LogToFile` and invoke again with `"User logged out"`.

### Interview Tips
- All methods are called **in registration order**
- If the delegate has a **return type**, only the last method's return value is captured — others are discarded
- This is exactly how C# `event` works under the hood
- Common question: *"What happens if you call a null delegate?"* — `NullReferenceException`. Always null-check before invoking.

---

## 3. Event

### Concept
An event is a **multicast delegate with access restrictions**. Outside classes can only subscribe (`+=`) or unsubscribe (`-=`) — they cannot invoke the event directly. This enforces proper publisher/subscriber encapsulation.

### Delegate vs Event
```csharp
// Delegate — anyone can invoke from outside (dangerous)
public Logger OnLog;
OnLog("hacked!"); // outsider can fire it ❌

// Event — only the owning class can invoke it
public event Logger OnLog;
OnLog("hacked!"); // compile error from outside ✅
```

### Code Example
```csharp
public class Order
{
    public event Action<string> OrderPlaced;

    public void PlaceOrder(string orderID)
    {
        Console.WriteLine($"Placed Order: {orderID}");
        OrderPlaced?.Invoke(orderID); // ?. = safe invoke, won't crash if no subscribers
    }
}

// In Main:
Order order = new Order();
order.OrderPlaced += SendSms;
order.OrderPlaced += SendEmail;
order.PlaceOrder("abc123");

public static void SendSms(string message) =>
    Console.WriteLine($"[SMS] Order confirmed: {message}");

public static void SendEmail(string message) =>
    Console.WriteLine($"[Email] Order confirmed: {message}");
```

### Problem Statement
> Create an `Order` class with an `event Action<string>` called `OnOrderPlaced`.
> When `PlaceOrder(string item)` is called, it fires the event.
> Subscribe two handlers: one prints `[Email] Order confirmed: {item}`, other prints `[SMS] Order confirmed: {item}`.

### Interview Tips
- Always use `?.Invoke()` — safe call if no subscribers are attached
- `event` = delegate + encapsulation. The owning class is the only publisher
- This is the **Observer pattern** built natively into C#
- Common question: *"Why use event over a public delegate?"* — Events prevent external code from invoking or resetting the delegate directly

---

## 4. Action & Func

### Concept
Built-in generic delegates — no need to declare your own every time.

| Type | Returns | Signature Example |
|---|---|---|
| `Action` | `void` | `Action<string>` — takes string, returns nothing |
| `Func` | a value | `Func<int, int, int>` — takes 2 ints, returns int |

> **Rule:** In `Func`, the **last type parameter is always the return type**.

### Code Example
```csharp
// Action — void return
Func<int, int> square = x => x * x;
Console.WriteLine(square(6)); // 36

// Action with multiple params
Action<string, string> greet = (fname, lname) =>
    Console.WriteLine($"Hello {fname} {lname}");
greet("Manthan", "K");

// Func with a list
Func<List<int>, int> sumList = ls =>
{
    int total = 0;
    ls.ForEach(n => total += n);
    return total;
};
Console.WriteLine(sumList(new List<int> { 3, 4 })); // 7
```

### Problem Statement
> - Create a `Func<int, int>` that returns the **square** of a number. Print result for `6`.
> - Create an `Action<string, string>` that prints `"Hello {firstName} {lastName}"`.
> - Create a `Func<List<int>, int>` that returns the **sum** of the list.

### Interview Tips
- `Action` = `delegate void`, `Func` = `delegate T` — just built-in shortcuts
- Use `Action`/`Func` by default. Only use custom delegates when you need a descriptive name for clarity
- `Func<int>` with no input = a method that takes nothing and returns int
- Common question: *"What is a Predicate<T>?"* — It's `Func<T, bool>`, a shorthand for boolean-returning delegates (used in `.Where()`, `.Find()`, etc.)

---

## 5. Threading

### Concept
Running code **in parallel on separate threads** instead of waiting sequentially. Each `Thread` runs independently on the CPU. Use `Join()` to make the main thread wait for others to finish. Use `lock` to prevent race conditions when threads share data.

### Code Example
```csharp
using System.Threading;

protected static void DoTask(string name, int length)
{
    for (int i = 0; i < length; i++)
    {
        Console.WriteLine($"{name} is working {i} on thread {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(100);
    }
}

Thread ta = new Thread(() => DoTask("A", 5));
Thread tb = new Thread(() => DoTask("B", 5));

ta.Start();
tb.Start();

ta.Join(); // Main waits for A
tb.Join(); // Main waits for B

Console.WriteLine("All threads done!");
```

### Lock Example (Race Condition Prevention)
```csharp
int counter = 0;
object _lock = new object();

void Increment()
{
    for (int i = 0; i < 1000; i++)
    {
        lock (_lock) // only one thread enters at a time
        {
            counter++;
        }
    }
}

Thread t1 = new Thread(Increment);
Thread t2 = new Thread(Increment);
t1.Start(); t2.Start();
t1.Join();  t2.Join();

Console.WriteLine(counter); // always 2000, never corrupted
```

### Problem Statement
> Create two threads — `Thread-A` and `Thread-B`.
> Each prints `"{name} is working... {i}"` for i = 1 to 5 with `Thread.Sleep(100)`.
> Main thread waits for both, then prints `"All threads done!"`.

### Interview Tips
- `Thread.Join()` = block main thread until that thread finishes
- Without `lock`, shared state = unpredictable results (race condition)
- `Thread.Sleep()` blocks the **current** thread, not others
- Common question: *"What is a deadlock?"* — Two threads waiting on each other's lock forever. Avoid by always acquiring locks in the same order.
- Common question: *"Thread vs Task?"* — Thread is manual and heavy. Task uses a thread pool, is lighter, and integrates with async/await.

---

## 6. Task & async/await

### Concept
`Task` is a higher-level abstraction over threads — it uses a **thread pool** under the hood. `async/await` makes asynchronous code readable and non-blocking. Best for **I/O-bound** operations (API calls, file reads, DB queries).

### Code Example
```csharp
using System.Threading.Tasks;

public static async Task DownloadFile(string fileName, int delayMs)
{
    Console.WriteLine($"Downloading...{fileName}");
    await Task.Delay(delayMs); // non-blocking wait
    Console.WriteLine($"Downloaded {fileName}");
}

public static async Task Main(string[] args)
{
    Task f1 = DownloadFile("file1.mp4", 5000);
    Task f2 = DownloadFile("file2.mp4", 500);
    Task f3 = DownloadFile("file3.mp4", 2000);

    await Task.WhenAll(f1, f2, f3); // runs all 3 concurrently
    Console.WriteLine("All downloads finished!");
}
```

Output order: `file2` (500ms) → `file3` (2000ms) → `file1` (5000ms) — proof of concurrency.

### Key Methods
| Method | Behaviour |
|---|---|
| `Task.Run()` | Runs on thread pool |
| `Task.Delay()` | Non-blocking wait |
| `Task.WhenAll()` | Wait for all tasks to complete |
| `Task.WhenAny()` | Wait for first task to complete |
| `task.Wait()` | Blocking wait (avoid in async code) |
| `task.Result` | Blocking value get (avoid in async code) |

### Problem Statement
> Write an `async` method `DownloadFile(string fileName)` that prints `"Downloading {fileName}..."`, waits 1 second, then prints `"Downloaded {fileName}!"`.
> Call it for 3 files concurrently using `Task.WhenAll`.

### Interview Tips
- `await` pauses **that method only** — it does not block the thread
- Never use `.Result` or `.Wait()` in async code — causes deadlocks
- `async void` is only for event handlers — always use `async Task` otherwise
- `Task.WhenAll` = run concurrently. Calling `await` on each separately = sequential
- Common question: *"What's the difference between Task.WhenAll and Task.WhenAny?"* — `WhenAll` waits for every task. `WhenAny` returns as soon as the first one completes.

---

## 7. TPL & PLINQ

### Concept
**TPL (Task Parallel Library)** provides high-level parallelism for **CPU-bound** work — processing large datasets across multiple cores simultaneously. **PLINQ** is the parallel version of LINQ — just add `.AsParallel()`.

| | Best for |
|---|---|
| `async/await` | I/O-bound (waiting on network, disk, DB) |
| `TPL / Parallel` | CPU-bound (heavy computation, large data processing) |

### Code Example — Parallel.ForEach
```csharp
using System.Threading.Tasks;

var reports = new List<string> { "Report 1", "Report 2", "Report 3", "Report 4" };

Parallel.ForEach(reports, report =>
{
    Console.WriteLine($"Generating {report} on thread {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(1000); // simulate heavy work
});
```

### Code Example — PLINQ
```csharp
var numbers = Enumerable.Range(1, 20);

var divisibleBy3 = numbers
    .AsParallel()
    .Where(n => n % 3 == 0)
    .ToList();

Console.WriteLine(string.Join(", ", divisibleBy3));
// 3, 6, 9, 12, 15, 18
```

### Problem Statement
> You have a list of 6 report names (`"Report1"` through `"Report6"`).
> Use `Parallel.ForEach` to generate each — print the report name and thread ID.
> Then using PLINQ, filter numbers 1–20 divisible by 3 and print them.

### Interview Tips
- Different thread IDs in output = proof multiple cores are working
- `Parallel.For` / `Parallel.ForEach` automatically partitions work across cores
- PLINQ can give **unordered results** — use `.AsOrdered()` if order matters
- Common question: *"When would you NOT use PLINQ?"* — For small datasets. Parallelism has overhead — it's only faster when the data is large enough to justify it.
- Common question: *"Parallel.ForEach vs Task.WhenAll?"* — `Parallel.ForEach` = CPU-bound sync work. `Task.WhenAll` = concurrent async (I/O-bound) work.

---

## Quick Reference Cheat Sheet

```
Delegate          → type-safe method reference
Multicast         → delegate holding multiple methods (+=, -=)
Event             → multicast delegate with invoke restriction
Action<T>         → built-in delegate, void return
Func<T, TResult>  → built-in delegate, returns a value
Thread            → low-level, manual parallelism
Task              → high-level, thread pool, async-friendly
async/await       → non-blocking I/O-bound async code
Parallel          → CPU-bound parallel processing
PLINQ             → parallel LINQ (.AsParallel())
```

---

*Generated for interview preparation — .NET Internals*
