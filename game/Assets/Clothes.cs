using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class Clothes : MonoBehaviour
{
    public List<string> clothes;

    public SpriteRenderer[] c_hair1;
    public SpriteRenderer[] c_hair2;

    GameObject[] gameObj;
    Texture2D[] textList;

   // private string pathPreFix;
    private ClothesSettings clothSettings;
   // private SavedSettings savedSettings;

    void Start()
    {
        clothSettings = Data.Instance.clothesSettings;
        clothes = clothSettings.GetRandomCustomization();
    }


    //private IEnumerator LoadImages(SpriteRenderer spriteContainer)
    //{
    //    // print("loading: " + pathTemp);
    //   // WWW www = new WWW(pathTemp);
    //    //yield return www;

    //    //if (www.error != null)
    //    //{
    //    //    spriteContainer.sprite = null;
    //    //}
    //    //else
    //    //{
    //    //    Sprite sprite = new Sprite();

    //    //    sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0), 100.0f);

    //    //    spriteContainer.sprite = sprite;
    //    //}
    //}
}
