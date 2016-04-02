using UnityEngine;
using System.Collections;

public class MeshDeformer : MonoBehaviour {

	public BezierSpline spline;
	public int frequency;
	public bool lookForward;
	public GameObject[] items;


	// Use this for initialization
	private void Awake () {
	
		if (frequency < 0 || items == null || items.Length == 0) {
			return;
		}

		float stepSize = frequency * items.Length;
		if (spline.Loop || stepSize == 1) {
			stepSize = 1f / stepSize;
		} else {
			stepSize = 1f / (stepSize - 1);
		}
		for (int p = 0, f = 0; f < frequency; f++) {
			for(int i = 0; i < items.Length; i++,p++){

				GameObject item = Instantiate (items [i]) as GameObject;
				Vector3 pos = spline.GetPoint (p * stepSize);
				item.transform.localPosition = pos;
				if (lookForward) {
					item.transform.LookAt (pos + spline.GetDirection (p * stepSize),new Vector3(0,1,1));
				}
				item.transform.parent = transform;
		}

	}

 }

}
