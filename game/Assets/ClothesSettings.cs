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
        string lastType = ""; 
        foreach (string path in arr)
        {
            String[] textSplit = path.Split("_"[0]);

            HairsPart part = new HairsPart();
            part.type = textSplit[3];
            part.sex = textSplit[1];

            
            if(lastType != part.type)
                Arr.Add(part);
            lastType = part.type;
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
    public string GetHair(string type, string sex)
    {
        int id = 0;
        HairsPart hairsPart = GetRandomHair(id, sex);
        return hairsPart.type;
    }
    HairsPart GetRandomHair(int _type, string sex)
    {
        List<HairsPart> newList = new  List<HairsPart>();
        foreach (HairsPart part in parts[_type].Hairs)
        {
            if (part.sex == sex)
                newList.Add(part);
        }
        return newList[UnityEngine.Random.Range(0,newList.Count)];
    }
}
