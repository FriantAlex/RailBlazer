using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class BouncingLaser : MonoBehaviour
{

	public int dist; //max travel distance
	public LineRenderer line; // the laser
	public string reflectTag;  // what the laser can bounce off of
	public string targetTag; // checks for hitting targets
	public string enemyTag; // checks for hitting enemis
	public int limit; // how many times it can bounce
	public bool isHit;

	private int verti =  1;  //laser segment handler leave as is
	private bool isActive;
	private Vector3 currentRot; //current rotation
	private Vector3 currentPos; //current position

	void Start(){
	}

	void Update(){

	//	line.enabled = Input.GetKey (KeyCode.Space);

		//if (Input.GetKey (KeyCode.Space) || Input.GetKeyUp (KeyCode.Space))
			DrawLaser ();
	}

	void DrawLaser(){

		int timesReflected = 1; //how many times the laser has bounced
		int vertexCounter = 1; //how many segments there are
		bool loopActive = true;
		Vector3 laserDir = transform.right; // direction of the next laser
		Vector3 lastLaserPos = transform.position; // orgin of the next laser
		RaycastHit hit;

		line.SetVertexCount (1);
		line.SetPosition (0, transform.position);


		while (loopActive) {
			 

			if (Physics.Raycast(lastLaserPos,laserDir,out hit, dist)&& hit.transform.gameObject.tag ==  reflectTag) {

				Debug.Log ("Boop");
				timesReflected++;
				vertexCounter += 3;
				line.SetVertexCount (vertexCounter);
				line.SetPosition (vertexCounter-3, Vector3.MoveTowards(hit.point, lastLaserPos, 0.01f));
				line.SetPosition(vertexCounter-2, hit.point);
				line.SetPosition(vertexCounter-1, hit.point);
				lastLaserPos = hit.point;
				laserDir = Vector3.Reflect(laserDir, hit.normal);

			} else {
				Debug.Log ("No Boop");
				timesReflected++;
				vertexCounter++;
				line.SetVertexCount (vertexCounter);
				line.SetPosition (vertexCounter - 1, lastLaserPos + (laserDir.normalized * dist));

				loopActive = false;
			}

			if (Physics.Raycast (lastLaserPos, laserDir, out hit, dist) && hit.transform.gameObject.tag == targetTag){
				isHit = true;
			}

			if (Physics.Raycast (lastLaserPos, laserDir, out hit, dist) && hit.transform.gameObject.tag == enemyTag)
				Destroy(this.gameObject);

			if (timesReflected > limit)
				loopActive = false;
		}
	}




}
