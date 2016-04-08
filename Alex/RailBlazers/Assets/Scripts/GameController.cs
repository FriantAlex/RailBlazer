using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static GameController s;
	public bool isStopped;
	public GameObject player;
	public GameObject cameraObj;
	public bool isPaused;
	public int score;
	public GUIText scoreText;

	void Awake(){
		if (s == null) {
			s = this;
		} else {
			Destroy (gameObject);
		}

		player = GameObject.FindGameObjectWithTag ("Player");
		cameraObj = GameObject.FindGameObjectWithTag ("MainCamera");
	}
		
	void Start(){

		score = 0;
		UpdateScore();
	}


	// Update is called once per frame
	void Update () {

		if (player != null) {
			
			if (Input.GetKeyDown ("escape")) {
				Application.Quit ();
			}
	
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

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore()
	{
		scoreText.text = "Gems: " + score;
	}

}
