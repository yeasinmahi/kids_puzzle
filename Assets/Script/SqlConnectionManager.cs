using System.Data.SqlClient;

public class SqlConnectionManager {

    public static string ConnectionString
    {
        get
        {
            return _connectionString;
        }
        private set
        {
            _connectionString = @"Data Source = 27.147.182.102;
							user id = su;
							password = sa2009;
							Initial Catalog = KidsPuzzle;";
        }
    }
    public static SqlConnection Conn
    {
        get
        {
            return conn;
        }
        private set
        {
            conn = new SqlConnection(ConnectionString);
        }
    }

    protected static string Query { get; set; }
    protected static string _connectionString;
    protected static SqlConnection conn;
    protected static SqlCommand cmd;
    protected static SqlDataReader reader;

    static SqlConnectionManager()
    {
        _connectionString = @"Data Source = 27.147.182.102;
							user id = su;
							password = sa2009;
							Initial Catalog = KidsPuzzle;";
        conn = new SqlConnection(_connectionString);
        cmd = conn.CreateCommand();
    }
    
}
