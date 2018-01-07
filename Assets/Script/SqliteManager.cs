using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

public class SqliteManager: SqliteConnectionManager
{
    public static List<World> GetWorlds()
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
            world.Name = reader["Name"].ToString();
            world.Image = reader["Image"].ToString();
            world.TargetedToy = int.Parse(reader["TargetedToy"].ToString());
            world.IsReady = int.Parse(reader["IsReady"].ToString());
            world.UpdateDate = reader["UpdateDate"].ToString();
            worlds.Add(world);
        }
        reader.Close();
        reader = null;
        conn.Close();
        return worlds;
    }
    public static List<InsideWorld> GetInsideWorlds(int worldId)
    {
        List<InsideWorld> insideWorlds = new List<InsideWorld>();
        Conn.Open(); //Open connection to the database.
        string sqlQuery = "SELECT * from insideWorld where worldId = " + worldId;
        cmd.CommandText = sqlQuery;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            InsideWorld insideWorld = new InsideWorld();
            insideWorld.Sl = int.Parse(reader["Sl"].ToString());
            insideWorld.ColorImage = reader["ColorImage"].ToString();
            insideWorld.BWImage = reader["BWImage"].ToString();
            insideWorld.FinishingTime = float.Parse(reader["FinishingTime"].ToString());
            insideWorld.WorldId = int.Parse(reader["WorldId"].ToString());
            insideWorld.IsReady = int.Parse(reader["IsReady"].ToString());
            insideWorld.IsComplete = int.Parse(reader["IsComplete"].ToString());
            insideWorld.UpdateDate = reader["UpdateDate"].ToString();
            insideWorld.AchievedToy = int.Parse(reader["AchievedToy"].ToString());
            insideWorlds.Add(insideWorld);
        }
        reader.Close();
        reader = null;
        conn.Close();
        return insideWorlds;
    }
    public static InsideWorld GetInsideWorld(int insideWorldId)
    {
        Conn.Open(); //Open connection to the database.
        string sqlQuery = "SELECT * from insideWorld where SL = " + insideWorldId;
        InsideWorld insideWorld = new InsideWorld();
        cmd.CommandText = sqlQuery;
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            
            insideWorld.Sl = int.Parse(reader["Sl"].ToString());
            insideWorld.ColorImage = reader["ColorImage"].ToString();
            insideWorld.BWImage = reader["BWImage"].ToString();
            insideWorld.FinishingTime = float.Parse(reader["FinishingTime"].ToString());
            insideWorld.WorldId = int.Parse(reader["WorldId"].ToString());
            insideWorld.IsReady = int.Parse(reader["IsReady"].ToString());
            insideWorld.IsComplete = int.Parse(reader["IsComplete"].ToString());
            insideWorld.UpdateDate = reader["UpdateDate"].ToString();
            insideWorld.AchievedToy = int.Parse(reader["AchievedToy"].ToString());
            break;
        }
        reader.Close();
        reader = null;
        conn.Close();
        return insideWorld;
    }
    public static void InsertDataIntoWorld(List<World> worlds)
    {
        Conn.Open();
        foreach (var item in worlds)
        {
            string sqlQuery = "INSERT INTO world(Name,Image,TargetedToy,IsReady,UpdateDate) VALUES('"+item.Name + "," + item.Image + "," + item.TargetedToy + "," + item.IsReady + "," + item.UpdateDate + "," + "')";
            cmd.CommandText = sqlQuery;
            int row = cmd.ExecuteNonQuery();
        }
        reader.Close();
        reader = null;
        conn.Close();
    }
    public static void InsertDataIntoInsideWorld(List<InsideWorld> insideWorlds)
    {
        Conn.Open();
        foreach (var item in insideWorlds)
        {
            string sqlQuery = "INSERT INTO insideWorld(ColorImage,BWImage,FinishingTime,IsReady,UpdateDate,IsComplete,AchievedToy) VALUES('" + item.ColorImage + "," + item.BWImage + "," + item.FinishingTime + "," + item.IsReady + "," + item.UpdateDate + "," + item.IsComplete + "," + item.AchievedToy + "')";
            cmd.CommandText = sqlQuery;
            int row = cmd.ExecuteNonQuery();
        }
        reader.Close();
        reader = null;
        conn.Close();
    }
}
    
