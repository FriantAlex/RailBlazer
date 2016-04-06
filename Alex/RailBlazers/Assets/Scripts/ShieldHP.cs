using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldHP : MonoBehaviour {

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
	}

	public void TakeDamage(int damage)
	{
		hp.value -= damage;
		if(hp.value == 0)
		{
			Debug.Log("Game Over");
		}
	}
}
