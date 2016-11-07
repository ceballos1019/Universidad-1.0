using UnityEngine;
using System.Collections;

public static class TransformExtension{
	
	public static IEnumerator Move (this Transform t, Vector3 target, float duration)	
	{		
		Vector3 diffVector = (target - t.position);
		float diffLength = diffVector.magnitude;
		diffVector.Normalize ();
		float counter = 0f;
		while (counter < duration) {
			float nowAmount = (Time.deltaTime * diffLength)/duration;
			t.position += diffVector * nowAmount;
			counter+=Time.deltaTime;
			yield return null;
		}

		t.position = target;
	}

	public static IEnumerator Scale(this Transform t, Vector3 target, float duration)
	{
		Vector3 diffVector = (target - t.localScale);
		float diffLength = diffVector.magnitude;
		diffVector.Normalize ();
		float counter = 0f;
		while (counter < duration) {
			float nowAmount = (Time.deltaTime * diffLength)/duration;
			t.localScale += diffVector * nowAmount;
			counter+=Time.deltaTime;
			yield return null;
		}
		t.localScale = target; 
	}

	public static IEnumerator Spin(this Transform t, float speed, float duration)
	{
		float lastSpeed = speed/2;
		float counter = 0f;
		while (counter < duration) {
			if (counter > (duration / 2)) {
				t.localRotation *= Quaternion.Euler (0, 0, lastSpeed);	
			} else {
				t.localRotation *= Quaternion.Euler(0, 0, speed);
			}
			counter += Time.deltaTime;
			yield return null;
		}
		t.localRotation = Quaternion.Euler(0,0,0);
	}

}
