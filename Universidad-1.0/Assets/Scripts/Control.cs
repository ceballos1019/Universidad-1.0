using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	public GameObject character1;
	public GameObject character2;
	public GameObject character3;
	public GameObject character4;

	public GameObject Select1;
	public GameObject Select2;
	public GameObject Select3;
	public GameObject Select4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		Select1 = GameObject.FindGameObjectWithTag ("Regina");
		Select2 = GameObject.FindGameObjectWithTag ("Remy");
		Select3 = GameObject.FindGameObjectWithTag ("Stefani");
		Select4 = GameObject.FindGameObjectWithTag ("Malcolm");
		if (Select1 == true) {
			character1.SetActive (true);
		}
		if (Select2 == true) {
			character2.SetActive (true);
		}
		if (Select3 == true) {
			character3.SetActive (true);
		}
		if (Select4 == true) {
			character4.SetActive (true);
		}
	}
}
