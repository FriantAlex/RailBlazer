using UnityEngine;
using System.Collections;

public class TutorialManage : MonoBehaviour {

    private GameObject player;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Player Stopped");
            player.GetComponent<SplineWalker>().enabled = false;

        }
    }
}
