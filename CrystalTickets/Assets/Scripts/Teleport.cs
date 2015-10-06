using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public GameObject entranceDoor;
	public GameObject exitDoor;
	public GameObject player;
	private bool enabled;

	// Use this for initialization
	void Start () {
		enabled = false;
		player = Player.GetPlayerGameObject();
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject == player && !enabled) {
			enabled = true;
		} 
		if (collision.gameObject == player && enabled){
			enabled = false;
		}

		if (collision.gameObject == entranceDoor && enabled) {
			Vector3 newPosition = exitDoor.transform.position;
			player.transform.position = newPosition;
		}

		if (collision.gameObject == exitDoor && enabled) {
			Vector3 newPosition = entranceDoor.transform.position;
			player.transform.position = newPosition;
		}
	}
}
