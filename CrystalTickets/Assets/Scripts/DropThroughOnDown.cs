using UnityEngine;
using System.Collections;

public class DropThroughOnDown : MonoBehaviour {

	private bool _playerOnObject;

	// Update is called once per frame
	void Update () {
		if (_playerOnObject)
			if (Input.GetAxis ("Vertical") < 0) {
				(gameObject.GetComponent(typeof(Collider2D)) as Collider2D).isTrigger = true;
			} else {
				(gameObject.GetComponent(typeof(Collider2D)) as Collider2D).isTrigger = false;
			}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player")
			_playerOnObject = true;
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == "Player")
			_playerOnObject = false;
	}
}
