public interface IReadable {
    string LoadText();
}

public interface IWritable {
    void SaveText();
}

public class WritableSqlFile : IReadable, IWritable {
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText() {
        return File.ReadAllText(FilePath); // example
    }

    public void SaveText() {
        File.WriteAllText(FilePath, FileText); // example
    }
}

public class ReadOnlySqlFile : IReadable {
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText() {
        return File.ReadAllText(FilePath);
    }
    // No SaveText() — doesn't even exist here ✅
}

public class SqlFileManager {
    public string GetTextFromFiles(List<IReadable> readableFiles) {
        var sb = new StringBuilder();
        foreach (var file in readableFiles)
            sb.Append(file.LoadText());
        return sb.ToString();
    }

    public void SaveTextIntoFiles(List<IWritable> writableFiles) {
        foreach (var file in writableFiles)
            file.SaveText();
    }
}