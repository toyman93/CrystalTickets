using UnityEngine;
using System.Collections;

public class GameEnd : MonoBehaviour {

	public SpriteRenderer gameEndScreen;

	// Use this for initialization
	void Start () {
		gameEndScreen.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Stop time if character touches game ending scroll
	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.name == "playerCharacter") {
			Time.timeScale = 0.0f;
			gameEndScreen.enabled = true;
		}
	}
	
}
