using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {

    public bool puzzleDone = false;
    private Animator anim;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
	
        if(puzzleDone == true)
        {
            anim.SetBool("GateActive", true);

        }

	}
}
