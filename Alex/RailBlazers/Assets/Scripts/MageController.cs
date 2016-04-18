using UnityEngine;
using System.Collections;

public class MageController : MonoBehaviour
{
    public Animator anim;

    public bool lookAt;
    public bool isFiring;
	public float dist;
	public float firingRange;
    public Transform target;
	public float sightRange;
	public ParticleSystem chargeUp;
	public bool standing;

	private Quaternion startingRot;
    private LineRenderer line;
	private BouncingLaser laser;

    private AudioSource mySource;
    private bool sourcePlayed;
    // Use this for initialization

    public AudioClip[] mageAudio;
    public AudioClip[] deathAudio;

    void Awake()
    {
       anim = GetComponent<Animator>();
       mySource = GetComponent<AudioSource>();
    }

    void Start()
    {
		chargeUp.Stop ();
		chargeUp.Clear ();
		startingRot = transform.rotation;
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		line = this.gameObject.GetComponent<LineRenderer> ();
		laser = this.gameObject.GetComponent<BouncingLaser> ();
    }

    // Update is called once per frame
    void Update()
    {
		
        if (target != null)
        {
            dist = Vector3.Distance(target.position, transform.position);

            if (dist < sightRange){
				lookAt = true;
				chargeUp.Play ();
                PlayFireAnimation();
            }
            if (dist > sightRange){
				transform.rotation = Quaternion.Slerp(transform.rotation,startingRot,  5 * Time.deltaTime);
				lookAt = false;

			}
			if (lookAt && !standing)
            {
                Vector3 diff = target.position - transform.position;			
                float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }

			if (dist < firingRange)
				isFiring = true;

            if (isFiring)
            {
				line.enabled = true;
				laser.enabled = true;
				chargeUp.Stop ();
				chargeUp.Clear ();               
            }
			if(!isFiring)
            {
				line.enabled = false;
				laser.enabled = false;
			}
        }
    }

    void PlayFireAnimation()
    {
        if(anim != null)
        {
            anim.SetBool("Attacking", true);
            if (!sourcePlayed)
            {
                //mySource.Play();

                PlayRandomAudio();
                sourcePlayed = true;
            }
        }
    }

    void PlayDeath()
    {
        Debug.Log("death running ");
        if (anim != null)
        {
            PlayRandomDeathAudio();
            target = null;
            anim.SetBool("isDead", true);
            line.enabled = false;
            laser.enabled = false;
            chargeUp.Stop();
            chargeUp.Clear();
        }
    }

	void PlayIdle(){
		Debug.Log("Idle running ");
		if (anim != null)
		{
			anim.SetBool("Idle", true);
		}
	}

    void OnTriggerStay(Collider col)
    {
        Debug.Log("I got hit" + col.gameObject.name);
        if (col.gameObject.tag == "Shield")
        {
            Debug.Log("Shield hit me");
            if (!col.gameObject.transform.parent.GetComponent<ControllerInput>().returned)
            {
                Debug.Log("I got bashed bruh");
                
                PlayDeath();
            }
        }
    }

    void HitByLaser()
    {

        PlayDeath();
    }

    void PlayRandomAudio()
    {
        mySource.PlayOneShot(mageAudio[(int)Random.Range(0, mageAudio.Length)], 1f);
    }
   
    void PlayRandomDeathAudio()
    {
        mySource.PlayOneShot(deathAudio[(int)Random.Range(0, deathAudio.Length)], 1f);
    }
}