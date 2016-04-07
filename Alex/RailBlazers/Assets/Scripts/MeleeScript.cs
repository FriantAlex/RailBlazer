﻿using UnityEngine;
using System.Collections;

public class MeleeScript : MonoBehaviour
{
	public Animator anim;
    public int health;
    public int speed;
    public float attackDelay;
    public float attackTimer = 0.0f;
    public float chargeRange;
    public float sightRange;

    public bool lookAt;
    public bool isAttacking;

    public float dist;
    public float minDist;
    public Transform target;
	public GameObject dethAnim;

    private Quaternion startingRot;
    private GameObject basher;

    void Start()
    {
		anim = GetComponent<Animator>();
        startingRot = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        basher = GameObject.Find("LeftStickHome");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);
        if (target != null && !isAttacking)
        {
            if (dist < sightRange)
            {
                lookAt = true;
                if(dist < chargeRange && dist > minDist)
                {
                    Charging();
					PlayWalk ();
                }
                else if(attackTimer > attackDelay)
                {
                    Attack();
					//PlayFireAnimation ();
                    attackTimer = 0;
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
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }
            attackTimer += Time.deltaTime;
        }
    }

    void Charging()
    {
        transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;       
    }

    void Attack()
    {
        Debug.Log("Attack");
		PlayFireAnimation();  
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("I got hit");
        if(col.gameObject.tag == "Shield")
        {
            Debug.Log("Shield hit me");
            if (col.gameObject.transform.parent.GetComponent<ControllerInput>().bashing)
            {
                Debug.Log("I got bashed bruh");
				Instantiate (dethAnim, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }


	void HitByLaser(){
		Instantiate (dethAnim, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}

	void PlayFireAnimation()
	{
		if(anim != null)
		{
			anim.SetBool("Attacking", true);
		}
	}

	void PlayWalk()
	{
		Debug.Log("death running ");
		if (anim != null)
		{
			anim.SetBool("Walking", true);
		}
	}

	void PlayIdle(){
		Debug.Log("Idle running ");
		if (anim != null)
		{
			anim.SetBool("Idle", true);
		}

	}
}
