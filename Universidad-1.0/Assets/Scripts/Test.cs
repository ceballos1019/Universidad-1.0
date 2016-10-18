using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	Slider mySlider;
	bool increasing;

	void Start(){
		mySlider = GetComponent<Slider> ();
		increasing = true;
	}

	public void Update(){
		if (increasing) {
			mySlider.value += 0.01f;
		} else {
			mySlider.value -= 0.01f;
		}

		if (mySlider.value == mySlider.maxValue || mySlider.value == mySlider.minValue) {
			increasing = !increasing;
		}
	}



}