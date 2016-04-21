using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public BouncingLaser laser;
	public float hitTimmer;
    private Animator anim;

	public float counter = 0.0f;
    public float resetSpeed;
    public GateScript gate;

    public bool puzzleDone = false;

    public GameObject rock;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (laser != null) {
			
			if (laser.isHit) {
				//Debug.Log ("I am hit");
				counter += Time.deltaTime;

				if (counter > hitTimmer) {

                    puzzleDone = true;
                    anim.SetBool("puzzleSolved", puzzleDone);
                    //Destroy(rock);

                    Debug.Log("Puzzle done");

                    StartCoroutine("PuzzleDone");
                   
                    //Destroy (gameObject);
				}
			} else if(!laser.isHit) {
                float step = resetSpeed * Time.deltaTime;
				counter = Mathf.Lerp(counter, 0, step);
			}

		}

        
	}IEnumerator PuzzleDone(){

            yield return new WaitForSeconds(2f);

            GameController.s.Go();

        }
}
