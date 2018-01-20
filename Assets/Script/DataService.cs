using UnityEngine;
using System.Linq;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

	public void CreateDB(){
		_connection.DropTable<World> ();
		_connection.CreateTable<World> ();

       
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

    public List<World> GetWorlds(){
		return _connection.Table<World>().ToList();
	}
    public List<InsideWorld> GetInsideWorlds()
    {
        return _connection.Table<InsideWorld>().ToList();
    }
    public List<InsideWorld> GetInsideWorlds(int worldId)
    {
        return _connection.Table<InsideWorld>().Where(x=>x.WorldId.Equals(worldId)).ToList();
    }
    public int GetTotalAchivedToy(int worldId)
    {
        return _connection.Table<InsideWorld>().Where(x => x.WorldId.Equals(worldId)).Sum(x => x.AchievedToy);
    }
}
