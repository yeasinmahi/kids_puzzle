using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Screenshoot : MonoBehaviour {
    public static RectTransform rectT; 
    static int width;
    static int height;
    float scaleX;
    float scaleY;
    float scalingFraction;
    float screenHeight;
    float screenWidth;

    void Start () {
        rectT = gameObject.GetComponent<RectTransform>();
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        scaleX = screenHeight / 600;
        scaleY = screenWidth / 800;
        if (scaleX > scaleY)
        {
            scalingFraction = scaleX;
        }
        else
        {
            scalingFraction = scaleY;
        }
        height = System.Convert.ToInt32(scalingFraction * rectT.rect.height);
        width = System.Convert.ToInt32(scalingFraction * rectT.rect.width);
        Debug.Log("scaleX: " + scaleX.ToString());
        Debug.Log("scaleY: " + scaleY.ToString());

        Debug.Log("Height: "+height.ToString());
        Debug.Log("Width: " + width.ToString());

    }
    private static string ScreenshootSaveLocation = "/Resources/";
    public static string ScreenshootImageName = "Screenshoot.png";
    public static string ScreenshootFullLocation = ScreenshootSaveLocation + ScreenshootImageName;

    public static IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame();

        Vector2 temp = rectT.transform.position;
        Vector3[] worldPosition = new Vector3[4];
        rectT.GetWorldCorners(worldPosition);

        var tex = GetTexture2DScreenshoot();
        tex.ReadPixels(new Rect(worldPosition[0].x, worldPosition[0].y, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(Application.dataPath + ScreenshootFullLocation, bytes);

    }
    public static Texture2D GetTexture2DScreenshoot()
    {
        return new Texture2D(width, height, TextureFormat.RGB24, false);
    }
    private IEnumerator load_image_preview(string _path)
    {
        WWW www = new WWW(_path);
        yield return www;
        Texture2D texTmp = new Texture2D(256, 256, TextureFormat.RGB24, false);

        www.LoadImageIntoTexture(texTmp);
    }
    //public static void CaptureScreenShot()
    //{
    //    Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    //    if (tex != null)
    //    {
    //        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
    //        tex.Apply();
    //        var bytes = tex.EncodeToPNG();
    //        if (bytes.Length > 0)
    //        {
    //            Debug.Log("Texture got");
    //            SaveTextureToFile(tex, ScreenshootFullLocation);
    //        }
    //    }
    //}


    //private static void SaveTextureToFile(Texture2D tex, string fileName)
    //{
    //    var bytes = tex.EncodeToPNG();
    //    var file = File.Open(ScreenshootFullLocation, FileMode.Create);
    //    var binary = new BinaryWriter(file);
    //    binary.Write(bytes);
    //    file.Close();
    //}

    // Update is called once per frame
}
