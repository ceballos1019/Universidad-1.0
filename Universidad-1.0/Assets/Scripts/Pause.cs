using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public GameObject pause;

	public void TogglePause(){
	
		pause.SetActive (!pause.activeSelf);
	}

	public void OnMouseDown(){

		Application.Quit ();
	
	}
}
