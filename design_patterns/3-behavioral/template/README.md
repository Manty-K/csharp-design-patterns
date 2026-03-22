# Template Method

Define the skeleton of an algorithm in a base class, deferring some steps to subclasses. Subclasses can override specific steps without changing the overall structure.

## When to use it

- Multiple classes share the same algorithm structure but differ in specific steps.
- You want to avoid code duplication by pulling the invariant parts into a base class.
- You need optional hook points — steps that subclasses may override but don't have to.

## How it works

1. Define an **abstract base class** with a **template method** that calls a fixed sequence of steps.
2. Mark varying steps as `abstract` (must override) or `virtual` (hook — optional override with a default).
3. **Concrete subclasses** implement only the steps that differ.

```csharp
DataExporter exporter = new CsvExporter();
exporter.Export();
// [CSV] Fetching from database...
// [CSV] Converting to CSV format...
// [Default] Saving to disk...      ← hook not overridden, uses default

exporter = new PdfExporter();
exporter.Export();
// [PDF] Fetching from API...
// [PDF] Rendering PDF layout...
// [PDF] Uploading to cloud...      ← hook overridden
```

## Examples

| File | What it shows |
|---|---|
| `data-exporter/data-exporter.cs` | `DataExporter.Export()` is the template method; `FetchData`/`ProcessData` are abstract; `SaveData` is a virtual hook — `CsvExporter` uses the default, `PdfExporter` overrides it |
| `report-generator/report-generator.cs` | `Reporter.Generate()` sequences four steps; `SalesReporter` uses the default `SendReport` (email), `InventoryReport` overrides it to send via Slack |
