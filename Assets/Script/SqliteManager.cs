using System.Collections.Generic;

public class SqliteManager: SqliteConnectionManager
{
    static DataService dataService = null;
    static SqliteManager()
    {
        dataService = new DataService("KidsPuzzle.dll");
        //dataService.CreateDB();
    }
    public static List<World> GetWorlds()
    {
        return dataService.GetWorlds();
    }
    public static List<InsideWorld> GetInsideWorlds()
    {
        return dataService.GetInsideWorlds();
    }
    public static List<InsideWorld> GetInsideWorlds(int worldId)
    {
        return dataService.GetInsideWorlds(worldId);
    }
    public static int GetTotalAchivedToy()
    {
        return dataService.GetTotalAchivedToy();
        //Conn.Open(); //Open connection to the database.
        //Query = "select sum(achievedToy) as total from insideWorld where worldId = " + worldId;
        //cmd.CommandText = Query;
        //reader = cmd.ExecuteReader();
        //int achievedToy = 0;
        //while (reader.Read())
        //{
        //    int.TryParse(reader["total"].ToString(),out achievedToy);

        //}
        //reader.Close();
        //reader = null;
        //conn.Close();
        //return achievedToy;
    }
    public static InsideWorld GetInsideWorld(int insideWorldId)
    {
        return dataService.GetInsideWorld(insideWorldId);
        //Conn.Open(); //Open connection to the database.
        //Query = "SELECT * from insideWorld where SL = " + insideWorldId;
        //InsideWorld insideWorld = new InsideWorld();
        //cmd.CommandText = Query;
        //reader = cmd.ExecuteReader();
        //while (reader.Read())
        //{
            
        //    insideWorld.Sl = int.Parse(reader["Sl"].ToString());
        //    insideWorld.ColorImage = reader["ColorImage"].ToString();
        //    insideWorld.BWImage = reader["BWImage"].ToString();
        //    insideWorld.FinishingTime = float.Parse(reader["FinishingTime"].ToString());
        //    insideWorld.WorldId = int.Parse(reader["WorldId"].ToString());
        //    insideWorld.IsReady = int.Parse(reader["IsReady"].ToString());
        //    insideWorld.IsComplete = int.Parse(reader["IsComplete"].ToString());
        //    insideWorld.UpdateDate = reader["UpdateDate"].ToString();
        //    insideWorld.AchievedToy = int.Parse(reader["AchievedToy"].ToString());
        //    break;
        //}
        //reader.Close();
        //reader = null;
        //conn.Close();
        //return insideWorld;
    }
    public static int GetAchievedToy(int insideWorldId)
    {
        return dataService.GetAchivedToy(insideWorldId);
        //int achivedToy = 0;
        //Conn.Open(); //Open connection to the database.
        //Query = "SELECT AchievedToy from insideWorld where SL = " + insideWorldId;
        //cmd.CommandText = Query;
        //reader = cmd.ExecuteReader();
        //while (reader.Read())
        //{
        //    achivedToy = int.Parse(reader["AchievedToy"].ToString());
        //    break;
        //}
        //reader.Close();
        //reader = null;
        //conn.Close();
        //return achivedToy;
    }
    public static bool InsertDataIntoWorld(World world)
    {
        return dataService.InsertWorld(world);
        //int row = 0;
        //Conn.Open();
        //if(world != null)
        //{
        //    Query = "INSERT INTO world(Sl,Name,Image,TargetedToy,IsReady,UpdateDate) VALUES(" + world.Sl + ",'" + world.Name + "','" + world.Image + "'," + world.TargetedToy + "," + world.IsReady + ",'" + world.UpdateDate + "')";
        //    cmd.CommandText = Query;
        //    row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
        //return row > 0;
    }
    public static bool UpdateIsCompleteAndToyIntoInsideWorld(int insideWorldId, int isComplete, int achievedToy)
    {
        InsideWorld insideWorld = dataService.GetInsideWorld(insideWorldId);
        if (insideWorld != null)
        {
            insideWorld.IsComplete = isComplete;
            insideWorld.AchievedToy = achievedToy;
        }
        return dataService.UpdateInsideWorld(insideWorld);
        //int row = 0;
        //Conn.Open();
        //if (insideWorldId > 0)
        //{
        //    Query = "UPDATE insideWorld SET isComplete = '" + isComplete + "', achievedToy = '" + achievedToy + "' where Sl = " + insideWorldId;
        //    cmd.CommandText = Query;
        //    row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
        //return row > 0;
    }
    public static bool UpdateDataIntoWorld(World world)
    {
        return dataService.UpdateWorld(world);
        //Conn.Open();
        //if (world != null)
        //{
        //    Query = "UPDATE world SET Name = '" + world.Name + "', Image = '"+ world.Image + "', TargetedToy = " + world.TargetedToy + ", IsReady = " + world.IsReady + ", UpdateDate = '" + world.UpdateDate + "' where Sl = " + world.Sl;
        //    cmd.CommandText = Query;
        //    int row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
    }
    public static bool UpdateIsReadyIntoWorld(int sl)
    {
        World world = dataService.GetWorld(sl);
        if (world != null)
        {
            world.IsReady = 1;
        }
        return dataService.UpdateWorld(world);
        //int row = 0;
        //Conn.Open();
        //if (Sl > 0)
        //{
        //    Query = "UPDATE world SET IsReady = 1 where Sl = " + Sl;
        //    cmd.CommandText = Query;
        //    row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
        //return row > 0;
    }
    public static bool DeleteDataFromWorld(int sl)
    {
        return dataService.DeleteWorld(sl);
        //int row = 0;
        //Conn.Open();
        //if (Sl > 0)
        //{
        //    Query = "DELETE FROM world WHERE Sl = " + Sl;
        //    cmd.CommandText = Query;
        //    row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
        //return row > 0;
    }
    public static bool InsertDataIntoInsideWorld(InsideWorld insideWorld)
    {
        return dataService.InsertInsideWorld(insideWorld);
        //int row = 0;
        //Conn.Open();
        //if (insideWorld != null)
        //{
        //    Query = "INSERT INTO insideWorld(Sl,ColorImage,BWImage,FinishingTime,WorldId,IsReady,UpdateDate,IsComplete,AchievedToy) VALUES(" + insideWorld.Sl + ",'" + insideWorld.ColorImage + "','" + insideWorld.BWImage + "'," + insideWorld.FinishingTime + "," + insideWorld.WorldId + "," + insideWorld.IsReady + ",'" + insideWorld.UpdateDate + "'," + insideWorld.IsComplete + "," + insideWorld.AchievedToy + ")";
        //    cmd.CommandText = Query;
        //    row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
        //return row > 0;
    }
    public static bool UpdateDataIntoInsideWorld(InsideWorld insideWorld)
    {
        return dataService.UpdateInsideWorld(insideWorld);
        //Conn.Open();
        //if (insideWorld != null)
        //{
        //    Query = "UPDATE insideWorld SET ColorImage = '" + insideWorld.ColorImage + "', BWImage = '" + insideWorld.BWImage + "', FinishingTime = " + insideWorld.FinishingTime + ", WorldId = " + insideWorld.WorldId + ", IsReady = " + insideWorld.IsReady + ", UpdateDate = '" + insideWorld.UpdateDate + "', IsComplete = " + insideWorld.IsComplete + ", AchievedToy = " + insideWorld.AchievedToy + " where Sl = " + insideWorld.Sl;
        //    cmd.CommandText = Query;
        //    int row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
    }
    public static bool UpdateIsReadyIntoInsideWorld(int sl)
    {
        InsideWorld insideWorld = dataService.GetInsideWorld(sl);
        if (insideWorld != null)
        {
            insideWorld.IsReady = 1;
        }
        return dataService.UpdateInsideWorld(insideWorld);
        //Conn.Open();
        //if (Sl > 0)
        //{
        //    Query = "UPDATE insideWorld SET IsReady = 1 where Sl = " + Sl;
        //    cmd.CommandText = Query;
        //    int row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
    }
    public static bool DeleteDataFromInsideWorld(int sl)
    {
        return dataService.DeleteInsideWorld(sl);
        //Conn.Open();
        //if (Sl > 0)
        //{
        //    Query = "DELETE FROM insideWorld WHERE Sl = " + Sl;
        //    cmd.CommandText = Query;
        //    int row = cmd.ExecuteNonQuery();
        //}
        //conn.Close();
    }
    //public static List<World> GetWorlds()
    //{
    //    List<World> worlds = new List<World>();
    //    Conn.Open(); //Open connection to the database.
    //    Query = "SELECT * from world";
    //    cmd.CommandText = Query;
    //    reader = cmd.ExecuteReader();
    //    while (reader.Read())
    //    {
    //        World world = new World();
    //        world.Sl = int.Parse(reader["Sl"].ToString());
    //        world.Name = reader["Name"].ToString();
    //        world.Image = reader["Image"].ToString();
    //        world.TargetedToy = int.Parse(reader["TargetedToy"].ToString());
    //        world.IsReady = int.Parse(reader["IsReady"].ToString());
    //        world.UpdateDate = reader["UpdateDate"].ToString();
    //        worlds.Add(world);
    //    }
    //    reader.Close();
    //    reader = null;
    //    conn.Close();
    //    return worlds;
    //}

    //public static List<InsideWorld> GetInsideWorlds()
    //{
    //    List<InsideWorld> insideWorlds = new List<InsideWorld>();
    //    Conn.Open(); //Open connection to the database.
    //    Query = "SELECT * from insideWorld";
    //    cmd.CommandText = Query;
    //    reader = cmd.ExecuteReader();
    //    while (reader.Read())
    //    {
    //        InsideWorld insideWorld = new InsideWorld();
    //        insideWorld.Sl = int.Parse(reader["Sl"].ToString());
    //        insideWorld.ColorImage = reader["ColorImage"].ToString();
    //        insideWorld.BWImage = reader["BWImage"].ToString();
    //        insideWorld.FinishingTime = float.Parse(reader["FinishingTime"].ToString());
    //        insideWorld.WorldId = int.Parse(reader["WorldId"].ToString());
    //        insideWorld.IsReady = int.Parse(reader["IsReady"].ToString());
    //        insideWorld.IsComplete = int.Parse(reader["IsComplete"].ToString());
    //        insideWorld.UpdateDate = reader["UpdateDate"].ToString();
    //        insideWorld.AchievedToy = int.Parse(reader["AchievedToy"].ToString());
    //        insideWorlds.Add(insideWorld);
    //    }
    //    reader.Close();
    //    reader = null;
    //    conn.Close();
    //    return insideWorlds;
    //}

    //public static List<InsideWorld> GetInsideWorlds(int worldId)
    //{
    //    List<InsideWorld> insideWorlds = new List<InsideWorld>();
    //    Conn.Open(); //Open connection to the database.
    //    Query = "SELECT * from insideWorld where worldId = " + worldId;
    //    cmd.CommandText = Query;
    //    reader = cmd.ExecuteReader();
    //    while (reader.Read())
    //    {
    //        InsideWorld insideWorld = new InsideWorld();
    //        insideWorld.Sl = int.Parse(reader["Sl"].ToString());
    //        insideWorld.ColorImage = reader["ColorImage"].ToString();
    //        insideWorld.BWImage = reader["BWImage"].ToString();
    //        insideWorld.FinishingTime = float.Parse(reader["FinishingTime"].ToString());
    //        insideWorld.WorldId = int.Parse(reader["WorldId"].ToString());
    //        insideWorld.IsReady = int.Parse(reader["IsReady"].ToString());
    //        insideWorld.IsComplete = int.Parse(reader["IsComplete"].ToString());
    //        insideWorld.UpdateDate = reader["UpdateDate"].ToString();
    //        insideWorld.AchievedToy = int.Parse(reader["AchievedToy"].ToString());
    //        insideWorlds.Add(insideWorld);
    //    }
    //    reader.Close();
    //    reader = null;
    //    conn.Close();
    //    return insideWorlds;
    //}

}

