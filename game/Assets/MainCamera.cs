using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
    	
    public void UpdatePosition(float _x)
    {
        Vector3 pos =  transform.localPosition;
        pos.x = _x + 2;
        transform.localPosition = pos;
	}
}
