using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{
    public float minSwipeDistY;
    public float minSwipeDistX;
    private Vector2 startPos;

    public enum directions
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    private float newTime;
    private bool touched;
    private float timeSinceTouch;

    void Update()
	{
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches[0];
			switch (touch.phase) 
			{
			    case TouchPhase.Began:
                    timeSinceTouch = -0.5f;
                    newTime = 0;
                    touched = true;
				    startPos = touch.position;				
				    break;					
			    case TouchPhase.Ended:
                    newTime = 0;
                    touched = false;
                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                    if (timeSinceTouch < 0.06f && swipeDistVertical < minSwipeDistY)
                    {
                      //  Events.OnHeroJump();
                    } else {
                        Move(touch.position.y);
                    }
				    break;
			}


            if (touched)
            {
                newTime += Time.deltaTime;
                timeSinceTouch += Time.deltaTime;
            }

            if (newTime > 0.06f && touched)
            {
                Move(touch.position.y);
                startPos = touch.position;
                newTime = -0.5f;
            }


		}
	}
    void Move(float touchFinalPositionY)
    {
        float swipeDistVertical = (new Vector3(0, touchFinalPositionY, 0) - new Vector3(0, startPos.y, 0)).magnitude;
        if (swipeDistVertical > minSwipeDistY/2)
        {
            float swipeValue = Mathf.Sign(touchFinalPositionY - startPos.y);
            if (swipeValue > 0)
                Swipe(directions.UP);
            else if (swipeValue < 0)
                Swipe(directions.DOWN);
        }
    }
    void Swipe(directions direction)
    {
        print(direction);

        switch (direction)
        {
            case directions.UP:
                Events.OnSwipe(directions.UP); break;
            case directions.DOWN:
                Events.OnSwipe(directions.DOWN); break;
        }
    }
}