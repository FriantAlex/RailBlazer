using UnityEngine;
using System.Collections;

public class BashableObject : MonoBehaviour {
	public int scoreValue;

	private ControllerInput isBashing;

	void Awake(){

		isBashing = GameObject.FindGameObjectWithTag("MainShield").GetComponent<ControllerInput>();
	}

	void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.tag == "Shield" && isBashing.bashing && this.gameObject.tag == "ObjectInPath") 
		{
            Debug.Log("Shield " + col.gameObject.name + " destroyed me");
			GameController.s.Go ();
			GameController.s.AddScore(scoreValue);
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "Shield" && this.gameObject.tag == "Breakable")
        {
			GameController.s.AddScore(scoreValue);
            Destroy(this.gameObject);
        }

            Debug.Log("I got hit");
		if(col.gameObject.tag == "Shield")
		{
			Debug.Log("Shield hit me");
			if (col.gameObject.transform.parent.GetComponent<ControllerInput>().bashing)
			{
				Debug.Log("I got bashed bruh");
				GameController.s.AddScore(scoreValue);
				Destroy(this.gameObject);
			}
		}

	}
}
