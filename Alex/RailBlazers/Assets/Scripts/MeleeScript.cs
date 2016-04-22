using UnityEngine;
using System.Collections;

public class MeleeScript : MonoBehaviour
{
	public Animator anim;
    public int health;
    public int speed;
    public float attackTimer;
    public float attackDelay;
    public float chargeRange;
    public float sightRange;
	public GameObject blood;

    public bool lookAt;
    public bool isAttacking;

    public float dist;
    public float minDist;
    public Transform target;
	public GameObject dethAnim;

    private Quaternion startingRot;
    private GameObject basher;

    //audio
    public AudioClip[] meleeAudio;
    public AudioSource audioSources;

    void Start()
    {
		anim = GetComponent<Animator>();
        startingRot = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Eid").GetComponent<Transform>();
        basher = GameObject.Find("LeftStickHome");
        audioSources = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);
        if (target != null)
        {
            if (dist < sightRange)
            {
                lookAt = true;
                if(dist < chargeRange && dist > minDist)
                {
                    Charging();
					PlayWalk ();
                    attackTimer = 0;
                }
                else if(dist <= minDist)
                {
                    PlayFireAnimation();
                    attackTimer += Time.deltaTime;
					//PlayFireAnimation ();
                }
            }
            if (dist > sightRange)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, startingRot, 5 * Time.deltaTime);
                lookAt = false;
            }
            if (lookAt)
            {
                Vector3 diff = target.position - transform.position;
                float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                //Vector3 newRot = Vector3.zero;
                //newRot.z = Mathf.Lerp(transform.eulerAngles.z, -rotZ, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0,0,rotZ);

                //PlayRandom();
            }
        }

        if(attackTimer > attackDelay)
        {
            target.transform.parent.GetComponent<PlayerControl>().TakeDamage(1);
            attackTimer = 0;
        }
    }

    void Charging()
    {
        if(!isAttacking)
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;       
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("I got hit");
        if(col.gameObject.tag == "Shield")
        {
            Debug.Log("Shield hit me");
            if (!col.gameObject.transform.parent.GetComponent<ControllerInput>().returned)
            {
                Debug.Log("I got bashed bruh");
				Instantiate (blood, transform.position, transform.rotation);
                Instantiate(dethAnim, new Vector3(transform.position.x, transform.position.y, transform.position.z - .8f), Quaternion.Euler(new Vector3(0, 90, 0)));
                GameController.s.AddScore(5);
                Destroy(this.gameObject);
            }
        }

        if(col.gameObject.tag == "Bullet")
        {
			Instantiate (blood, transform.position, transform.rotation);
            Instantiate(dethAnim, new Vector3(transform.position.x, transform.position.y, transform.position.z - .8f), Quaternion.Euler(new Vector3(0, 90, 0)));
            Destroy(this.gameObject);
        }
    }




	void HitByLaser()
    {
		Instantiate (blood, transform.position, transform.rotation);
        Instantiate(dethAnim, new Vector3(transform.position.x, transform.position.y, transform.position.z - .8f), Quaternion.Euler(new Vector3(0, 90, 0)));
        Destroy(this.gameObject);
	}

	void PlayFireAnimation()
	{
		if(anim != null)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Attacking", true);
            isAttacking = true;
		}
	}

    void ResetAttack()
    {
        isAttacking = false;
        PlayWalk();
    }

	void PlayWalk()
	{
		if (anim != null)
		{
            isAttacking = false;
            anim.SetBool("Attacking", false);
            anim.SetBool("Walking", true);
		}
	}

	void PlayIdle(){
		if (anim != null)
		{
			anim.SetBool("Idle", true);
		}
	}

    void PlayRandom()
    {

        audioSources.PlayOneShot(meleeAudio[(int)Random.Range(0, meleeAudio.Length)], 1f);

    }
}
