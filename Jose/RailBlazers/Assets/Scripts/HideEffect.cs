using UnityEngine;
using System.Collections;

public class HideEffect : MonoBehaviour {

	public float scrollSpeed = 0.5f;
	public bool u = false;
	public bool v = true;
	public Material mat;
	public float offSet;

	// Use this for initialization
	void Start () {
	
		mat = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {

		offSet = Time.time * scrollSpeed % 1;

		if (u & v) {
			mat.mainTextureOffset = new Vector2 (offSet, offSet);
		} else if (u) {
			mat.mainTextureOffset = new Vector2 (0, offSet);
		}else if (v) {
		mat.mainTextureOffset = new Vector2 (0, offSet);
	}
}
}