using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{

    public static string assetPath;
    public static string externalFolderLocation = "/mnt/sdcard/DCIM/Camera/";
    public static string fileName = "Image.png";
    void Start()
    {
        assetPath = Application.persistentDataPath;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static string getFullPathOfSprite(string fileName)
    {
        assetPath = Application.dataPath;
        //return Path.Combine(Path.Combine(assetPath,"Sprites"), fileName);
        return assetPath + "/Sprites/" + fileName;
    }
    public static Texture2D LoadImageFromSprite(string fileName)
    {
        string fullPAth = getFullPathOfSprite(fileName);
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(fullPAth))
        {
            fileData = File.ReadAllBytes(fullPAth);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
        }
        return tex;
    }
    public static void SaveImage(Texture2D texture, string fileLocation, string fileName)
    {
        var bytes = texture.EncodeToPNG();
        Others.CreateDirectory(fileLocation);
        File.SetAttributes(fileLocation, FileAttributes.Normal);
        File.WriteAllBytes(fileLocation + fileName, bytes);
        //yield return new WaitForEndOfFrame();

    }
    public static void MoveAsset(string fromLocation, string toLocation, string fileName)
    {
        if (File.Exists(fromLocation+fileName))
        {
            Others.CreateDirectory(toLocation);
            File.Move(fromLocation + fileName, toLocation + fileName);
        }
        //yield return new WaitForEndOfFrame();
    }
    public static string GetImageSaveLocation()
    {
        return Application.persistentDataPath + "/Resources/";
    }

}
