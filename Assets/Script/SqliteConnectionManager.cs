using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class SqliteConnectionManager {

    public static string ConnectionString
    {
        get
        {
            return _connectionString;
        }
        private set
        {
            _connectionString = "file://" + Application.streamingAssetsPath + "/KidsPuzzle.dll";
        }
    }
    public static IDbConnection Conn
    {
        get
        {
            return conn;
        }
        private set
        {
            conn = new SqliteConnection(ConnectionString);
        }
    }

    protected static string Query { get; set; }
    protected static string _connectionString;
    protected static IDbConnection conn;
    protected static IDbCommand cmd;
    protected static IDataReader reader;

    static SqliteConnectionManager()
    {
        _connectionString = "file://" + Application.streamingAssetsPath + "/KidsPuzzle.dll";
        conn = new SqliteConnection(_connectionString);
        cmd = conn.CreateCommand();
    }

}
