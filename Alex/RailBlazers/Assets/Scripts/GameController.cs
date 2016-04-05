using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static GameController s;
	public bool isStopped;
	public GameObject player;

	void Awake(){
		if (s == null) {
			s = this;
		} else {
			Destroy (gameObject);
		}

		player = GameObject.FindGameObjectWithTag ("Player");
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
	}

	public void Stop(){
		player.GetComponent<SplineWalker> ().enabled = false;
	}
}
