using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class NpcDialogue3 : MonoBehaviour {
	public GUIStyle myGUIStyle;
	public GUIStyle customizeButton;

	public string[] answers;
	public string[] Questions;
	private bool displayDialogue=false;
	public GameObject character1;


	void OnTriggerEnter(){
		displayDialogue = true;
	}

	void OnTriggerExit(){
		displayDialogue = false;
	}

	void OnGUI(){
		customizeButton = new GUIStyle ("button");
		customizeButton.fontSize = 50;

		if (displayDialogue) {

			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), Questions [0],myGUIStyle);
		

			if (GUI.Button (new Rect (50, 300, 500, 200), answers [0],customizeButton)) {
				Invoke ("NextScene", 1f);
			} if(GUI.Button (new Rect (50, 520, 500, 200), answers [1],customizeButton)) {
				displayDialogue = false;

			}
		}
	



	}
	void NextScene(){
		SceneManager.LoadScene ("FeedGame");
	}
}


