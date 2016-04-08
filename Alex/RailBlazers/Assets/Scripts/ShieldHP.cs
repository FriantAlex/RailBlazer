using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldHP : MonoBehaviour {

    public static ShieldHP s;

	public Slider hp;
	public float regenWait;
	public int maxHP;
	public GameObject shield;

    public bool inCombat;
    private bool regening;
    private float regenTime;
    
    

	void Start()
    {
		hp.maxValue = maxHP;
	}

	void Update()
    {
		if (hp.value <= 0)
        {
			StartCoroutine (Regen ());
            shield.GetComponent<MeshRenderer>().enabled = false;
            shield.GetComponent<MeshCollider>().enabled = false;
        }

        if (regenTime > 0)
        {
            regenTime -= Time.deltaTime;
            inCombat = true;
        }
        else
        {
            inCombat = false;
        }


        if(hp.value > 0 && !inCombat && !regening)
        {
            RegenNow();
        }
	}

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet" && hp.value > 0)
        {
            TakeDamage(1);
            regenTime = regenWait;
        }
	}
		
	public void TakeDamage(int damage)
	{
		hp.value -= damage;
	}

	private IEnumerator Regen()
    {
        regening = true;
		yield return new WaitForSeconds (regenWait * 2);
        hp.value = 10;
        shield.GetComponent<MeshRenderer>().enabled = true;
        shield.GetComponent<MeshCollider>().enabled = true;
        regening = false;
    }

    void RegenNow()
    {
        hp.value = 10;
        shield.GetComponent<MeshRenderer>().enabled = true;
        shield.GetComponent<MeshCollider>().enabled = true;
    }
}
