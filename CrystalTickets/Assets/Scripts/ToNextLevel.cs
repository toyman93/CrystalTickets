using UnityEngine;
using System.Collections;

public class ToNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		// Load level on click
		Application.LoadLevel ("LevelTwo");
	
		// At start of scene, set timescale to 1.0
		Time.timeScale = 1.0f;
	}
}
