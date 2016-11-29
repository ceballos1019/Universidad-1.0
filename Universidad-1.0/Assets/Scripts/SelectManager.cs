using UnityEngine;
using System.Collections;

public class SelectManager : MonoBehaviour {
	static Animator anim;
	public float rotationSpeed=100.0F; 
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;


		rotation *= Time.deltaTime;


		transform.Rotate (0,rotation,0);

		if(Input.GetButtonDown("Jump")){
			anim.SetTrigger ("isJumping");
		}
		if(Input.GetButtonDown("Fire2")){
			anim.SetTrigger ("isDancing");

		}
		anim.SetBool ("isIdle",true);
	}
}
