using UnityEngine;
using System.Collections;

public class DropThroughOnDown : MonoBehaviour {

	private bool playerOnObject;

	void Update () {
		if (playerOnObject)
            (gameObject.GetComponent(typeof(Collider2D)) as Collider2D).isTrigger = Input.GetAxis("Vertical") < 0;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Player")
			playerOnObject = true;
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == "Player")
			playerOnObject = false;
	}
}
