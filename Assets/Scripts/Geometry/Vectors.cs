using UnityEngine;
using System.Collections;

public class Vectors : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public static Vector2 ShortenVector(Vector2 vector, float length){
		float x = (length * vector.x) / vector.magnitude;
		float y = (length * vector.y) / vector.magnitude;
		return new Vector2 (x, y);
	}
	
	public static float AngleBetweenVectors(Vector3 vector1, Vector3 vector2){
		float scalar = vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
		float cosAlpha = scalar / (vector1.magnitude * vector2.magnitude);
		return Mathf.Acos (cosAlpha);
	}
	
	public static float AngleBetweenVectors(Vector2 vector1, Vector2 vector2){
		float scalar = vector1.x * vector2.x + vector1.y * vector2.y;
		float cosAlpha = scalar / (vector1.magnitude * vector2.magnitude);
		return Mathf.Acos (cosAlpha);
	}
}
