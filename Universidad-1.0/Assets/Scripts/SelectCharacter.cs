using UnityEngine;
using System.Collections;

public class SelectCharacter : MonoBehaviour {
	static public bool once_call;
	public GameObject select;
	void Awake(){
		Debug.Log (once_call);
		if (select!= null) {    
			
			if (!once_call) {
				DontDestroyOnLoad (select);
				once_call = true;
			} else {
				
				Destroy (select);

			}
		}
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
