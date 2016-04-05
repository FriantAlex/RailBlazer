using UnityEngine;
using System.Collections;

public class BashableObject : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
        if(col.gameObject.tag == "Player")
        {
            GameController.s.Stop();
        }

		if (col.gameObject.tag == "Shield" && this.gameObject.tag == "ObjectInPath") 
		{
			GameController.s.Go ();
		}

		Debug.Log("I got hit");
		if(col.gameObject.tag == "Shield")
		{
			Debug.Log("Shield hit me");
			if (col.gameObject.transform.parent.GetComponent<ControllerInput>().bashing)
			{
				Debug.Log("I got bashed bruh");
				Destroy(this.gameObject);
			}
		}

	}
}
