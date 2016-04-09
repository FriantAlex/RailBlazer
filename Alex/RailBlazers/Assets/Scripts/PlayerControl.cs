using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public Slider hp;


	private AudioSource mySource;
	private float timer;

	void Awake(){

		mySource = GetComponent<AudioSource> ();

	}

	void Update(){

		if (timer > 0) {
			timer -= Time.deltaTime;
		}

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Player was hit");
			mySource.Play ();
            TakeDamage(1);

        }
    }

    public void TakeDamage(int damage)
    {
        hp.value -= damage;
        if(hp.value <= 0)
        {
			Debug.Log ("Game Over");
			GameController.s.Stop ();
			SceneFadeInOut.s.EndScene ();
        }
    }	

	void HitByLaser(){

		if (timer <= 0) {

			TakeDamage (1);
			timer = 1f;

		}

	}
}
