using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour {
	public SpriteRenderer midScroll;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		// At start of scene, set timescale to 1.0
		Time.timeScale = 1.0f;

		// Hide the components upon resuming
		midScroll.enabled = false;
		this.gameObject.SetActive (false);

	}
}
