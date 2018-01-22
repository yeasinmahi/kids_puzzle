using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Others : MonoBehaviour
{
    public static int worldId = 0;
    public static int insideWorldId = 0;
    public enum MyAudioType
    {
        Matching,
        Mismatching
    }
    public static void WriteDebugLog(string key, string value)
    {
        Debug.Log(key + ": " + value);
    }
    public static bool CreateDirectory(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    private const string MediaStoreImagesMediaClass = "android.provider.MediaStore$Images$Media";
    private static AndroidJavaObject _activity;
    public static string SaveImageToGallery(Texture2D texture2D, string title, string description)
    {
        using (var mediaClass = new AndroidJavaClass(MediaStoreImagesMediaClass))
        {
            using (var cr = Activity.Call<AndroidJavaObject>("getContentResolver"))
            {
                var image = Texture2DToAndroidBitmap(texture2D);
                var imageUrl = mediaClass.CallStatic<string>("insertImage", cr, image, title, description);
                return imageUrl;
            }
        }
    }

    public static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D texture2D)
    {
        byte[] encoded = texture2D.EncodeToPNG();
        using (var bf = new AndroidJavaClass("android.graphics.BitmapFactory"))
        {
            return bf.CallStatic<AndroidJavaObject>("decodeByteArray", encoded, 0, encoded.Length);
        }
    }
    public static AndroidJavaObject Activity
    {
        get
        {
            if (_activity == null)
            {
                var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                _activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return _activity;
        }
    }
    public static Sprite[] SliceTexureintoSprite(Texture2D source, int slice)
    {
        Sprite[] sprites = new Sprite[slice * slice];
        float width = source.width / slice;
        float height = source.height / slice;
        int counter = 0;
        for (int i = 0; i < slice; i++)
        {
            for (int j = 0; j < slice; j++)
            {
                Sprite newSprite = Sprite.Create(source, new Rect(i * width, j * height, width, height), new Vector2(0.5f, 0.5f));
                //GameObject gameObject = new GameObject();

                //Image image = gameObject.AddComponent<Image>();
                //image.sprite = newSprite;
                newSprite.name = (counter + 1).ToString();
                sprites[counter] = newSprite;
                counter++;
            }
        }
        return sprites;
    }
    public static Sprite[] Reshuffle(Sprite[] sprites)
    {
        for (int t = 0; t < sprites.Length; t++)
        {
            Sprite tmp = sprites[t];
            int r = UnityEngine.Random.Range(t, sprites.Length);
            sprites[t] = sprites[r];
            sprites[r] = tmp;
        }
        return sprites;
    }
    public static Sprite CreateSpriteFromTexture(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);
    }
    public static string GetStreamingAssetsPath()
    {
        string path;
#if UNITY_EDITOR
        path = string.Format(@"Assets/StreamingAssets/");
#elif UNITY_ANDROID
     path = "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IOS
     path = Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_OSX
     path = Application.dataPath + "/Resources/Data/StreamingAssets/";
#else
     //Desktop (Mac OS or Windows)
     path = Application.dataPath + "/StreamingAssets/";
#endif

        return path;
    }
    public static string GetStreamingAssetsPath(string fileName)
    {
        return GetStreamingAssetsPath() + fileName;
    }
    public static string GetPersistentDataPath()
    {
        return Application.persistentDataPath;
    }
    public static string GetPersistentDataPath(string fileName)
    {
        return string.Format("{0}/{1}", GetPersistentDataPath(), fileName);
    }

    public static void MoveAsset(string source, string destination)
    {
        if (!File.Exists(destination))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            WWW www = new WWW(source);
            while (!www.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(destination, www.bytes);
#elif UNITY_IOS
                File.Copy(source, destination);
		
#elif UNITY_STANDALONE_OSX
		File.Copy(source, destination);
#else
	File.Copy(source, destination);

#endif

            Debug.Log("Database written");
        }
    }
    public static void MoveAssetStreamingToPersistendDataPath(string fileName)
    {
        MoveAsset(GetStreamingAssetsPath(fileName), GetDestinationPath(fileName));
    }
    public static string GetDestinationPath(string fileName)
    {
        string destinationFile =GetPersistentDataPath(fileName);
#if UNITY_EDITOR
        destinationFile = GetStreamingAssetsPath(fileName);
#endif
        return destinationFile;
    }
    public static void get()
    {
        TextAsset[] l_assets = Resources.LoadAll("nameOfResourcesSubFolder", typeof(TextAsset)).Cast<TextAsset>().ToArray();
        for (int i = 0; i < l_assets.Length; i++)
        {
            File.WriteAllBytes(Application.persistentDataPath + "/db/" + l_assets[i].name + ".dat", l_assets[i].bytes);
        }

    }

    public static void WriteLogInText(string message)
    {
        Console.WriteLine(message);
        string path = Application.persistentDataPath + "/kidsLog.txt";
        using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
        using (StreamWriter writetext = new StreamWriter(fs))
        {
            writetext.WriteLine(message);
        }
    }

}
