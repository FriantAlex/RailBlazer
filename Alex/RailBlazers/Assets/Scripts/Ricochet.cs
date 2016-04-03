using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {

	public LayerMask collsionMask;

	public float speed; //movement speed
	public float rotSpeed; //rotation speed

    private Rigidbody rb;

    bool stuck = false;
    float depth = 0.30F;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (!stuck)
        {
            //move object forward
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, speed * Time.deltaTime + .1f, collsionMask))
            {
                Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, rot);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (!stuck && other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            stuck = true;
            ArrowStick(other);          
        }
    }
    void ArrowStick(Collider col)
    {
        // move the arrow deep inside the enemy or whatever it sticks to
        // col.transform.Translate(depth * -Vector2.right);
        // Make the arrow a child of the thing it's stuck to
        GetComponent<SphereCollider>().enabled = false;
        transform.parent = col.transform;
        //Destroy the arrow's rigidbody2D and collider2D
        Destroy(rb);
        
    }
}
