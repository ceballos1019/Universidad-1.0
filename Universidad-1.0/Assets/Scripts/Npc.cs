using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Npc : MonoBehaviour {

	public GUIStyle myGUIStyle;
	public string[] answers;
	public string[] Questions;
	private bool displayDialogue=false;
	public GameObject character1;
	public GameObject character2;
	public GameObject character3;
	public GameObject character4;
	public GUIStyle customButton;

	void OnTriggerEnter(){
		displayDialogue = true;
	}

	void OnTriggerExit(){
		displayDialogue = false;
	}

	void OnGUI(){
		customButton = new GUIStyle ("button");
		customButton.fontSize = 50;
		if (displayDialogue) {

			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), Questions [0],myGUIStyle);
			GUI.Label (new Rect (50, 300, Screen.width, Screen.height), Questions [1],myGUIStyle);

			if (GUI.Button (new Rect (150, 450, 150, 150), answers [0],customButton)) {
				Invoke ("WaitToEnd", 1f);

			} if(GUI.Button (new Rect (380, 450, 150, 150), answers [1],customButton)) {
				displayDialogue = false;

			}
		}
		/*	Invoke ("WaitToEnd", 5f);
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.Label (Questions [1]);

			isClicked = GUILayout.Button (answers [0]);
	
			if (isClicked) {
				Debug.Log ("aquii");
				Invoke ("WaitToEnd", 5f);
	
			}
			GUILayout.Button (answers [1]);
			GUILayout.EndArea ();
		}

		*/	



	}
	void WaitToEnd(){

		character1.transform.position=new Vector3(-120,-10,600);
		character2.transform.position=new Vector3(-120,-10,600);
		character3.transform.position=new Vector3(-120,-10,600);
		character4.transform.position=new Vector3(-120,-10,600);

	}
}