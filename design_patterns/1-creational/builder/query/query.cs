public class QueryBuilder
{
    private string _table;
    private List<string> _columns = new();
    private string _where;
    private string _orderBy;
    private int? _limit;

    public QueryBuilder From(string table)
    {
        _table = table;
        return this;
    }

    public QueryBuilder Select(params string[] columns)
    {
        _columns.AddRange(columns);
        return this;
    }

    public QueryBuilder Where(string condition)
    {
        _where = condition;
        return this;
    }

    public QueryBuilder OrderBy(string column)
    {
        _orderBy = column;
        return this;
    }

    public QueryBuilder Limit(int limit)
    {
        _limit = limit;
        return this;
    }

    public Query Build()
    {
        if (string.IsNullOrWhiteSpace(_table))
            throw new InvalidOperationException("Table is required.");

        return new Query(
            table:   _table,
            columns: _columns.Count > 0 ? string.Join(", ", _columns) : "*",
            where:   _where,
            orderBy: _orderBy,
            limit:   _limit
        );
    }
}

public class Query(string table, string columns, string where, string orderBy, int? limit)
{
    public override string ToString()
    {
        var sql = $"SELECT {columns} FROM {table}";
        if (where   != null) sql += $" WHERE {where}";
        if (orderBy != null) sql += $" ORDER BY {orderBy}";
        if (limit   != null) sql += $" LIMIT {limit}";
        return sql;
    }
}

public class Program
{
    public static void Main()
    {
        var query = new QueryBuilder()
            .From("orders")
            .Select("id", "customer", "total")
            .Where("total > 100")
            .OrderBy("total DESC")
            .Limit(10)
            .Build();

        Console.WriteLine(query);
        // SELECT id, customer, total FROM orders WHERE total > 100 ORDER BY total DESC LIMIT 10

        var simple = new QueryBuilder()
            .From("products")
            .Build();

        Console.WriteLine(simple);
        // SELECT * FROM products
    }
}
