using UnityEngine;
using System.Collections;

public class CameraController1 : MonoBehaviour {

	public Transform player;
	public float dist = 15f;
	public float currentX;
	public float currentY;
	public float rotX;
	public float rotY;



	private Camera cam;



	// Use this for initialization
	void Awake () {
	
		cam = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		Vector3 dir = new Vector3 (0, 0, -dist);
		Quaternion pos = Quaternion.Euler (currentX, currentY, 0);
		Quaternion rot = Quaternion.Euler (rotX, rotY, 0);
		transform.rotation = rot;
		transform.position = player.position  + pos * dir;

	}
}
