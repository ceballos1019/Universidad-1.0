using UnityEngine;
using System.Collections;

public class NpcDialogue2 : MonoBehaviour {
	public GUIStyle myGUIStyle;




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
		myGUIStyle.fontSize = 50;
			if (displayDialogue) {

			GUI.Label (new Rect (0, 150, Screen.width, Screen.height), Questions [0],myGUIStyle);
			GUI.Label (new Rect (0, 450, Screen.width, Screen.height), Questions [1],myGUIStyle);


			}
	

	
	}
	}