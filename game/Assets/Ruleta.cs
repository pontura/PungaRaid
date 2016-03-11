using UnityEngine;
using System.Collections;
using System;

public class Ruleta : MonoBehaviour {

    public float InitialSpeed;
    public float acceleration;

    public GameObject container;
    public RuletaItem ruletaItem;
    public Item[] items;
    public int itemsHeight;

    [Serializable]
    public class Item
    {
        public Color color;
        public Sprite asset;
        public int id;
        public int zone;
        public int itemID;
    }
    public states state;
    public enum states
    {
        IDLE,
        ROLLING,
        REPOSITION,
        FINISH
    }
    public float speed;
    public int totalHeight;
    public float offsetY;
    public float repositionTo;

	public void Init () {
        
        offsetY = container.transform.localPosition.y;
        foreach (Item item in items)
            Add(item);

        Add(items[0]);

        totalHeight = items.Length * itemsHeight;
	}
    void Add(Item item)
    {
        RuletaItem newItem = Instantiate(ruletaItem);
        newItem.transform.SetParent(container.transform);
        newItem.transform.transform.localScale = Vector2.one;
        newItem.Init(item.id, item.color, item.asset, itemsHeight);
    }
    public void RuletaOn()
    {
        InitialSpeed = UnityEngine.Random.Range(15, 50);
        speed = InitialSpeed;
        state = states.ROLLING;
    }
    void Update()
    {
        if (state == states.ROLLING) Rolling();
        else if (state == states.REPOSITION) Repositionate();
    }
   
    void Rolling()
    {
        speed -= Time.deltaTime + acceleration;
        float newY = container.transform.localPosition.y + speed;

        if (speed <= 0)
        {
            CalculateItem();
            state = states.REPOSITION;            
        }

        if (container.transform.localPosition.y > (totalHeight + offsetY))
            ResetPosition();
        else
            container.transform.localPosition = new Vector3(0, newY, 0);
    }
    private void CalculateItem()
    {
        int _y = (int)Mathf.Round((container.transform.localPosition.y-offsetY)/itemsHeight);
        repositionTo = (itemsHeight * _y) + offsetY ;
        //Debug.Log("totalHeight" + totalHeight + " y: " + _y + " container.transform.localPosition.y : " + container.transform.localPosition.y);
    }
    void Repositionate()
    {
        if (container.transform.localPosition.y > repositionTo)
            RepositionateUp();
        else
            RepositionateDown();
    }
    void RepositionateUp()
    {
        speed += Time.deltaTime + acceleration;
        float newY = container.transform.localPosition.y - speed;
        container.transform.localPosition = new Vector3(0, newY, 0);

        if (container.transform.localPosition.y <= repositionTo) Ready();

    }
    void RepositionateDown()
    {
        speed += Time.deltaTime + acceleration;
        float newY = container.transform.localPosition.y + speed;
        container.transform.localPosition = new Vector3(0, newY, 0);

        if (container.transform.localPosition.y >= repositionTo) Ready();

    }
    void Ready()
    {
        container.transform.localPosition = new Vector3(0, repositionTo, 0);
        state = states.FINISH;
        GetComponent<RuletaPopup>().Open();
    }
    void ResetPosition()
    {
        container.transform.localPosition = new Vector3(0, offsetY, 0);
    }
}
