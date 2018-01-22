using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
    void Start()
    {
        StartCoroutine(checkInternetConnection((isConnected) => {
            if(isConnected)
            {
                Debug.Log("Connected");
                List<World> SqlWorldList = SqlManager.GetWorld();
                List<InsideWorld> SqlInsideWorldList = SqlManager.GetInsideWorld();
                List<World> SqliteWorldList = SqliteManager.GetWorlds();
                List<InsideWorld> SqliteInsideWorldList = SqliteManager.GetInsideWorlds();

                foreach(var world in SqlWorldList)
                {
                    World w = new World();
                    w.Sl = world.Sl;
                    w.Name = world.Name.ToString();
                    w.Image = "W" + world.Sl;
                    w.TargetedToy = int.Parse(world.TargetedToy.ToString());
                    w.IsReady = 0;
                    w.UpdateDate = world.UpdateDate.ToString();

                    if (SqliteWorldList.Exists(x => x.Sl == world.Sl))
                    {
                        World _w = SqliteWorldList.Find(x => x.Sl == world.Sl);
                        DateTime sqlUpdateDate;
                        DateTime sqliteUpdateDate;
                        DateTime.TryParse(world.UpdateDate, out sqlUpdateDate);
                        DateTime.TryParse(_w.UpdateDate, out sqliteUpdateDate);

                        if (sqlUpdateDate.Date > sqliteUpdateDate.Date)
                        {
                            //update
                            SqliteManager.UpdateDataIntoWorld(w);
                        }
                    }
                    else
                    {
                        //insert
                        SqliteManager.InsertDataIntoWorld(w);
                    }
                 }

                foreach (var world in SqliteWorldList)
                {
                    if (!SqlWorldList.Exists(x => x.Sl == world.Sl))
                    {
                        //delete
                        SqliteManager.DeleteDataFromWorld(world.Sl);
                    }
                }

                foreach (var insideWorld in SqlInsideWorldList)
                {
                    InsideWorld iw = new InsideWorld();
                    iw.Sl = insideWorld.Sl;
                    iw.ColorImage = "IWC" + insideWorld.Sl;
                    iw.BWImage = "IWB" + insideWorld.Sl;
                    iw.FinishingTime = insideWorld.FinishingTime;
                    iw.WorldId = insideWorld.WorldId;
                    iw.UpdateDate = insideWorld.UpdateDate.ToString();
                    iw.IsReady = 0;

                    if (SqliteInsideWorldList.Exists(x => x.Sl == insideWorld.Sl))
                    {
                        InsideWorld _iw = SqliteInsideWorldList.Find(x => x.Sl == insideWorld.Sl);
                        DateTime sqlUpdateDate;
                        DateTime sqliteUpdateDate;
                        DateTime.TryParse(insideWorld.UpdateDate, out sqlUpdateDate);
                        DateTime.TryParse(_iw.UpdateDate, out sqliteUpdateDate);

                        if (sqlUpdateDate.Date > sqliteUpdateDate.Date)
                        {
                            //update
                            iw.IsComplete = _iw.IsComplete;
                            iw.AchievedToy = _iw.AchievedToy;
                            SqliteManager.UpdateDataIntoInsideWorld(iw);
                        }
                    }
                    else
                    {
                        //insert
                        iw.IsComplete = 0;
                        iw.AchievedToy = 0;
                        SqliteManager.InsertDataIntoInsideWorld(iw);
                    }
                }

                foreach (var insideWorld in SqliteInsideWorldList)
                {
                    if (!SqlInsideWorldList.Exists(x => x.Sl == insideWorld.Sl))
                    {
                        //delete
                        SqliteManager.DeleteDataFromInsideWorld(insideWorld.Sl);
                    }
                }
                Debug.Log("Completed");
            }
            else
            {
                Debug.Log("Disconnected");
            }
        }));
        DownloadImage();
    }

    void DownloadImage()
    {
        StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                Debug.Log("Connected");
                List<World> SqliteWorldList = SqliteManager.GetWorlds();
                List<InsideWorld> SqliteInsideWorldList = SqliteManager.GetInsideWorlds();

                foreach (var world in SqliteWorldList)
                {
                    if (world.IsReady == 0)
                    {
                        //download
                        string filename = "W" + world.Sl + ".jpg";
                        Texture2D worldImage = SqlManager.LoadImageFromWorld(world.Sl);
                        ImageManager.SaveImage(worldImage, ImageManager.GetImageSaveLocation(), filename);
                        SqliteManager.UpdateIsReadyIntoWorld(world.Sl);
                    }
                }
                foreach (var insideWorld in SqliteInsideWorldList)
                {
                    if (insideWorld.IsReady == 0)
                    {
                        //download
                        string colorImageName = "IWC" + insideWorld.Sl + ".jpg";
                        string bwImageName = "IWB" + insideWorld.Sl + ".jpg";
                        Texture2D insideWorldColorImage = SqlManager.LoadImageFromInsideWorld(insideWorld.Sl);
                        ImageManager.SaveImage(insideWorldColorImage, ImageManager.GetImageSaveLocation(), colorImageName);
                        Texture2D insideWorldBWImage = ImageManager.ConvertToGrayscale(insideWorldColorImage);                        
                        ImageManager.SaveImage(insideWorldBWImage, ImageManager.GetImageSaveLocation(), bwImageName);
                        SqliteManager.UpdateIsReadyIntoInsideWorld(insideWorld.Sl);
                    }
                }
                Debug.Log("Completed");
            }
            else
            {
                Debug.Log("Disconnected");
            }
        }));
    }
}
    
