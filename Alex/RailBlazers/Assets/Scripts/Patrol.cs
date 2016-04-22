using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

	public Transform[] waypoints; //list of point
    [Header ("Movement")]
	public float maxPatrolSpeed = 6; //max speed
    public float speed = 0.0f; // keep constant
    public float acceleration; //how fast top speed is reached

    [Header ("Time")]
    public float pauseDuration; // stop at point
    public float curTime;
    public int dampingLook = 4; // rotation speed

    [Header("Track")]
    public bool patrolLoop = true; // loops partol	
	public int currentWayPoint = 0;//index of waypoint currently moving towards
    public bool changing;
	public bool isStopped;


	// Update is called once per frame
	void Update () {
		

		if (maxPatrolSpeed == 0) {
			isStopped = true;
		}

		if (currentWayPoint < waypoints.Length)
        {
			PatrolCycle ();
		}
        else
        {
			if (patrolLoop)
            {
				currentWayPoint = 0;
			}
		}
	}

	void PatrolCycle()
    {
		
		speed = Mathf.Lerp (speed, maxPatrolSpeed, acceleration * Time.deltaTime);

		Vector3 nextWayPoint = waypoints[currentWayPoint].position;

		Vector3 moveDirection = nextWayPoint - transform.position;
		if(moveDirection.magnitude < .1)
        {
			Debug.Log("Cart is about to change waypoint");          
			if(curTime == 0)
            {
                curTime = Time.time;              
            }

            if ((Time.time - curTime) >= pauseDuration)
            {
                Debug.Log("Increasing Waypoint");
                currentWayPoint++;
                curTime = 0;
            }		
		}
        else
        {
			Debug.Log("Next waypoint in " + moveDirection.magnitude);


			float angle = Mathf.Atan2(moveDirection.y,moveDirection.x)*Mathf.Rad2Deg -  90;

			Vector3 lookAngle = Vector3.zero;

			lookAngle.z = Mathf.LerpAngle(transform.eulerAngles.z,angle, speed * Time.deltaTime);
			transform.eulerAngles = lookAngle;

			transform.position = Vector3.MoveTowards (transform.position, nextWayPoint, speed * Time.deltaTime);
		}
	}

	//draws partol path
	void OnDrawGizmos()
    {
		if (waypoints == null)
			return;

		if (waypoints != null)
        {
			for (int i = 1; i < waypoints.Length; i++)
            {
				Gizmos.color = Color.green;
				Gizmos.DrawLine (waypoints [i].position, waypoints [i - 1].position);
			}
		}
	}
}
