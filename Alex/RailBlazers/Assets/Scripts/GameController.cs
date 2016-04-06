using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static GameController s;
	public bool isStopped;
	public GameObject player;
	public GameObject cameraObj;
	public bool isPaused;

	void Awake(){
		if (s == null) {
			s = this;
		} else {
			Destroy (gameObject);
		}

		player = GameObject.FindGameObjectWithTag ("Player");
		cameraObj = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(player != null){
			
		}
	
	}


	public void Go(){
		player.GetComponent<SplineWalker> ().enabled = true;
		cameraObj.GetComponent<SplineWalker> ().enabled = true;
        player.GetComponent<AudioSource>().enabled = true;
	}

	public void Stop(){
		player.GetComponent<SplineWalker> ().enabled = false;
		cameraObj.GetComponent<SplineWalker> ().enabled = false;
        player.GetComponent<AudioSource>().enabled = false;

    }


}
