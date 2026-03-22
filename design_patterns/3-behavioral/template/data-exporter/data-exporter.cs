// Abstract class defines the skeleton
public abstract class DataExporter
{

    // Template method — fixed structure, not overridable
    public void Export()
    {
        FetchData();
        ProcessData();
        SaveData();
    }

    protected abstract void FetchData();
    protected abstract void ProcessData();

    // Hook — optional override, has default behaviour
    protected virtual void SaveData()
    {
        Console.WriteLine("[Default] Saving to disk...");
    }
}

// Subclasses fill in the steps
public class CsvExporter : DataExporter
{
    protected override void FetchData() =>
        Console.WriteLine("[CSV] Fetching from database...");
    protected override void ProcessData() =>
        Console.WriteLine("[CSV] Converting to CSV format...");
}

public class PdfExporter : DataExporter
{
    protected override void FetchData() =>
        Console.WriteLine("[PDF] Fetching from API...");
    protected override void ProcessData() =>
        Console.WriteLine("[PDF] Rendering PDF layout...");
    protected override void SaveData() =>       // overrides hook
        Console.WriteLine("[PDF] Uploading to cloud...");
}

public class Program
{
    public static void Main()
    {
        // Usage
        DataExporter exporter = new CsvExporter();
        exporter.Export();
        // [CSV] Fetching from database...
        // [CSV] Converting to CSV format...
        // [Default] Saving to disk...

        exporter = new PdfExporter();
        exporter.Export();
        // [PDF] Fetching from API...
        // [PDF] Rendering PDF layout...
        // [PDF] Uploading to cloud...

    }
}

