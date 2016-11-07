using UnityEngine;
using System.Collections;

public static class SpriteExtension{

	public static IEnumerator ChangeColor(this SpriteRenderer sp, Vector4 target, float duration){
		Vector4 vectorAux = sp.color;
		Vector4 diffVector = (target - vectorAux);
		float diffLength = diffVector.magnitude;
		diffVector.Normalize ();
		Color colorAux;
		float counter = 0f;
		while (counter < duration) {
			float nowAmount = (Time.deltaTime * diffLength)/duration;
			colorAux = diffVector * nowAmount;
			sp.color += colorAux;
			counter+=Time.deltaTime;
			yield return null;
		}
		sp.color= target; 
	}
}
