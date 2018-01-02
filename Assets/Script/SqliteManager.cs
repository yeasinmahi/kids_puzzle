using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SqliteManager: SqliteConnectionManager
{
    public static List<World> GetWorld()
    {
        List<World> worlds = new List<World>();
        Conn.Open(); //Open connection to the database.
        string sqlQuery = "SELECT * from world";
        cmd.CommandText = sqlQuery;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            World world = new World();
            world.Sl = int.Parse(reader["Sl"].ToString());
            world.WorldName = reader["WorldName"].ToString();
            world.WorldImage = reader["WorldImage"].ToString();
            world.TargetedToy = int.Parse(reader["TargetedToy"].ToString());
            world.IsActive = int.Parse(reader["IsActive"].ToString());
            worlds.Add(world);
        }
        reader.Close();
        reader = null;
        conn.Close();
        return worlds;
    }
}
    
