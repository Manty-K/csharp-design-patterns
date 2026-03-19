// 1. Abstraction — both loggers depend on this
public interface ILogger {
    void LogMessage(string message);
}

// 2. Low-level modules implement the abstraction
public class FileLogger : ILogger {
    public void LogMessage(string message) {
        // write to file
    }
}

public class DbLogger : ILogger {
    public void LogMessage(string message) {
        // write to database
    }
}

// 3. ExceptionLogger depends on ILogger — not on FileLogger or DbLogger
public class ExceptionLogger {
    private readonly ILogger _logger;

    public ExceptionLogger(ILogger logger) { // injected ✅
        _logger = logger;
    }

    public void LogException(Exception ex) {
        _logger.LogMessage(GetUserReadableMessage(ex));
    }

    private string GetUserReadableMessage(Exception ex) {
        return ex.Message; // simplified
    }
}

// 4. DataExporter depends on ExceptionLogger injected — not newed up
public class DataExporter {
    private readonly ExceptionLogger _exceptionLogger;

    public DataExporter(ExceptionLogger exceptionLogger) { // injected ✅
        _exceptionLogger = exceptionLogger;
    }

    public void ExportDataFromFile() {
        try {
            // export data
        }
        catch (Exception ex) {
            _exceptionLogger.LogException(ex);
        }
    }
}