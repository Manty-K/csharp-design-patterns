# Adapter

Make incompatible interfaces work together. Wrap an existing class with a new interface so callers never see the mismatch.

## When to use it

- You need to use an existing class but its interface does not match what your code expects.
- You cannot (or should not) modify the existing class — legacy code, third-party library, etc.
- You want to keep your application code clean by depending on a stable interface, not on a messy external API.

## How it works

1. Define the interface your application expects.
2. Wrap the incompatible class in an adapter that implements that interface.
3. Translate calls inside the adapter — the caller never touches the wrapped class directly.

```csharp
// Incompatible existing class
class LegacyPrinter {
    public void PrintRaw(byte[] data) { ... }
}

// Interface your app expects
interface IDocument {
    void Print(string text);
}

// Adapter bridges the gap
class PrinterAdapter : IDocument {
    private LegacyPrinter _printer;
    public void Print(string text) =>
        _printer.PrintRaw(Encoding.UTF8.GetBytes(text));
}
```

## Examples

| File | What it shows |
|---|---|
| `sms/sms.cs` | `SmsAdapter` wraps `LegacySmsService` (takes `phone` + `content`) behind `INotification.Send(message)` |
| `weather/weather.cs` | `WeatherAdapter` wraps `WeatherAPI` (takes `lat`, `lon`) behind `IWeatherService.GetWeather(cityCoordinates)` — parses a `"lat,lon"` string and delegates to the raw API |
