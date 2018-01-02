﻿using System;
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
    //public static string getFullPathOfSprite(string fileName)
    //{

    //    assetPath = Application.dataPath;
    //    //return Path.Combine(Path.Combine(assetPath,"Sprites"), fileName);
    //    return assetPath + "/Sprites/" + fileName;
    //}
    public enum ActionType
    {
        Move,
        Copy,
        Delete
    }
    //public static Image LoadImageFromTexure2D(Texture2D texture)
    //{
    //    Image image = Instantiate(image,new Vector3(0,0,0), Quaternion.identity) as Image;
    //    image.sprite = Sprite.Create(texture: texture, rect: new Rect(0, 0, 128, 128), pivot: new Vector2());
    //    return image;
    //}


    public static Sprite LoadSpriteFromResource(string imageName)
    {
        Texture2D texture = LoadTexureFromResource(imageName);
        Sprite sprite = Sprite.Create(texture: texture, rect: new Rect(0, 0, texture.width, texture.height), pivot: new Vector2());
        return sprite;
    }
    public static Texture2D LoadTexureFromResource(string fileName)
    {
        return Resources.Load(fileName) as Texture2D;
    }
    public static void SaveImage(Texture2D texture, string fileLocation, string fileName)
    {
        byte[] bytes = texture.EncodeToPNG();
        Others.CreateDirectory(fileLocation);
        File.SetAttributes(fileLocation, FileAttributes.Normal);
        File.WriteAllBytes(fileLocation + fileName, bytes);
        //yield return new WaitForEndOfFrame();

    }
    public static void MoveAsset(string fromLocation, string toLocation, string fileName, ActionType type)
    {

        if (File.Exists(fromLocation + fileName))
        {
            Others.CreateDirectory(toLocation);
            if (type.Equals(ActionType.Move))
            {
                File.Move(fromLocation + fileName, toLocation + fileName);
            }
            else if (type.Equals(ActionType.Copy))
            {
                File.Copy(fromLocation + fileName, toLocation + fileName);
            }
            else if (type.Equals(ActionType.Delete))
            {
                File.Delete(fromLocation + fileName);
            }

        }
        //yield return new WaitForEndOfFrame();
    }
    public static string GetImageSaveLocation()
    {
        return Application.persistentDataPath + "/Resources/";
    }

}
