using UnityEngine;
using System.Collections;

public class SpeedController : MonoBehaviour {

		public float duration;
		public float offSet;


		private GameObject player;
		private GameObject cameraObj;

		void Awake()
		{
			player = GameObject.FindGameObjectWithTag("Player");
			cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
		}
		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void OnTriggerEnter(Collider col)
		{
		if(col.gameObject.tag == "Player")
			{
				Debug.Log("Player speed adjusted");
			player.GetComponent<SplineWalker>().duration = duration;

			}

		if(col.gameObject.tag == "MainCamera")
		{
			Debug.Log("Camera speed adjusted");
			cameraObj.GetComponent<SplineWalker>().duration = duration - offSet;

		}
		}
	}