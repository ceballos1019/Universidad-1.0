using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	public AudioSource audioSourcebutton;
  public void Quit() {
		
			audioSourcebutton.Play ();
			Application.Quit ();
		


}
}
