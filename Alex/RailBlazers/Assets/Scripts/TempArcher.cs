using UnityEngine;
using System.Collections;

public class TempArcher : MonoBehaviour {

    public int health;
	public GameObject projectilePrefab;
	public float shotDelay;
    public float shotTimmer = 0.0f;
    public Transform shotSpawn;
    public bool lookAt;
    public float attackRange;
    public bool isFiring;
	public float dist;
	public float sightRange;
	public Transform target;
	public Animator anim;
	public GameObject deathAnim;
    
    //Audio
    private AudioSource mySource;
    public AudioClip shotClip;
    public AudioClip noticeClip;
	public bool noticed = false;

    private Quaternion startingRot;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		startingRot = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mySource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
		dist = Vector3.Distance(target.position,transform.position);
		
		if (target != null)
		{
			if(dist < sightRange){
				lookAt = true;
			}
			if(dist > sightRange){
				transform.rotation = Quaternion.Slerp(transform.rotation,startingRot,  2 * Time.deltaTime);
				lookAt = false;				
			}
            if(dist < attackRange)
            {
                isFiring = true;
            }
			if (lookAt)
			{
				if (!noticed) {
					noticed = true;
					mySource.Play ();
				}
				Vector3 diff = target.position - transform.position;				
				float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;	
				transform.rotation = Quaternion.Euler(0, 0, rotZ);
                	
			}
            if (isFiring)
            {
                if (shotTimmer > shotDelay)
                {
                    FireProjectile();
					PlayFireAnimation ();
                    shotTimmer = 0.0f;                  
                }
            }
            shotTimmer += Time.deltaTime;
        }
	}

    void FireProjectile()
    {
        var clone = Instantiate(projectilePrefab, shotSpawn.position, transform.rotation) as GameObject;
        //PlaySound(shotClip);
        mySource.PlayOneShot(shotClip);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            Debug.Log("Archer hit by projectile");
            PlayDeath();    
        }

        if(col.gameObject.tag == "Shield")
        {
            Debug.Log("Skeleton hit by shield");
			PlayDeath ();
        }
    }
    //Play sound once with arguement clip toPlay
    void PlaySound(AudioClip toPlay)
    {
        mySource.clip = toPlay;
        mySource.Play();
    }

    void PlayDeath()
    {
		Instantiate (deathAnim, transform.position, transform.rotation);
		Destroy (this.gameObject);
        GameController.s.AddScore(10);
    }

    void HitByLaser()
    {

        PlayDeath();
    }

	void PlayIdle(){
		if (anim != null)
		{
			anim.SetBool("Idle", true);
		}
	}

	void PlayFireAnimation()
	{
		if(anim != null)
		{
			anim.SetBool("Attacking", true);
		}
	}
}
