using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SliceController : MonoBehaviour {

    public Texture2D source;
    public int slice = 3;

    // Use this for initialization
    void Start()
    {
        List<GameObject> gameObjects = new List<GameObject>();

        GameObject spritesRoot = GameObject.Find("SpritesRoot");
        float width = source.width / slice;
        float height = source.height / slice;

        for (int i = 0; i < slice; i++)
        {
            for (int j = 0; j < slice; j++)
            {
                Sprite newSprite = Sprite.Create(source, new Rect(i * width, j * height, width, height), new Vector2(0.5f, 0.5f));
                GameObject gameObject = new GameObject();

                RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
                rectTransform.position = new Vector3(0, 0, 0);
                rectTransform.sizeDelta = new Vector2(200, 200);

                Image image = gameObject.AddComponent<Image>();
                image.sprite = newSprite;
                gameObject.transform.position = new Vector3(i * 300, j * 300, 0);
                gameObject.transform.parent = spritesRoot.transform;
                gameObjects.Add(gameObject);
            }
        }
    }

    public static Sprite[] GetSliceGameObjectTexture(Texture2D source, int slice)
    {
        Sprite[] gameObjects = new Sprite[slice*slice];

        GameObject spritesRoot = GameObject.Find("SpritesRoot");
        float width = source.width / slice;
        float height = source.height / slice;
        int counter=0;
        for (int i = 0; i < slice; i++)
        {
            for (int j = 0; j < slice; j++)
            {
                Sprite newSprite = Sprite.Create(source, new Rect(i * width, j * height, width, height), new Vector2(0.5f, 0.5f));
                //GameObject gameObject = new GameObject();

                //Image image = gameObject.AddComponent<Image>();
                //image.sprite = newSprite;

                gameObjects[counter] = newSprite;
                counter++;
            }
        }
        return gameObjects;
    }
}
