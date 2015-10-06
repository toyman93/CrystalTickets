using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public bool enabled = false;
	public GameObject targetDoor;
	public GameObject player;

	public float teleportedTime;
	public float teleportInterval = 1.0f;
	public float secondsAfterTeleport;

	public float teleportOffset;

	// Use this for initialization
	void Start () {
		player = Player.GetPlayerGameObject();
	}
	
	// Update is called once per frame
	void Update () {
		secondsAfterTeleport = Time.time - teleportedTime;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log ("collision door");
		if (this.enabled && collision.gameObject == player && (secondsAfterTeleport>teleportInterval)) {
			Debug.Log ("collision door with player");
			Vector3 newPosition = targetDoor.transform.position;
			newPosition.x += teleportOffset;
			player.transform.position = newPosition;
			teleportedTime = Time.time;

		}
	}
}
