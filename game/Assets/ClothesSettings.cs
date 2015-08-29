using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class ClothesSettings : MonoBehaviour {

    public Images[] types;

    [Serializable]
    public class Images
    {
        public List<string> glasses;
        public List<string> shoes;
        public List<string> tops;
        public List<string> hairs;
        public List<string> legs;
        public List<string> faces;
    }

	void Start () {
        LoadArray(types[0].glasses, @"Assets\Resources\Victims\A\Body");
        //LoadArray(shoes, @"images\shoes\");
        //LoadArray(tops, @"images\top\");
        //LoadArray(hairs, @"images\hair\");
        //LoadArray(legs, @"images\bottom\");
        //LoadArray(faces, @"images\face\");
	}

    private void LoadArray(List<string> arr, string path)
    {
        string lastName = "aaaa";

        foreach (string name in System.IO.Directory.GetFiles(path, "*.png"))
        {
            String[] textSplit = name.Split("/"[0])[0].Split("_"[0]);


            string realName = textSplit[0] + "_" + textSplit[1];

            if (realName != lastName)
            {
                arr.Add(realName);
                lastName = realName;
            }
        }
    }
}
