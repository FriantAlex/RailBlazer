using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SkillCD : MonoBehaviour {

	public List<Skill> skills;

	public void Start(){

		foreach (Skill s in skills) {

			s.currentCD = s.cd;
		}

	}

	public void FixedUpdate(){

		if (Input.GetButtonDown ("LeftBump")) {

			if (skills [0].currentCD >= skills [0].cd) {
				skills [0].currentCD = 0;
			}
		}

		if (Input.GetButtonDown ("RightBump")) {

			if (skills [1].currentCD >= skills [1].cd) {
				skills [1].currentCD = 0;
			}
		}
	}

	public void Update(){

		foreach(Skill s in skills){

			if (s.currentCD < s.cd) {
				s.currentCD += Time.deltaTime;
				s.skillIcon.fillAmount = s.currentCD / s.cd;
			}
		}

	}
}

[System.Serializable]
public class Skill{

	public float cd;
	public Image skillIcon;
	public float currentCD;

}
