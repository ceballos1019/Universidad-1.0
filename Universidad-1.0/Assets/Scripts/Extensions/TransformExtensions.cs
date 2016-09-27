using UnityEngine;
using System.Collections;

public static class TransformExtension{
	public static IEnumerator Move (this Transform t, Vector3 target, float duration)
	{
		Vector3 diffVector = (target - t.position);
		float diffLength = diffVector.magnitude;
		diffVector.Normalize ();
		float counter = 0;
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
		float counter = 0;
		while (counter < duration) {
			float nowAmount = (Time.deltaTime * diffLength)/duration;
			t.localScale += diffVector * nowAmount;
			counter+=Time.deltaTime;
			yield return null;
		}
		t.localScale = target;
	}
}
