using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class Clothes : MonoBehaviour
{
    public string[] sex;

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
        string randomSex = sex[UnityEngine.Random.Range(0, sex.Length)];
        Dress("A", randomSex);
    }
    void Dress(string type, string sex)
    {
        string CanBeBisexual = sex;
       if (UnityEngine.Random.Range(0, 100)<30)
           CanBeBisexual = "B";

       string face = clothSettings.GetFace(0, sex);
       faceA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Face/face_" + sex + "_" + face + "_a");
       faceB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Face/face_" + sex + "_" + face + "_b");

        string hair = clothSettings.GetHair(0, sex);
        hairA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Hair/hair_" + CanBeBisexual + "_1_" + hair + "_A");
        hairB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Hair/hair_" + CanBeBisexual + "_1_" + hair + "_B");
    }
}
