using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {


	public float secs;


	void Start(){

		StartCoroutine (B(secs));

	}

	IEnumerator B(float seconds){

		yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
	}
}
