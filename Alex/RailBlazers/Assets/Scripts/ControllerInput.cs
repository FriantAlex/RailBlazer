﻿using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {
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
    public float rotSpeed;
    public float shieldDelay;
    public bool bashing;
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
        if (Input.GetButtonDown("LeftBump"))
        {
            //Debug.Log("Hit left bumper");
            if(this.gameObject.name == "LeftStickHome")
                StartCoroutine(ShieldBash());
        }
        if (Input.GetButtonDown("RightBump"))
        {
            //Debug.Log("Hit right bumper");
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

				newAngles.z = Mathf.LerpAngle(transform.eulerAngles.z,-angle + 90, rotSpeed * Time.deltaTime);
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

				newRightAngles.z = Mathf.LerpAngle(transform.eulerAngles.z,-rightAngle + 90, rotSpeed * Time.deltaTime);
                transform.eulerAngles = newRightAngles;
            }
        }
        #endregion

        if (bashing)
        {

        }
        else
        {

        }
    }
    /*Moves shield forward dependent on position to bash enemies.
    Boolean bashing can be used to deal damage/kill enemies that come in contact with the shield
    Shield delay is the amount of time the shield is shot out
    Potentially add Lerp
    */
    private IEnumerator ShieldBash()
    {
        if (!bashing)
        {
            Debug.Log("shield bash!");
            bashing = true;
            Vector3 shieldPosLocal = transform.GetChild(0).transform.localPosition;
            Vector3 resetShield = shieldPosLocal;
            shieldPosLocal.y += .5f;
            transform.GetChild(0).transform.localPosition = shieldPosLocal;
            yield return new WaitForSeconds(shieldDelay);
            transform.GetChild(0).transform.localPosition = resetShield;
            bashing = false;
        }
    }
}
