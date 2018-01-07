using System.IO;
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
        Debug.Log(key+": " + value);
    }
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
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
            int r = Random.Range(t, sprites.Length);
            sprites[t] = sprites[r];
            sprites[r] = tmp;
        }
        return sprites;
    }
    public static Sprite CreateSpriteFromTexture(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);
    }
}
