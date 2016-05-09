using UnityEngine;
using System.Collections;

public class RenderPortalCamera : MonoBehaviour {
	public static Texture2D texture;
	public Material mat;
	// Use this for initialization
	void Start () {
		texture = new Texture2D (Screen.width, Screen.height);
		mat.mainTexture = texture;

	}
	
	// Update is called once per frame
	void OnPostRender () {
//		RenderTexture rendText = RenderTexture.active;
		texture.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		texture.Apply ();
		mat.mainTexture = texture;
	}
}
