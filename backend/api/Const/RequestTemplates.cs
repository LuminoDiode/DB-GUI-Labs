//namespace api.Const;

//public static class RequestTemplates
//{
//    public static string CreateDatabase_1p = """
//		CREATE DATABASE ?p1?;
//	""";

//    public static string DropDatabase_1p = """
//		DROP DATABASE :p1 WITH (FORCE);
//	""";

//    public const string SearchDatabase_1p = """
//		SELECT datname FROM pg_database WHERE datname = @p1;
//	""";

//    public const string ListTables_0p = """
//		SELECT table_name
//		FROM information_schema.tables
//		WHERE table_schema = 'public'
//		ORDER BY table_name;
//	""";

//    public const string SearchTable_1p = """
//		SELECT table_name
//		FROM information_schema.tables
//		WHERE table_schema = 'public' 
//		AND table_name = @p1
//		ORDER BY table_name;
//	""";

//    public const string _createTable = """
//		CREATE TABLE {0}({1});
//	""";
//    /// <summary>
//    /// never expose this directly to the public API
//    /// </summary>
//    public static string CreateTable(string tableName, IEnumerable<KeyValuePair<string, string>> columnNameToType)
//        => string.Format(_createTable, tableName,
//            string.Join(",", columnNameToType.Select(x => $"{x.Key} {x.Value}")));

//    private const string _dropTable = """
//		DROP TABLE {0};
//	""";
//    /// <summary>
//    /// never expose this directly to the public API
//    /// </summary>
//    public static string DropTable(string tableName)
//        => string.Format(_dropTable, tableName);

//}
