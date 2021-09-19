using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour
{
    // touch variable will contain the info about the touch on the screen, such as when the finger just touched the screen
    // or when it moved on the screen or when it was lifted from the screen, etc.
    private Touch touch;

    // speedModifier variable will control how fast does the 3D car model move in the real world when we do the DragToMove gesture on the screen.
    private float speedModifier;

    private void Start()
    {
        speedModifier = 0.001f;
    }

    private void Update()
    {
        if(Input.touchCount>0)      // ie at least one finger touches the screen
        {
            touch = Input.GetTouch(0);      // getting the first touch in case multi-touch happens.

            if(touch.phase == TouchPhase.Moved)     // ie if the user has performed the move gesture with his finger
            {
                // updating the car object's position in the AR scene depending up on how much did our finger move on the screen.
                // the x coordinate is the original position of the car + touch.deltaPosition.x (ie how much our finger moved on the screen in the x-axis for that particular frame) * speedModifier
                // the y coordinate will be the same since we are only moving the object in the x-z plane.
                // the z coordinate is same as x except we consider our finger movement of y direction because the mobile screen is 2D, it will just have x and y components.
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier, transform.position.y, transform.position.z + touch.deltaPosition.y * speedModifier);
            }
        }
    }
}
