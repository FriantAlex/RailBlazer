using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public Slider hp;



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Player was hit");
            TakeDamage(1);
        }
        if (col.gameObject.tag == "Melee")
        {
            TakeDamage(2);
        }

		if (hp.value == 0) {
			Regen ();
		}
    }

    public void TakeDamage(int damage)
    {
        hp.value -= damage;
        if(hp.value == 0)
        {
			gameObject.SetActive (false);
        }
    }

	public void Regen(){

		hp.value += Time.deltaTime;
		if (hp.value == 10) {
			gameObject.SetActive (true);
		}
	}
}
