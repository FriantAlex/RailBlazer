using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	 public void Starting(){

		Application.LoadLevel (1);
	}

	public void Credits(){

	}

	public void Quit(){
		Application.Quit ();
	}

	public void MainMenu(){
		Application.LoadLevel (0);

	}
}
