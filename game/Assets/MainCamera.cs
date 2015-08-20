using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
    	
    public void UpdatePosition(float _x)
    {
        Vector3 pos =  transform.localPosition;
        pos.x = _x;
        transform.localPosition = pos;
	}
}
