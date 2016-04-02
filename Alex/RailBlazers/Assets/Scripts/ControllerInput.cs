using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {
	public float rotSpeed;
    /*
    Set up in editor
        Input manager must match appropriate names and values for Xbox controller
        http://wiki.unity3d.com/images/thumb/a/a7/X360Controller2.png/600px-X360Controller2.png
        
        This code requires a deadzone on left stick and right stick > .2

        Current Names - Can be changed in editor
        Left stick - Horizontal, Vertical
        Right stick - RightStick, RightSticker
        A button - A
        B button - B
        X button - X
        Y button - Y
        Right bumper - RightBump
        Left bumper - LeftBump

        Shields are empty game objects with the script attached, named appropriately
        Shield is child object resting to the right of the game object at distance specified in GDD
        Rotation of stick moves parent object, shield rotates appropriately
        
    */
    void Update()
    {
        #region buttons
        if (Input.GetButtonDown("A"))
        {
            Debug.Log("Button A pressed");
        }
        if (Input.GetButtonDown("B"))
        {
            Debug.Log("Button B pressed");
        }
        if (Input.GetButtonDown("X"))
        {
            Debug.Log("Button X pressed");
        }
        if (Input.GetButtonDown("Y"))
        {
            Debug.Log("Button Y pressed");
        }

        if (Input.GetButton("LeftBump"))
        {
            Debug.Log("Holding left bumper");
        }
        if (Input.GetButton("RightBump"))
        {
            Debug.Log("Holding right bumper");
        }
        #endregion
        #region Sticks
        /*
        Controls for left and right stick, based on input and object affiliation
        Currently jumps to position immediately
        If we want it to lerp, 
        angles > 0 are represented by 360 - angle
        angles < 0 are represented by Mathf.Abs(angle)
        This returns the appropriate z value in rotation for the object
        Apply using this as target in Lerp/Slerp
        */
        if (this.gameObject.name == "LeftStickHome")//Looks for left stick input
        {//Create vector for input horizontal and vert, the normalize
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Returns angle whose tangent is y/x
            if (direction.magnitude > 0.0f)//If input
            {
				Vector3 newAngles = Vector3.zero;

				newAngles.z = Mathf.LerpAngle(transform.eulerAngles.z,-angle, rotSpeed * Time.deltaTime);
                transform.eulerAngles = newAngles;//apply to euler
            }
        }
        if (this.gameObject.name == "RightStickHome")//Looks for right stick input
        {//Create vector for input horizontal and vert, the normalize
            Vector3 rightDirection = new Vector3(Input.GetAxis("rightStickH"), Input.GetAxis("rightStickV"), 0f).normalized;
            float rightAngle = Mathf.Atan2(rightDirection.y, rightDirection.x) * Mathf.Rad2Deg;//Returns angle whose tangent is y/x
            if (rightDirection.magnitude > 0.0f)
            {
				Vector3 newRightAngles = Vector3.zero;

				newRightAngles.z = Mathf.LerpAngle(transform.eulerAngles.z,-rightAngle, rotSpeed * Time.deltaTime);
                transform.eulerAngles = newRightAngles;
            }
        }
    #endregion        
    }
}
