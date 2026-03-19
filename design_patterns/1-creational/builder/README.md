# Builder

Construct a complex object step by step. Each step is optional and the builder validates required fields before producing the final object.

## When to use it

- An object has many fields, most of them optional — a constructor with 8 parameters is hard to read and error-prone.
- The construction process must be separated from the representation (e.g., build an `HTTPRequest` independently of how it will be sent).
- You want to enforce validation rules before the object is handed out.

## How it works

1. Create a `Builder` class that mirrors the target object's fields.
2. Each setter method returns `this`, enabling a fluent (method-chaining) API.
3. A final `Build()` method validates required fields and returns the constructed object.

```csharp
HTTPRequest request = new HttpRequestBuilder()
    .addUrl("https://api.example.com/orders")
    .addMethod("POST")
    .addBody("{\"item\":\"laptop\"}")
    .addHeaders(new Dictionary<string, string> { ["Authorization"] = "Bearer token" })
    .build();  // throws if URL or Method is missing
```

## Example

| File | What it shows |
|---|---|
| `http.cs` | `HttpRequestBuilder` builds an `HTTPRequest` with URL, method, body, and headers — URL and method are required, the rest are optional |
