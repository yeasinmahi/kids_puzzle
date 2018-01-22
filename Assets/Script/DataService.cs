using System.Linq;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService
{

    private SQLiteConnection _connection;

    public DataService(string DatabaseName)
    {
        Others.MoveAssetStreamingToPersistendDataPath(DatabaseName);
        _connection = new SQLiteConnection(Others.GetDestinationPath(DatabaseName), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    public void CreateDB()
    {
        _connection.DropTable<World>();
        _connection.CreateTable<World>();
        _connection.InsertAll(new[]{
            new World{
                Sl = 1,
                Name = "Animal World",
                Image = "W1",
                IsReady = 1,
                TargetedToy = 0,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new World{
                Sl = 2,
                Name = "Flower World",
                Image = "W2",
                IsReady = 1,
                TargetedToy = 10,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new World{
                Sl = 3,
                Name = "Fish World",
                Image = "W3",
                IsReady = 1,
                TargetedToy = 20,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new World{
                Sl = 4,
                Name = "Sky World",
                Image = "W4",
                IsReady = 0,
                TargetedToy = 30,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new World{
                Sl = 5,
                Name = "Baby World",
                Image = "W5",
                IsReady = 0,
                TargetedToy = 40,
                UpdateDate = System.DateTime.Now.ToString()
            },

        });
        _connection.DropTable<InsideWorld>();
        _connection.CreateTable<InsideWorld>();
        _connection.InsertAll(new[]{
            new InsideWorld{
                Sl = 1,
                ColorImage = "IWC1",
                BWImage = "IWB1",
                IsReady = 1,
                IsComplete = 0,
                AchievedToy = 0,
                FinishingTime = 30,
                WorldId = 1,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new InsideWorld{
                Sl = 2,
                ColorImage = "IWC2",
                BWImage = "IWB2",
                IsReady = 1,
                IsComplete = 0,
                AchievedToy = 0,
                FinishingTime = 30,
                WorldId = 1,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new InsideWorld{
                Sl = 3,
                ColorImage = "IWC3",
                BWImage = "IWB3",
                IsReady = 1,
                IsComplete = 0,
                AchievedToy = 0,
                FinishingTime = 30,
                WorldId = 1,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new InsideWorld{
                Sl = 4,
                ColorImage = "IWC4",
                BWImage = "IWB4",
                IsReady = 1,
                IsComplete = 0,
                AchievedToy = 0,
                FinishingTime = 30,
                WorldId = 1,
                UpdateDate = System.DateTime.Now.ToString()
            },
            new InsideWorld{
                Sl = 5,
                ColorImage = "IWC5",
                BWImage = "IWB5",
                IsReady = 1,
                IsComplete = 0,
                AchievedToy = 0,
                FinishingTime = 30,
                WorldId = 1,
                UpdateDate = System.DateTime.Now.ToString()
            },

        });
    }
    #region Get
    public List<World> GetWorlds()
    {
        return _connection.Table<World>().ToList();
    }
    public List<InsideWorld> GetInsideWorlds()
    {
        return _connection.Table<InsideWorld>().ToList();
    }
    public List<InsideWorld> GetInsideWorlds(int worldId)
    {
        return _connection.Table<InsideWorld>().Where(x => x.WorldId.Equals(worldId)).ToList();
    }
    public World GetWorld(int worldId)
    {
        return _connection.Find<World>(worldId);
    }
    public InsideWorld GetInsideWorld(int insideWorldId)
    {
        return _connection.Find<InsideWorld>(insideWorldId);
    }
    public int GetAchivedToy(int insideWorldId)
    {
        return _connection.Get<InsideWorld>(e => e.Sl.Equals(insideWorldId)).AchievedToy;
        //return _connection.Table<InsideWorld>().Where(x => x.WorldId.Equals(worldId)).Sum(x => x.AchievedToy);
    }
    public int GetTotalAchivedToy()
    {
        return _connection.Table<InsideWorld>().Sum(x => x.AchievedToy);
    }
    #endregion
    #region Insert
    public bool InsertWorld(World world)
    {
        return _connection.Insert(world) > 0;
    }
    public bool InsertInsideWorld(InsideWorld insideWorld)
    {
        return _connection.Insert(insideWorld) > 0;
    }
    #endregion
    #region Delete
    public bool DeleteWorld(int worldId)
    {
        return _connection.Delete<World>(worldId) > 0;
    }
    public bool DeleteInsideWorld(int insideWorldId)
    {
        return _connection.Delete<World>(insideWorldId) > 0;
    }
    #endregion
    #region Update
    public bool UpdateWorld(World world)
    {
        return _connection.Update(world) > 0;
    }
    public bool UpdateInsideWorld(InsideWorld insideWorld)
    {
        return _connection.Update(insideWorld) > 0;
    }
    #endregion
}
