using UnityEngine;
using System.Collections;

public class SplineSwap : MonoBehaviour {

	public SplineWalker walker;
	public BezierSpline curSpline;
	public BezierSpline newSpline;

	// Use this for initialization
	void Start () {
		
		curSpline = walker.spline;
	}


	void OnTriggerEnter(Collider other){

			walker.spline = newSpline;
		walker.mode = SplineWalkerMode.Once;
		
	}
}
