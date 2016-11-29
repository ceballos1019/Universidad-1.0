using UnityEngine;
using System.Collections;

public class PreviousCharacter : MonoBehaviour {
	public AudioSource audioSourcebutton;
	public GameObject character1;
	public GameObject character2;
	public GameObject character3;
	public GameObject character4;

	public GameObject Select1;
	public GameObject Select2;
	public GameObject Select3;
	public GameObject Select4;

	public void Previous () {
		
			audioSourcebutton.Play ();
			if (character1.activeSelf) {
				character1.SetActive (false);
				character2.SetActive (false);
				character3.SetActive (false);
				character4.SetActive (true);

				Select1.SetActive (false);
				Select2.SetActive (false);
				Select3.SetActive (false);
				Select4.SetActive (true);
			} else if (character2.activeSelf) {
				character1.SetActive (true);
				character2.SetActive (false);
				character3.SetActive (false);
				character4.SetActive (false);

				Select1.SetActive (true);
				Select2.SetActive (false);
				Select3.SetActive (false);
				Select4.SetActive (false);
			} else if (character3.activeSelf) {
				character1.SetActive (false);
				character2.SetActive (true);
				character3.SetActive (false);
				character4.SetActive (false);

				Select1.SetActive (false);
				Select2.SetActive (true);
				Select3.SetActive (false);
				Select4.SetActive (false);
			} else {
				character1.SetActive (false);
				character2.SetActive (false);
				character3.SetActive (true);
				character4.SetActive (false);

				Select1.SetActive (false);
				Select2.SetActive (false);
				Select3.SetActive (true);
				Select4.SetActive (false);
			}


	}
}
