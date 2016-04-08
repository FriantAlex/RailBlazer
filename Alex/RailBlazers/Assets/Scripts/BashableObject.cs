using UnityEngine;
using System.Collections;

public class BashableObject : MonoBehaviour {

    public int scoreValue;
	private ControllerInput isBashing;
    private AudioSource mySource;

	void Awake()
    {
		isBashing = GameObject.FindGameObjectWithTag("MainShield").GetComponent<ControllerInput>();
        mySource = GetComponent<AudioSource>();
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
            if(mySource != null)
            {
                mySource.Play();
                transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
                Destroy(this.gameObject, 1f);
            }
            else
            {
                Destroy(this.gameObject);
            }        
        }		
	}
}
