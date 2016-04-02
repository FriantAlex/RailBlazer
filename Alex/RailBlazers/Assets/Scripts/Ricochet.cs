using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {

	public LayerMask collsionMask;


	public Transform transform; // projectile transform
	public float speed; //movement speed
	public float rotSpeed; //rotation speed


	// Use this for initialization
	void Start () {
	
		transform = GetComponent<Transform> ();

	}
	
	// Update is called once per frame
	void Update () {

		//move object forward
		transform.Translate (Vector3.right * speed * Time.deltaTime);

		Ray ray = new Ray (transform.position, transform.right);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, speed * Time.deltaTime + .1f, collsionMask)) {

			Vector3 reflectDir = Vector3.Reflect (ray.direction, hit.normal);

			float rot = Mathf.Atan2(reflectDir.y,reflectDir.x) * Mathf.Rad2Deg;

			transform.eulerAngles = new Vector3 (0, 0, rot);

		}
	}


}
