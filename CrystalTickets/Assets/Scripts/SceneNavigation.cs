using UnityEngine;
using System.Collections;

public class SceneNavigation : MonoBehaviour {

	public string nextScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		Application.LoadLevel(nextScene);
	}
}
