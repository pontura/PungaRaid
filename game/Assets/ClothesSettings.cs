using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class ClothesSettings : MonoBehaviour {

    public Parts[] parts;

    [Serializable]
    public class Parts
    {
        public List<ClothesPart> Clothes;
        public List<FacesPart> Faces;
        public List<HairsPart> Hairs;
    }
    [Serializable]
    public class ClothesPart
    {
        public string part;
        public string sex;
        public string style;
        public string texture;
        public string subPart;
    }

    [Serializable]
    public class FacesPart
    {
        public string type;
        public string sex;
        public string expresion;
    }

    [Serializable]
    public class HairsPart
    {
        public string type;
        public string sex;
    }
	void Start () {
        List<string> clothes = LoadArray(@"Assets\Resources\Victims\A\Clothes");
        List<string> faces = LoadArray(@"Assets\Resources\Victims\A\Face");
        List<string> hairs = LoadArray(@"Assets\Resources\Victims\A\Hair");

        GetClothes(clothes, parts[0].Clothes);
        GetFaces(faces, parts[0].Faces);
        GetHairs(hairs, parts[0].Hairs);
	}
    private void GetClothes(List<string> arr, List<ClothesPart> Arr)
    {
        foreach (string path in arr)
        {
            String[] textSplit = path.Split("_"[0]);

            ClothesPart part = new ClothesPart();
            part.part = textSplit[0];
            part.sex = textSplit[1];
            part.style = textSplit[2];
            part.texture = textSplit[3];
            part.subPart = textSplit[4];

            Arr.Add(part);
        }
    }
    private void GetFaces(List<string> arr, List<FacesPart> Arr)
    {
        foreach (string path in arr)
        {
            String[] textSplit = path.Split("_"[0]);

            FacesPart part = new FacesPart();
            part.type = textSplit[1];
            part.sex = textSplit[0];
            part.expresion = textSplit[2];

            Arr.Add(part);
        }
    }
    private void GetHairs(List<string> arr, List<HairsPart> Arr)
    {
        foreach (string path in arr)
        {
            String[] textSplit = path.Split("_"[0]);

            HairsPart part = new HairsPart();
            part.type = textSplit[3];
            part.sex = textSplit[1];

            Arr.Add(part);
        }
    }
    private List<string> LoadArray(string path)
    {
        List<string> newArr = new List<string>();

        foreach (string name in System.IO.Directory.GetFiles(path, "*.png"))
        {
            String[] textSplit = name.Split(@"\"[0]);
            String[] textSplit2 = textSplit[5].Split("."[0]);
            newArr.Add(textSplit2[0]);
        }
        return newArr;
    }
    public List<string> GetRandomCustomization()
    {
        List<string> newParts = new List<string>();

        ClothesPart clothesPart = new ClothesPart();
        clothesPart = parts[0].Clothes[0];

        HairsPart hairsPart = new HairsPart();
        hairsPart = parts[0].Hairs[0];

        FacesPart facesPart = new FacesPart();
        facesPart = parts[0].Faces[0];
        
       // newParts.Add(clothesPart.part);

        newParts.Add(clothesPart.part + "_" + clothesPart.sex + "_" + clothesPart.style+ "_" + clothesPart.texture + "_" + clothesPart.subPart);
        newParts.Add("hair_" + hairsPart.sex + "_1_" +  hairsPart.type + "_A");
        newParts.Add("face_" + facesPart.sex + "_" + facesPart.type + "_" + facesPart.expresion);

        return newParts;
    }
}
