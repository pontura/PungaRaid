using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{
    public float minSwipeDistY;
    public float minSwipeDistX;
    private float startPosY;

    public enum directions
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    private float newTime;
    private bool touched;
    private bool movedByTime;
    private float timeSinceTouch;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    movedByTime = false;
                    newTime = 0;
                    touched = true;
                    startPosY = touch.position.y;
                    break;
                case TouchPhase.Ended:
                    newTime = 0;
                    touched = false;
                    if (!movedByTime)
                        Move(touch.position.x, touch.position.y);
                    break;
            }

            if (touched)
            {
                newTime += Time.deltaTime;
            }

            if (newTime > 0.06f && touched)
            {
                Move(touch.position.x, touch.position.y);
                startPosY = touch.position.y;
                movedByTime = true;                
                newTime = -0.12f;
            }


        }
    }
    void Move(float touchFinalPositionX, float touchFinalPositionY)
    {
        if (Mathf.Abs(touchFinalPositionX) < Mathf.Abs(touchFinalPositionY))
        {
            float swipeDistVertical = (new Vector3(0, touchFinalPositionY, 0) - new Vector3(0, startPosY, 0)).magnitude;
            if (swipeDistVertical > minSwipeDistY / 2)
            {
                float swipeValue = Mathf.Sign(touchFinalPositionY - startPosY);
                if (swipeValue > 0)
                    Swipe(directions.UP);
                else if (swipeValue < 0)
                    Swipe(directions.DOWN);
            }
        }
        else
        {
            Swipe(directions.RIGHT);
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
            case directions.RIGHT:
                Events.OnSwipe(directions.RIGHT); break;
        }
    }
}