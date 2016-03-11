using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuletaItem : MonoBehaviour {

    public int id;
    public Color color;
    
    public void Init(int id, Color color, Sprite item, int height)
    {
        this.id = id;
        GetComponent<Image>().color = color;
        GetComponent<LayoutElement>().minHeight = height;
    }
}
