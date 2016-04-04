using UnityEngine;
using System.Collections;

public class Stopper : MonoBehaviour {

	private GameObject player;
	private GameObject cameraObj;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Player Stopped");
            player.GetComponent<SplineWalker>().enabled = false;

        }

		if(col.gameObject.tag == "MainCamera")
		{
			Debug.Log("Camera Stopped");
			cameraObj.GetComponent<SplineWalker>().enabled = false;

		}
    }
}
