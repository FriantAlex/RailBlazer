using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {

    public GameObject Fader;

	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Ending Scene");
            Fader.SendMessage("SetBool");
        }
    }
}
