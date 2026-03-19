# Singleton

Ensure a class has only one instance and provide a global access point to it.

## When to use it

- Shared resources that must not be duplicated — loggers, config managers, connection pools, asset managers.
- Controlling access to a single file, hardware device, or cache.

## How it works

1. Make the constructor `private` so no one can call `new`.
2. Store the single instance in a `private static` field.
3. Expose a `static GetInstance()` method that creates the instance on first call and returns it on every subsequent call (lazy initialisation).

```csharp
class Logger {
    private static Logger _instance;
    private Logger() {}                         // prevent external instantiation

    public static Logger GetInstance() {
        if (_instance == null)
            _instance = new Logger();
        return _instance;
    }

    public void Log(string message) { ... }
}

// Both calls return the same object
Logger.GetInstance().Log("App started");
Logger.GetInstance().Log("Request received");
```

## Examples

| File | What it shows |
|---|---|
| `logger/singleton.cs` | Application logger — one file, one writer |
| `asset-manager/singleton.cs` | Game asset manager — loaded once, shared everywhere |

## Caution

The basic implementation above is not thread-safe. In multi-threaded environments, use `lock` or the .NET `Lazy<T>` wrapper to prevent two threads from creating separate instances simultaneously.
