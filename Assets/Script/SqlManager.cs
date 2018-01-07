using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SqlManager: SqlConnectionManager
{
    public static List<World> GetWorld()
    {
        List<World> worlds = new List<World>();
        Conn.Open(); //Open connection to the database.
        string sqlQuery = "SELECT * FROM tbl_World WHERE IsActive == true";
        cmd.CommandText = sqlQuery;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            World world = new World();
            world.Sl = int.Parse(reader["Sl"].ToString());
            world.Name = reader["Name"].ToString();
            world.Image = reader["Image"].ToString();
            world.TargetedToy = int.Parse(reader["TargetedToy"].ToString());
            world.UpdateDate = reader["UpdateDate"].ToString();
            worlds.Add(world);
        }
        reader.Close();
        reader = null;
        conn.Close();
        return worlds;
    }
    public static List<InsideWorld> GetInsideWorld(int worldId)
    {
        List<InsideWorld> insideWorlds = new List<InsideWorld>();
        Conn.Open(); //Open connection to the database.
        string sqlQuery = "SELECT * FROM tbl_InsideWorld WHERE IsActive == true and WorldId = " + worldId;
        cmd.CommandText = sqlQuery;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            InsideWorld insideWorld = new InsideWorld();
            insideWorld.Sl = int.Parse(reader["Sl"].ToString());
            insideWorld.ColorImage = reader["ColorImage"].ToString();
            //insideWorld.BWImage = reader["BWImage"].ToString();
            insideWorld.FinishingTime = float.Parse(reader["FinishingTime"].ToString());
            insideWorld.WorldId = int.Parse(reader["WorldId"].ToString());
            insideWorld.UpdateDate = reader["UpdateDate"].ToString();
            insideWorlds.Add(insideWorld);
        }
        reader.Close();
        reader = null;
        conn.Close();
        return insideWorlds;
    }
}
    
