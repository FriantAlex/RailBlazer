﻿using UnityEngine;
using System.Collections;

public class MageController : MonoBehaviour
{
    public bool lookAt;
    public bool isFiring;
	public float dist;
    public Transform target;
	public float sightRange;

	private Quaternion startingRot;
    private LineRenderer line;
	private BouncingLaser laser;
    // Use this for initialization
    void Start()
    {
		startingRot = transform.rotation;
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		line = this.gameObject.GetComponent<LineRenderer> ();
		laser = this.gameObject.GetComponent<BouncingLaser> ();

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
				line.enabled = true;
				laser.enabled = true;
            }
			if(!isFiring){
				line.enabled = false;
				laser.enabled = false;
			}
        }
    }
}