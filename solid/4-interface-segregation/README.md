# I — Interface Segregation Principle

> "Clients should not be forced to implement interfaces they don't use. Many small, focused interfaces are preferred over one fat interface."

## What it means

Don't bundle unrelated methods into a single interface. If a class is forced to implement methods it doesn't need, it signals the interface is too broad. Split it into role-specific interfaces instead.

## Problem (`it-firm/bad.cs`, `smart-home/bad.cs`)

`ILead` groups three distinct responsibilities together. `Manager` can't code, so it throws on `WorkOnTask()`. `Programmer` can't manage, so it throws on `AssignTask()` and `CreateSubTask()`. Both classes implement a contract they can only partially honour.

```csharp
interface ILead {
    void CreateSubTask();  // management concern
    void AssignTask();     // management concern
    void WorkOnTask();     // coding concern
}

class Manager    : ILead { public void WorkOnTask() => throw new Exception(...); }
class Programmer : ILead { public void AssignTask() => throw new Exception(...); }
```

## Solution (`it-firm/good.cs`, `smart-home/good.cs`)

Split into two focused interfaces. Each class implements only what it actually does.

```csharp
interface IProgrammer { void WorkOnTask(); }
interface IManager    { void CreateSubTask(); void AssignTask(); }

class Programmer : IProgrammer { ... }      // coding only
class Manager    : IManager    { ... }      // management only
class TeamLead   : IProgrammer, IManager { ... }  // both
```
