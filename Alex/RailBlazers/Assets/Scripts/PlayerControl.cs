using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public Slider hp;


	private AudioSource mySource;
	private float timer;

    public AudioClip[] signClips;
    public AudioClip[] damageClips;
    public AudioClip[] puzzleClips;



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
            mySource.PlayOneShot(damageClips[(int)Random.Range(0, damageClips.Length)], 5f);
            TakeDamage(1);

        }

        if(col.gameObject.tag == "Signs")
        {

            Debug.Log("Sign Plays");
            mySource.PlayOneShot(signClips[(int)Random.Range(0, signClips.Length)], 5f);

        }
        if(col.gameObject.tag == "AudioPuzzle")
        {
            mySource.PlayOneShot(puzzleClips[(int)Random.Range(0, puzzleClips.Length)], 5f);

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
