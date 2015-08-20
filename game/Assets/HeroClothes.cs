using UnityEngine;
using System.Collections;

public class HeroClothes : MonoBehaviour {

    public GameObject[] hats;
    public GameObject[] chairs;
    public GameObject[] legs;
    public GameObject[] legs2;

    public GameObject itemsParticles;

	void Start () {


        if (PlayerPrefs.GetInt("hats") > 0)
            OnHeroClothes("hats", PlayerPrefs.GetInt("hats"));
        if (PlayerPrefs.GetInt("chairs") > 0)
            OnHeroClothes("chairs", PlayerPrefs.GetInt("chairs"));
        if (PlayerPrefs.GetInt("legs") > 0)
            OnHeroClothes("legs", PlayerPrefs.GetInt("legs"));

    }
    void OnDestroy()
    {

    }
    void OnHeroWinClothes(string type, int num)
    {
        GameObject particles = Instantiate(itemsParticles, Vector3.zero, Quaternion.identity) as GameObject;
        particles.transform.SetParent(transform);
        particles.transform.localScale = Vector3.one;
        
        OnHeroClothes(type, num);
        particles.GetComponent<Animator>().Play("magicBlast");

        if (type == "legs") particles.transform.SetParent(legs[0].gameObject.transform.parent);
        else if (type == "hats") particles.transform.SetParent(hats[0].gameObject.transform.parent);
        else if (type == "chairs") particles.transform.SetParent(chairs[0].gameObject.transform.parent);        

        particles.transform.localPosition = Vector3.zero;
    }
    void OnHeroClothes(string type, int num)
    {
        GameObject[] clothes;
        switch (type)
        {
            case "hats": clothes = hats; break;
            case "chairs": clothes = chairs; break;
            default: clothes = legs; break;
        }
        
        foreach (GameObject cloth in clothes)
            cloth.SetActive(false);

        clothes[num - 1].SetActive(true);

        if (type == "legs")
        {
            foreach (GameObject cloth in legs2)
                cloth.SetActive(false);
            legs2[num - 1].SetActive(true);
        }

    }
}
