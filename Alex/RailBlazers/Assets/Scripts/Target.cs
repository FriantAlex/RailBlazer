using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public BouncingLaser laser;
	public float hitTimmer;

	public float counter = 0.0f;
    public float resetSpeed;

    public GameObject rock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (laser != null) {
			
			if (laser.isHit) {
				//Debug.Log ("I am hit");
				counter += Time.deltaTime;

				if (counter > hitTimmer) {

					GameController.s.Go ();
                    Destroy(rock);
					Destroy (gameObject);
				}
			} else if(!laser.isHit) {
                float step = resetSpeed * Time.deltaTime;
				counter =Mathf.Lerp(counter, 0, step);
			}

		}
	}
}
