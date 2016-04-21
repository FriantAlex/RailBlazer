using UnityEngine;
using System.Collections;

public class CameraController1 : MonoBehaviour {

	public Transform player;
	public float dist = 15f;
	public float currentX;
	public float currentY;
	public float rotX;
	public float rotY;
	public float rotSpeed;
	//public bool iso;
	public bool topDown;



	private Camera cam;



	// Use this for initialization
	void Awake () {
	
		cam = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void LateUpdate () {


		if (topDown) {
			rotX = Mathf.Lerp (rotX, 360, rotSpeed * Time.deltaTime);
			currentX = Mathf.Lerp (currentX, 0, rotSpeed * Time.deltaTime);
			dist = Mathf.Lerp (dist, 30, rotSpeed * Time.deltaTime);

		}else{
			rotX = Mathf.Lerp (rotX, 300, rotSpeed * Time.deltaTime);
			currentX = Mathf.Lerp (currentX, 40, rotSpeed * Time.deltaTime);
			dist = Mathf.Lerp (dist, 15, rotSpeed * Time.deltaTime);
		}

		Vector3 dir = new Vector3 (0, 0, dist);
		Quaternion pos = Quaternion.Euler (currentX, currentY, 0);
		Quaternion rot = Quaternion.Euler (rotX, rotY, 0);
		transform.rotation = rot;
		transform.position = player.position  + pos * dir;

	}

	void TopDown(){
		dist = 15;
		currentY = 0;
		currentX = 0;
		rotX = 0;
		rotY = 180;

		Vector3 dir = new Vector3 (0, 0, dist);
		Quaternion pos = Quaternion.Euler (currentX, currentY, 0);
		Quaternion rot = Quaternion.Euler (rotX, rotY, 0);
		transform.rotation = rot;
		transform.position = player.position  + pos * dir;


	}

	void Iso(){
		dist = 15;
		currentY = 0;
		currentX = 0;
		rotX = 330;
		rotY = 180;

		Vector3 dir = new Vector3 (0, 0, dist);
		Quaternion pos = Quaternion.Euler (currentX, currentY, 0);
		Quaternion rot = Quaternion.Euler (rotX, rotY, 0);
		transform.rotation = rot;
		transform.position = player.position  + pos * dir;

	}
		

		
}
