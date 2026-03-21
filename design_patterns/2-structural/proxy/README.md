# Proxy

Provide a surrogate that controls access to another object. The proxy implements the same interface as the real object and intercepts calls to add behaviour — access control, caching, logging, lazy init — without the caller knowing.

## When to use it

- You need to control access to an object (auth, permissions).
- You want to cache results of expensive operations transparently.
- You need to add cross-cutting behaviour (logging, metrics) without modifying the real class.

## How it works

1. Define a common interface for both the real object and the proxy.
2. Implement the real class with the core logic.
3. Implement the proxy class: hold a reference to the real object, intercept calls, and add behaviour before/after delegating.

```csharp
IDatabase db = new DatabaseProxy("guest");
db.Query("SELECT * FROM users");
// [Proxy] Access denied. Admins only.

IDatabase adminDb = new DatabaseProxy("admin");
adminDb.Query("SELECT * FROM users");
// [Proxy] Access granted.
// [DB] Executing: SELECT * FROM users
```

## Examples

| File | What it shows |
|---|---|
| `database/database.cs` | `DatabaseProxy` wraps `Database` and enforces role-based access — only `admin` users reach the real query |
| `file-reader/file-reader.cs` | `FileReaderCacheProxy` wraps `MyFileReader` and serves cached content on a simulated cache hit, falling back to the real reader on a miss |
