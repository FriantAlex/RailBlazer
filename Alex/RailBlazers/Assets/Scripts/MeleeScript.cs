using UnityEngine;
using System.Collections;

public class MeleeScript : MonoBehaviour
{
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

    private Quaternion startingRot;
    void Start()
    {
        startingRot = transform.rotation;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
                }
                else if(attackTimer > attackDelay)
                {
                    Attack();
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
        //Play attack animation       
    }
}
