﻿using UnityEngine;
using System.Collections;

public class TempArcher : MonoBehaviour {

	public GameObject projectilePrefab;
	public float shotDelay;
   public float shotTimmer = 0.0f;
    public Transform shotSpawn;
    public bool lookAt;
    public bool isFiring;
	public float dist;
	public float sightRange;
	public Transform target;

	private Quaternion startingRot;
	// Use this for initialization
	void Start () {
	
		startingRot = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
				transform.rotation = Quaternion.Slerp(transform.rotation,startingRot,  5 * Time.deltaTime);
				lookAt = false;
				
			}
			if (lookAt)
			{
				Vector3 diff = target.position - transform.position;
				
				float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				
				transform.rotation = Quaternion.Euler(0, 0, rotZ);
				
			}
            if (isFiring)
            {
                if (shotTimmer > shotDelay)
                {
                    FireProjectile();
                    shotTimmer = 0.0f;
                }
            }
            shotTimmer += Time.deltaTime;
        }
	}

    void FireProjectile()
    {

        var clone = Instantiate(projectilePrefab, shotSpawn.position, transform.rotation) as GameObject;
    }
}
