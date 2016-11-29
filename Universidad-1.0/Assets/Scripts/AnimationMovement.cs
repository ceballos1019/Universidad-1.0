using UnityEngine;
using System.Collections;

public class AnimationMovement : MonoBehaviour {
	static Animator anim;
	public float speed=100.0F;
	public float rotationSpeed=100.0F; 
	public VirtualJoystic joystic;


		

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

	}



	// Update is called once per frame
	void Update () {
		

		float translation = Input.GetAxis ("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
	
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		transform.Translate (0,0,translation);
		transform.Rotate (0,rotation,0);
	


		if(joystic.InputDirection!=Vector3.zero){
	
			translation = joystic.InputDirection.z*speed;

			translation *= Time.deltaTime;
			Debug.Log (translation);

			rotation = joystic.InputDirection.x *rotationSpeed;
			rotation *= Time.deltaTime;
			transform.Translate (0,0,translation);
			transform.Rotate (0,rotation,0);
		
		}

		if(Input.GetButtonDown("Jump")){
			anim.SetTrigger ("isJumping");
		}
		if(Input.GetButtonDown("Fire2")){
			anim.SetTrigger ("isDancing");
		
		}
		if (translation != 0 && translation>1|| translation<-1 ) {
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle",false);
		} else {
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle",true);
		}





}
}
