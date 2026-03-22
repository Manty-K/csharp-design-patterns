# Advanced C# — Reference Guide

> Personal reference for interview preparation.  
> Covers: Generics, LINQ, async/await, Nullable, Records, Pattern Matching, Extension Methods, yield return, IDisposable, ref/out/in, Tuples, Local Functions, Reflection

---

## Table of Contents
1. [Generics](#1-generics)
2. [LINQ](#2-linq)
3. [async / await](#3-async--await)
4. [Nullable](#4-nullable)
5. [Records](#5-records)
6. [Pattern Matching](#6-pattern-matching)
7. [Extension Methods](#7-extension-methods)
8. [yield return / IEnumerable](#8-yield-return--ienumerable)
9. [IDisposable + using](#9-idisposable--using)
10. [ref / out / in](#10-ref--out--in)
11. [Tuples](#11-tuples)
12. [Local Functions](#12-local-functions)
13. [Reflection](#13-reflection)

---

## 1. Generics

### Concept
Write a class or method **once** that works with **any type** — instead of rewriting for `int`, `string`, etc. The compiler enforces type safety at compile time.

### Code Example
```csharp
public class Stack<T>
{
    List<T> items = new List<T>();

    public void Push(T item)
    {
        Console.WriteLine($"Pushing {item} onto the stack.");
        items.Add(item);
    }

    public T Pop()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Stack is empty.");
        var item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    public T Peek()
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Stack is empty.");
        return items[items.Count - 1];
    }

    public int Count => items.Count;
}

// Usage
var stackInt = new Stack<int>();
stackInt.Push(10);
stackInt.Push(20);
Console.WriteLine(stackInt.Peek());  // 20
Console.WriteLine(stackInt.Pop());   // 20
Console.WriteLine(stackInt.Count);   // 1

var stackString = new Stack<string>();
stackString.Push("Hello");
stackString.Push("World");
Console.WriteLine(stackString.Pop()); // World
```

### Constraints
```csharp
// T must have a parameterless constructor
public T Create<T>() where T : new() => new T();

// T must implement an interface
public void Print<T>(T item) where T : IPrintable => item.Print();

// T must be a value type
public void Show<T>(T val) where T : struct => Console.WriteLine(val);
```

### Problem Statement
> Create a generic class `Stack<T>` with `Push`, `Pop`, `Peek`, and `Count`.
> Test with both `int` and `string` types.

### Interview Tips
- `T` is a placeholder — replaced at compile time with actual type
- Generics avoid boxing/unboxing — better performance than `object`
- Common question: *"What's the difference between `List<T>` and `ArrayList`?"* — `List<T>` is generic (type-safe, no boxing). `ArrayList` stores `object` (not type-safe, slower).
- Common question: *"What are generic constraints?"* — `where T : class`, `where T : new()`, `where T : IInterface` — they restrict what types can be used as `T`.

---

## 2. LINQ

### Concept
**Language Integrated Query** — query collections directly in C# with clean, readable syntax. No manual loops for filtering, sorting, or transforming.

### Code Example
```csharp
var products = new List<Product>
{
    new Product { Name = "Laptop",  Category = "Electronics", Price = 80000 },
    new Product { Name = "Phone",   Category = "Electronics", Price = 40000 },
    new Product { Name = "Desk",    Category = "Furniture",   Price = 15000 },
    new Product { Name = "Chair",   Category = "Furniture",   Price = 8000  },
    new Product { Name = "Monitor", Category = "Electronics", Price = 25000 },
};

// 1. All Electronics sorted by price descending
products.Where(p => p.Category == "Electronics")
        .OrderByDescending(p => p.Price)
        .ToList()
        .ForEach(p => Console.WriteLine(p));

// 2. Most expensive product
var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
Console.WriteLine($"Most expensive: {mostExpensive}");

// 3. Average price per category
var avgByCategory = products
    .GroupBy(p => p.Category)
    .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.Price) });

foreach (var item in avgByCategory)
    Console.WriteLine($"{item.Category}: ${item.AveragePrice:F2}");

// 4. Names of products over 20000
products.Where(p => p.Price > 20000)
        .Select(p => p.Name)
        .ToList()
        .ForEach(Console.WriteLine);
```

### Most Used LINQ Methods
```csharp
var nums = new List<int> { 5, 3, 8, 1, 9, 2, 7 };

nums.Where(n => n > 4)               // filter
nums.Select(n => n * 2)              // transform
nums.OrderBy(n => n)                 // sort ascending
nums.OrderByDescending(n => n)       // sort descending
nums.GroupBy(n => n % 2)             // group
nums.Sum()                           // 35
nums.Max()                           // 9
nums.Min()                           // 1
nums.Average()                       // 5.0
nums.First(n => n > 4)               // first match
nums.FirstOrDefault(n => n > 100)    // null/default if not found
nums.Any(n => n > 8)                 // true if any match
nums.All(n => n > 0)                 // true if all match
```

### Problem Statement
> Given a list of products, write LINQ to:
> 1. Get all Electronics sorted by price descending
> 2. Get the most expensive product name
> 3. Get average price per category
> 4. Get names of products where price > 20000

### Interview Tips
- LINQ methods are **extension methods** on `IEnumerable<T>` — LINQ itself is built using extension methods
- `FirstOrDefault` vs `First` — `First` throws if empty, `FirstOrDefault` returns null/default
- LINQ is **lazy** by default — execution happens when you call `.ToList()`, `.ToArray()`, or iterate
- Common question: *"What's the difference between `IEnumerable` and `IQueryable`?"* — `IEnumerable` executes in-memory (LINQ to Objects). `IQueryable` translates to SQL (LINQ to SQL/EF).

---

## 3. async / await

### Concept
Write **non-blocking** code that looks sequential. When you `await` something, the thread is freed to do other work instead of sitting idle waiting.

### Code Example
```csharp
public class ProductManager
{
    List<Product> Products;

    public ProductManager(List<Product> products) => Products = products;

    public async Task<double> GetProductPrice(string name)
    {
        await Task.Delay(1000); // simulate async DB call
        var price = Products
            .Where(p => p.Name == name)
            .Select(p => p.Price)
            .FirstOrDefault();
        return (double)price;
    }
}

// In Main — run all 3 concurrently
static async Task Main(string[] args)
{
    var manager = new ProductManager(products);

    var lp = manager.GetProductPrice("Laptop");
    var dp = manager.GetProductPrice("Desk");
    var cc = manager.GetProductPrice("Chair");

    var results = await Task.WhenAll(lp, dp, cc);

    Console.WriteLine($"Laptop: {results[0]}");
    Console.WriteLine($"Desk:   {results[1]}");
    Console.WriteLine($"Chair:  {results[2]}");
}
```

### Sequential vs Concurrent
```csharp
// Sequential — total ~3s ❌
var r1 = await GetProductPrice("Laptop"); // wait 1s
var r2 = await GetProductPrice("Desk");   // wait 1s
var r3 = await GetProductPrice("Chair");  // wait 1s

// Concurrent — total ~1s ✅
var t1 = GetProductPrice("Laptop"); // start all
var t2 = GetProductPrice("Desk");
var t3 = GetProductPrice("Chair");
var results = await Task.WhenAll(t1, t2, t3); // wait for all
```

### Problem Statement
> Write an async method `GetProductPrice(string name)` that simulates a DB fetch with `Task.Delay(1000)`.
> Call it for 3 products concurrently using `Task.WhenAll` and print all results.

### Interview Tips
- `await` pauses **that method only** — does not block the thread
- Never use `.Result` or `.Wait()` in async code — causes deadlocks
- `async void` is only for event handlers — always use `async Task` otherwise
- `Task.WhenAll` = concurrent. Awaiting each separately = sequential
- Common question: *"What's the difference between Task.WhenAll and Task.WhenAny?"* — `WhenAll` waits for every task. `WhenAny` returns when the first one completes.

---

## 4. Nullable

### Concept
Value types (`int`, `bool`, `struct`) can't be `null` by default. Add `?` to allow null. Use `??`, `??=`, and `?.` to handle null safely.

### Code Example
```csharp
int? age = null;

// ?? — default if null
int result = age ?? 0; // 0

// ??= — assign only if null
age ??= 18; // now age = 18

// ?. — null conditional, no crash
string name = null;
Console.WriteLine(name?.ToUpper());    // null, no NullReferenceException
Console.WriteLine(name?.Length ?? 0); // 0

// With records
var employees = new List<Employee>
{
    new("Alice", "Engineering", 90000),
    new("Bob",   "Marketing",   null),
};

employees.ForEach(e =>
    Console.WriteLine($"{e.Name}: {e.Salary?.ToString() ?? "Unpaid"}"));
// Alice: 90000
// Bob: Unpaid
```

### Interview Tips
- `int?` is shorthand for `Nullable<int>`
- `?.` (null conditional) chains safely — stops and returns null at first null
- `??` (null coalescing) provides a fallback value
- Common question: *"What's the difference between `??` and `?.`?"* — `?.` accesses a member safely. `??` provides a default when expression is null.

---

## 5. Records

### Concept
A concise way to define **immutable data types**. The compiler auto-generates `Equals`, `GetHashCode`, `ToString`, and supports `with` for non-destructive mutation.

### Code Example
```csharp
public record Employee(string Name, string Department, int? Salary);

var alice  = new Employee("Alice", "Engineering", 90000);
var alice2 = new Employee("Alice", "Engineering", 90000);

Console.WriteLine(alice == alice2); // True  — value equality
Console.WriteLine(alice);           // Employee { Name = Alice, Department = Engineering, Salary = 90000 }

// Non-destructive mutation — creates a new record
var promoted = alice with { Salary = 95000 };
Console.WriteLine(promoted); // Employee { Name = Alice, Department = Engineering, Salary = 95000 }
Console.WriteLine(alice);    // original unchanged
```

### Problem Statement
> Define an `Employee` record with `Name`, `Department`, `int? Salary`.
> Create a list, print salaries (null = "Unpaid"), then copy Alice with updated salary using `with`.

### Interview Tips
- Records are **immutable by default** — properties are `init`-only
- `==` compares **values**, not references (unlike classes)
- `with` creates a **copy** with specified properties changed — original untouched
- Common question: *"When would you use a record vs a class?"* — Records for immutable data models (DTOs, API responses). Classes for mutable objects with behavior.

---

## 6. Pattern Matching

### Concept
Elegantly match values against **types, properties, or conditions** using `is` and `switch` expressions — much cleaner than chains of `if/else`.

### Code Example
```csharp
public record Employee(string Name, string Department, int? Salary);

var employees = new List<Employee>
{
    new("Alice", "Engineering", 90000),
    new("Bob",   "Marketing",   null),
    new("Carol", "Engineering", 120000),
    new("Dave",  "Intern",      null),
};

// Switch expression with property pattern
employees.ForEach(e =>
{
    var bonus = e switch
    {
        { Department: "Engineering" } => "20% bonus",
        { Department: "Marketing"  } => "10% bonus",
        { Department: "Intern"     } => "No bonus",
        _                            => "5% bonus"   // default
    };
    Console.WriteLine($"{e.Name} gets {bonus}");
});

// Type pattern
object obj = "hello";
if (obj is string s)
    Console.WriteLine(s.ToUpper()); // HELLO

// Combined property + condition pattern
var product = new Product { Price = 80000, Category = "Electronics" };
string label = product switch
{
    { Price: > 50000, Category: "Electronics" } => "Premium Electronics",
    { Price: < 10000 }                          => "Budget",
    _                                           => "Standard"
};
```

### Problem Statement
> Use a switch expression on `Department` to assign bonuses to all employees:
> Engineering → 20%, Marketing → 10%, Intern → No bonus, else → 5%.

### Interview Tips
- `_` is the discard pattern — matches anything (default case)
- Property patterns `{ Prop: value }` match on object properties directly
- Switch expressions return a value — much cleaner than switch statements
- Common question: *"What's the difference between switch statement and switch expression?"* — Statement executes code blocks. Expression returns a value, more concise.

---

## 7. Extension Methods

### Concept
Add new methods to an **existing class without modifying it** — even built-in types like `string`, `int`, `List<T>`. Must be in a `static` class with a `static` method where the first parameter uses `this`.

### Code Example
```csharp
public static class ListExtension
{
    // returns true if list empty
    public static bool IsEmpty(this List<int> list) => list.Count == 0;

    // returns second element or throws
    public static int Second(this List<int> list)
    {
        if (list.Count < 2)
            throw new InvalidOperationException("List must have at least 2 elements.");
        return list[1];
    }

    // returns comma-separated string
    public static string ToCommaSeparatedString(this List<int> list)
    {
        if (list.Count == 0) return string.Empty;
        return string.Join(", ", list);
    }
}

// Usage — called like instance methods
var numbers = new List<int> { 1, 2, 3, 4, 5 };
Console.WriteLine(numbers.IsEmpty());               // false
Console.WriteLine(numbers.Second());                // 2
Console.WriteLine(numbers.ToCommaSeparatedString()); // 1, 2, 3, 4, 5
```

### Problem Statement
> Create extension methods for `List<int>`:
> - `IsEmpty()` — true if no elements
> - `Second()` — second element or exception
> - `ToCommaSeparated()` — `"1, 2, 3"` style string

### Interview Tips
- `this` before first parameter = this is an extension method
- LINQ methods (`.Where()`, `.Select()`, `.Sum()`) are all extension methods on `IEnumerable<T>`
- Extension methods don't modify the original class — purely additive
- Common question: *"Can extension methods access private members?"* — No. They can only access public members, just like any external code.

---

## 8. yield return / IEnumerable

### Concept
**Lazy evaluation** — generate sequence items one at a time on demand instead of building the whole collection in memory upfront. Method pauses at `yield return` and resumes on next iteration.

### Code Example
```csharp
public static IEnumerable<int> GetFibonacci()
{
    int a = 0, b = 1;
    while (true)
    {
        yield return a;        // return current, pause here
        (a, b) = (b, a + b);   // resume here on next call
    }
}

// Usage
var fibonacciNumbers = GetFibonacci().Take(10).ToList();
// 0, 1, 1, 2, 3, 5, 8, 13, 21, 34

// Even Fibonacci numbers
fibonacciNumbers
    .Where(n => n % 2 == 0)
    .ToList()
    .ForEach(Console.WriteLine);
// 0, 2, 8, 34

// Sum
int sum = fibonacciNumbers.Sum();
Console.WriteLine($"Sum: {sum}"); // 88
```

### Why it matters
```csharp
// Without yield — loads ALL in memory upfront ❌
List<int> GetMillionNumbers() {
    var list = new List<int>();
    for (int i = 0; i < 1_000_000; i++) list.Add(i);
    return list;
}

// With yield — generates on demand, stops early if needed ✅
IEnumerable<int> GetMillionNumbers() {
    for (int i = 0; i < 1_000_000; i++)
        yield return i;
}
// .Take(5) only generates 5 items — never touches the rest
```

### Problem Statement
> Write `GetFibonacci()` using `yield return` (infinite sequence).
> Take first 10, print even ones, print their sum.

### Interview Tips
- `yield return` turns a method into a **state machine** under the hood
- Execution is deferred — nothing runs until you iterate
- `yield break` exits the sequence early
- Common question: *"What's the difference between returning `List<T>` and `IEnumerable<T>` with yield?"* — `List<T>` builds everything upfront. `yield` generates lazily — faster startup, lower memory for large/infinite sequences.

---

## 9. IDisposable + using

### Concept
When a class holds **unmanaged resources** (file handles, DB connections, streams), implement `IDisposable` so resources are released properly. The `using` block guarantees `Dispose()` is called even if an exception occurs.

### Code Example
```csharp
class FileLogger : IDisposable
{
    private bool _disposed = false;

    public FileLogger(string filename)
    {
        Console.WriteLine($"FileLogger opened: {filename}");
    }

    public void Log(string message)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(FileLogger));
        Console.WriteLine($"[LOG] {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("FileLogger closed.");
        _disposed = true;
    }
}

// Usage
FileLogger logger = new FileLogger("app.log");
try
{
    using (logger)
    {
        logger.Log("Starting process");
        logger.Log("Process complete");
    } // Dispose() called automatically here

    logger.Log("This throws!"); // ObjectDisposedException
}
catch (ObjectDisposedException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// Modern C# — no braces needed
using var db = new DatabaseConnection("server=localhost");
db.Query("SELECT * FROM users");
// Dispose() called at end of scope
```

### Problem Statement
> Create a `FileLogger` class implementing `IDisposable`.
> Test with a `using` block, log 2 messages, then try logging after disposal and catch the exception.

### Interview Tips
- `using` = syntactic sugar for `try/finally` with `Dispose()` in the `finally` block
- `_disposed` flag prevents double-dispose bugs
- `using var` (C# 8+) — no braces, disposes at end of enclosing scope
- Common question: *"What's the difference between `Dispose` and `Finalize`?"* — `Dispose` is called explicitly/via `using`. `Finalize` (destructor) is called by GC — non-deterministic, slower. Always prefer `Dispose`.

---

## 10. ref / out / in

### Concept
Control how arguments are **passed to methods**.

| Keyword | Must initialize before | Method can write | Use case |
|---|---|---|---|
| `ref` | ✅ Yes | ✅ Yes | Modify existing value |
| `out` | ❌ No | ✅ Must assign | Return multiple values |
| `in` | ✅ Yes | ❌ Read only | Avoid copying large structs |

### Code Example
```csharp
// ref — modify in place
public static void Square(ref int x) => x = x * x;

int x = 5;
Square(ref x);
Console.WriteLine(x); // 25

// out — return multiple values
public static void Divide(int a, int b, out int quotient, out int remainder)
{
    quotient  = a / b;
    remainder = a % b;
}

Divide(10, 3, out int quotient, out int remainder);
Console.WriteLine($"Quotient: {quotient}, Remainder: {remainder}"); // 3, 1

// TryParse — real-world out usage
foreach (var input in new[] { "123", "abc" })
{
    if (int.TryParse(input, out int result))
        Console.WriteLine($"Parsed: {result}");
    else
        Console.WriteLine($"Invalid: {input}");
}
// Parsed: 123
// Invalid: abc
```

### Problem Statement
> 1. Write `Square(ref int x)` that squares in place. Test with `x = 4`.
> 2. Write `Divide(int a, int b, out int quotient, out int remainder)`. Test with `10 / 3`.
> 3. Use `int.TryParse` on `"123"` and `"abc"`.

### Interview Tips
- `ref` and `out` both pass by reference — difference is initialization requirement
- Declare `out` variables inline: `Divide(10, 3, out int q, out int r)` — cleaner
- `in` is mainly a performance optimization for large structs — rarely used day-to-day
- Common question: *"When would you use `out` over returning a Tuple?"* — `out` for patterns like `TryParse` where return value indicates success/failure. Tuple for general multi-value returns.

---

---

## 11. Tuples

### Concept
Return **multiple values** from a method without creating a class or using `out`. Named tuples make the intent clear and the code readable.

### Code Example
```csharp
// Named tuple return type
public static (int Sum, double Average, int Max, int Min) Analyze(List<int> numbers)
    => (numbers.Sum(), numbers.Average(), numbers.Max(), numbers.Min());

// Usage — deconstruct directly
var numbers = new List<int> { 12, 524, 5235, 12, 5 };
var (sum, average, max, min) = Analyze(numbers);
Console.WriteLine($"Sum: {sum}, Average: {average}, Max: {max}, Min: {min}");

// Inline tuple
(string Name, int Age) person = ("Manthan", 25);
Console.WriteLine(person.Name); // Manthan
Console.WriteLine(person.Age);  // 25
```

### Problem Statement
> Write `Analyze(List<int> numbers)` returning a named tuple with `Sum`, `Average`, `Max`, `Min`.
> Deconstruct and print all 4 values.

### Interview Tips
- Named tuples `(int Min, int Max)` are cleaner than unnamed `(int, int)`
- Use `_` to discard values you don't need: `var (min, _, _) = GetStats(...)`
- Tuples vs records — Tuples for quick internal returns. Records for public APIs/DTOs.
- Common question: *"Tuple vs out params?"* — Tuples are cleaner for multiple returns. `out` is idiomatic for `TryParse`-style patterns where return value = success/failure.

---

## 12. Local Functions

### Concept
A function **defined inside another method** — only visible within that method. Keeps helper logic close to where it's used without polluting the class with private methods.

### Code Example
```csharp
static void ProcessStudents(List<Student> students)
{
    // Local functions — only accessible inside ProcessStudents
    string GetGrade(int score) => score switch
    {
        >= 90 => "A",
        >= 75 => "B",
        >= 60 => "C",
        _     => "F"
    };

    bool IsPassing(int score) => score >= 60;

    foreach (var student in students)
    {
        string grade    = GetGrade(student.Score);
        bool   isPassing = IsPassing(student.Score);
        Console.WriteLine($"{student.Name}: Grade={grade}, {(isPassing ? "Pass" : "Fail")}");
    }
}
```

### Can access outer variables directly
```csharp
static void Main()
{
    int tax = 18;

    // Captures outer variable — no need to pass as param
    decimal WithTax(decimal price) => price + (price * tax / 100);

    Console.WriteLine(WithTax(100)); // 118
    Console.WriteLine(WithTax(200)); // 236
}
```

### Local Function vs Lambda
```csharp
// Lambda — stored in variable, no recursion
Func<int, int> square = x => x * x;

// Local function — named, supports recursion ✅
int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
Console.WriteLine(Factorial(5)); // 120
```

### Problem Statement
> Write `ProcessStudents(List<Student> students)` with local functions `GetGrade(int score)` and `IsPassing(int score)`.
> Print name, grade, and Pass/Fail for each student.

### Interview Tips
- Local functions support **recursion** — lambdas don't
- Better **stack traces** than lambdas — named in error messages
- Can't be called outside their containing method — strict encapsulation
- Common question: *"Local function vs private method?"* — Local function scoped to one method. Private method scoped to the whole class.

---

## 13. Reflection

### Concept
Inspect and interact with **types at runtime** — get class names, properties, methods, and invoke them dynamically without knowing the type at compile time. Used heavily in ORMs, serializers, DI containers, and test frameworks.

### Code Example
```csharp
public class Employee
{
    public string Name       { get; set; }
    public string Department { get; set; }
    public int    Salary     { get; set; }
}

// 1. Get type info
Type type = typeof(Employee);
Console.WriteLine(type.Name);    // Employee
Console.WriteLine(type.FullName); // VariableScope.Employee

// 2. Create instance dynamically — no new Employee() needed
object emp = Activator.CreateInstance(type);

// 3. Set property values dynamically
type.GetProperty("Name").SetValue(emp, "Manthan");
type.GetProperty("Department").SetValue(emp, "Engineering");
type.GetProperty("Salary").SetValue(emp, 90000);

// 4. Read all properties in a loop
foreach (var prop in type.GetProperties())
    Console.WriteLine($"{prop.Name}: {prop.GetValue(emp)}");
// Name: Manthan
// Department: Engineering
// Salary: 90000
```

### Real-world use — generic object printer
```csharp
static void PrintAllProperties(object obj)
{
    Type type = obj.GetType();
    Console.WriteLine($"--- {type.Name} ---");
    foreach (var prop in type.GetProperties())
        Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}");
}

PrintAllProperties(new Employee { Name = "Manthan", Department = "Engineering", Salary = 90000 });
```

### Invoking methods dynamically
```csharp
// Public method
MethodInfo display = type.GetMethod("Display");
display.Invoke(instance, null);

// Private method — needs BindingFlags
MethodInfo secret = type.GetMethod("Secret",
    BindingFlags.NonPublic | BindingFlags.Instance);
secret.Invoke(instance, null);
```

### Problem Statement
> Using only Reflection:
> 1. Create an `Employee` instance via `Activator.CreateInstance`
> 2. Set `Name`, `Department`, `Salary` via `SetValue`
> 3. Print all properties and values in a loop

### Interview Tips
- `typeof(T)` — get type at compile time. `obj.GetType()` — get type at runtime
- `Activator.CreateInstance()` — create object without knowing type at compile time
- `BindingFlags.NonPublic` — access private members
- Common question: *"What's the downside of Reflection?"* — **Performance** — much slower than direct calls. Cache `Type` and `PropertyInfo` if reused in hot paths.

---

## Quick Reference Cheat Sheet

```
Generics          → type-safe reusable classes/methods — Stack<T>, Box<T>
LINQ              → query collections — Where, Select, GroupBy, OrderBy
async/await       → non-blocking I/O — await Task.Delay, Task.WhenAll
Nullable          → int? age — use ??, ??=, ?. for null safety
Records           → immutable data types — value equality, with expression
Pattern Matching  → switch expressions, property patterns { Prop: value }
Extension Methods → add methods to existing types — this List<int> list
yield return      → lazy sequences — generates one item at a time
IDisposable       → resource cleanup — using block calls Dispose()
ref / out / in    → pass by reference — ref modifies, out returns multiple
Tuples            → multiple return values — (int Min, int Max) GetStats()
Local Functions   → helper methods scoped to one method, support recursion
Reflection        → inspect/invoke types at runtime — typeof, GetProperties
```

---

*Generated for interview preparation — Advanced C#*
