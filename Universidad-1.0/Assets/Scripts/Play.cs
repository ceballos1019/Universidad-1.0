using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {
	public AudioSource audioSourcebutton;

	void Start(){
		audioSourcebutton.volume = PlayerPrefs.GetFloat("SliderVolumeLevel", audioSourcebutton.volume);
	}
	public void Playing() {

			audioSourcebutton.Play ();
			Invoke ("WaitAudio",audioSourcebutton.clip.length);




	}
	public void WaitAudio() {
		
		SceneManager.LoadScene ("Game");
	}

}
