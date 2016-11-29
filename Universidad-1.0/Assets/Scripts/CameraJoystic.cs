using UnityEngine;
using System.Collections;

public class CameraJoystic : MonoBehaviour {



	public VirtualJoystic cameraJoystic;
	public GameObject Regina; 
	public GameObject Remy; 
	public GameObject Stefani; 
	public GameObject Malcolm; 



	private float distanceX=26.0f;
	private float distanceY=223.0f;
	private float distanceZ=304.0f;
	private float currentX=7.0f;
	private float currentY = 4.0f;
	private float sensiviteX = 3.0f;
	private float sensiviteY = 1.0f;

	void Start(){



		// get the float value of SliderVolumeLevel if it has been saved with PlayerPrefs.SetFloat()
		// else use defult value of audioSource.volume
		Camera.main.GetComponent < AudioSource >().volume= PlayerPrefs.GetFloat ("SliderMusicVolume", Camera.main.GetComponent<AudioSource>().volume);
	}

	private void Update(){

		currentX += cameraJoystic.InputDirection.x * sensiviteX;
		currentY += cameraJoystic.InputDirection.y * sensiviteY;


	}
	private void LateUpdate(){
		Vector3 dir = new Vector3 (-distanceX,distanceY,distanceZ);
		Quaternion rotation = Quaternion.Euler (-currentY,currentX,0);
		if(Regina.activeSelf){
			transform.position = Regina.transform.position + rotation * dir;
			transform.LookAt (Regina.transform.position);
		}else if(Remy.activeSelf){
			transform.position = Remy.transform.position + rotation * dir;
			transform.LookAt (Remy.transform.position);
		}else if(Stefani.activeSelf){
			transform.position = Stefani.transform.position + rotation * dir;
			transform.LookAt (Stefani.transform.position);
		}else if(Malcolm.activeSelf){
			transform.position = Malcolm.transform.position + rotation * dir;
			transform.LookAt (Malcolm.transform.position);
		}
	}

} 

