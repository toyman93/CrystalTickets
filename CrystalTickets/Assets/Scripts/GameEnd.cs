using UnityEngine;
using System.Collections;

public class GameEnd : MonoBehaviour {

	public SpriteRenderer gameEndScreen;
	public GameObject nextButton;

	// Use this for initialization
	void Start () {
		gameEndScreen.enabled = false;
		nextButton.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Stop time if character touches game ending scroll
	void OnCollisionEnter2D(Collision2D col) {

		// If the scroll touches the player, end the scene
		if (col.gameObject.name == "playerCharacter") {
			Time.timeScale = 0.0f;
			gameEndScreen.enabled = true;
			nextButton.SetActive(true);
		}
	}
}
