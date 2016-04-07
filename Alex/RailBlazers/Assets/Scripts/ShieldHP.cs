using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldHP : MonoBehaviour {
	public static ShieldHP s;

	public Slider hp;
	public float regenSpeed;
	public int maxHP;
	public GameObject shield;

	void Start(){

		hp.maxValue = maxHP;
	}


	void Update(){

		if (hp.value == 0) {

			StartCoroutine (Regen ());

			shield.SetActive (false);
		}

	}

	void OnTriggerEnter(Collider other){



	}
		
	public void TakeDamage(int damage)
	{
		hp.value -= damage;


	}

	private IEnumerator Regen(){
		yield return new WaitForSeconds (regenSpeed);
		hp.value = 10;
		shield.SetActive (true);

	}
		
}
