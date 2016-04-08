using UnityEngine;
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
    //Precision Vars
    public float rotSpeed;
    private float startingRotSpeed;

    //Bash Vars
    public float shieldGoDelay;
    public float shieldReturnDelay;

    public float shieldGoSpeed;
    public float shieldReturnSpeed;

    public float shieldGoStretch;
    public float shieldReturnStretch;

    public float shieldDist;
    public float shieldStretchLength; 

    private float startTime;
    private float journeyLength = .01f;
    public float scaleLength = .01f;

    public bool bashing;
    public bool returned;

    Vector3 shieldPosLocal;
    Vector3 shieldResetLocal;
    Vector3 shieldScaleLocal;
    Vector3 shieldScaleReset;

    //Hide Vars
    public GameObject innerShield;
    public GameObject outerShield;
    public GameObject hideShield;
    public bool hidden;
    public bool canHide;
    public float hiddenCD;
    public float hideTime;

    //Stops audio when paused
    private AudioListener sound;

    void Start()
    {
        shieldPosLocal = transform.GetChild(0).transform.localPosition;
        shieldResetLocal = shieldPosLocal;

        shieldScaleLocal = transform.GetChild(0).transform.localScale;
        shieldScaleReset = shieldScaleLocal;

        returned = true;
		startingRotSpeed = rotSpeed;
        //Stop audio when paused
        sound = GameObject.Find("Main Camera").GetComponent<AudioListener>();

        hideShield.SetActive(false);
        canHide = true;
    }

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
            Debug.Log("Right Bump");
            if(this.gameObject.name == "LeftStickHome")
                StartCoroutine(Hide());
        }
        if (Input.GetButton("LeftClick"))
        {
            Debug.Log("Hit left click");
			rotSpeed = 1;
		}
        else
        {
			rotSpeed = startingRotSpeed;
		}

		if (Input.GetButtonDown ("Start") && this.gameObject.name == "LeftStickHome")
        {
			Debug.Log("Pressed start");
			if (Time.timeScale == 1)
            {
				Time.timeScale = 0;
                sound.enabled = false;
			}else{
				Time.timeScale = 1;
                sound.enabled = true;
			}
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
            float distCovered = (Time.time - startTime) * shieldGoSpeed;
            float scaleCovered = (Time.time - startTime) * shieldGoStretch;

            float fracJourney = distCovered / journeyLength;
            float scaleJourney = scaleCovered / scaleLength;

            transform.GetChild(0).transform.localPosition = Vector3.Lerp(shieldResetLocal, shieldPosLocal, fracJourney);
            transform.GetChild(0).transform.localScale = new Vector3(Mathf.Lerp(shieldScaleReset.x, shieldScaleLocal.x, scaleJourney), shieldScaleReset.y, shieldScaleReset.z);
        }
        else if(gameObject.name == "LeftStickHome")
        {
            float distCovered = (Time.time - startTime) * shieldReturnSpeed;
            float scaleCovered = (Time.time - startTime) * shieldReturnStretch;

            float fracJourney = distCovered / journeyLength;
            float scaleJourney = scaleCovered / scaleLength;

            transform.GetChild(0).transform.localPosition = Vector3.Lerp(shieldPosLocal, shieldResetLocal, fracJourney);
            transform.GetChild(0).transform.localScale = Vector3.Lerp(shieldScaleLocal, shieldScaleReset, scaleJourney);
        }
    }
    /*Moves shield forward dependent on position to bash enemies.
    Boolean bashing can be used to deal damage/kill enemies that come in contact with the shield
    Shield delay is the amount of time the shield is shot out
    Potentially add Lerp
    */
    private IEnumerator ShieldBash()
    {
        if (!bashing && returned)
        {
            bashing = true;
            returned = false;

            shieldPosLocal = transform.GetChild(0).transform.localPosition;
            shieldScaleLocal = transform.GetChild(0).transform.localScale;

            shieldResetLocal = shieldPosLocal;
            shieldScaleReset= shieldScaleLocal;

            shieldPosLocal.y += shieldDist;
            shieldScaleLocal.x += shieldStretchLength; //Swap from other value

            startTime = Time.time;

            journeyLength = Vector3.Distance(shieldPosLocal, shieldResetLocal);
            scaleLength = 2;

            yield return new WaitForSeconds(shieldGoDelay);
            startTime = Time.time;
            bashing = false;
            yield return new WaitForSeconds(shieldReturnDelay);
            returned = true;
        }
    }

    private IEnumerator Hide()
    {
        if (canHide)
        {
            hidden = true;
            canHide = false;
            innerShield.SetActive(false);
            outerShield.SetActive(false);
            hideShield.SetActive(true);
            if(this.gameObject.name == "LeftStickHome")
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            yield return new WaitForSeconds(hideTime);
            hidden = false;
            innerShield.SetActive(true);
            outerShield.SetActive(true);
            hideShield.SetActive(false);
            if (this.gameObject.name == "LeftStickHome")
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(hiddenCD);
            canHide = true;
        }
    }
}
