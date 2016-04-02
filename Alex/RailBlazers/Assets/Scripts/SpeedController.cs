using UnityEngine;
using System.Collections;

public class SpeedController : MonoBehaviour {

    public float waitTime;
	public float maxSpeed;
	public float acc;

	private Patrol playerController;


	// Use this for initialization
	void Start () {
		playerController = GameObject.Find("Cart").GetComponent<Patrol> ();
	}


	void OnTriggerEnter(Collider other)
    {   //speeds up cart 
		if (other.gameObject.tag == "Cart")
        {
            Debug.Log(this.gameObject.name);
            if (this.gameObject.name == "StopperStarter")
            {
                Debug.Log("StartStop engaged");
                StartCoroutine(StartStop());
                return;
            }
            Debug.Log ("Boop");
			playerController.maxPatrolSpeed = maxSpeed;
			playerController.acceleration = acc;
		}
	}

    private IEnumerator StartStop()
    {
        float curSpeed = playerController.maxPatrolSpeed;//players current max speed while en route
        float curAcc = playerController.acceleration;//players current acceleration while en route
        playerController.speed = maxSpeed;
        playerController.acceleration = acc;
        yield return new WaitForSeconds(waitTime);
        playerController.acceleration = curAcc;
    }
}
