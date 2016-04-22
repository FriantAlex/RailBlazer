using UnityEngine;
using System.Collections;

public class BulletLife : MonoBehaviour {

	public float secs;
	public GameObject poof;
	
	void Start(){

		StartCoroutine (B(secs));

	}

	IEnumerator B(float seconds){

		yield return new WaitForSeconds(seconds);
		Instantiate (poof, transform.position, transform.rotation);
			Destroy(gameObject);
	}
}
