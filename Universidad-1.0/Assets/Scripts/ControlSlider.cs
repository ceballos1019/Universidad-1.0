using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlSlider : MonoBehaviour {

	private float valueTarget;
	private float valueIncrement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Mathf.Abs (valueTarget - valueIncrement) > 0.01) {
			valueIncrement = Mathf.Lerp (valueIncrement, valueTarget, 0.1f);
			GetComponent<Slider> ().value = valueIncrement;
		} else if (valueTarget != valueIncrement) {
			valueIncrement = valueTarget;
			GetComponent<Slider> ().value = valueIncrement;
		}

	}

	public void CambiarValor(float valor){
		valueTarget = valor;
	}
	public void SumarValor(float valor){
		valueTarget += valor;
	}
}
