// Real service
public interface IDatabase {
    string Query(string sql);
}

public class Database : IDatabase {
    public string Query(string sql) {
        Console.WriteLine($"[DB] Executing: {sql}");
        return "result";
    }
}

// Proxy — adds auth check before allowing access
public class DatabaseProxy : IDatabase {
    private Database _db = new Database();
    private string _userRole;

    public DatabaseProxy(string userRole) {
        _userRole = userRole;
    }

    public string Query(string sql) {
        if (_userRole != "admin") {
            Console.WriteLine("[Proxy] Access denied. Admins only.");
            return null;
        }
        Console.WriteLine("[Proxy] Access granted.");
        return _db.Query(sql);
    }
}

// Usage
IDatabase db = new DatabaseProxy("guest");
db.Query("SELECT * FROM users");
// [Proxy] Access denied. Admins only.

IDatabase adminDb = new DatabaseProxy("admin");
adminDb.Query("SELECT * FROM users");
// [Proxy] Access granted.
// [DB] Executing: SELECT * FROM users