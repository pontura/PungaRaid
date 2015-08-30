using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class Clothes : MonoBehaviour
{
    public List<string> clothes;

    public SpriteRenderer headSkin;
    public SpriteRenderer bodySkin;
    public SpriteRenderer hipsSkin;

    public SpriteRenderer arm1Skin1;
    public SpriteRenderer arm1Skin2;
    public SpriteRenderer arm2Skin1;
    public SpriteRenderer arm2Skin2;

    public SpriteRenderer leg1Skin1;
    public SpriteRenderer leg1Skin2;
    public SpriteRenderer leg2Skin1;
    public SpriteRenderer leg2Skin2;
    

    public SpriteRenderer faceA;
    public SpriteRenderer faceB;

    public SpriteRenderer hairA;
    public SpriteRenderer hairB;

    public SpriteRenderer bodyTop;

    public SpriteRenderer arm1TopA;
    public SpriteRenderer arm1TopB;
    public SpriteRenderer arm2TopA;
    public SpriteRenderer arm2TopB;

    public SpriteRenderer leg1BottomA;
    public SpriteRenderer leg1BottomB;
    public SpriteRenderer leg2BottomA;
    public SpriteRenderer leg2BottomB;

    public SpriteRenderer objectsBag;
    public SpriteRenderer objectsPhones;

    private ClothesSettings clothSettings;

    void Start()
    {
        clothSettings = Data.Instance.clothesSettings;
        Dress("A", "M");
    }
    void Dress(string type, string sex)
    {
        string hair = clothSettings.GetHair(type, sex);
        hairA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Hair/hair_" + sex + "_1_" + hair + "_A");
        hairB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Hair/hair_" + sex + "_1_" + hair + "_B");
    }
}
