using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour {

	public GameObject entranceDoor;
	public GameObject exitDoor;
	public GameObject player;
	public bool enabled = false;

	private DoorController entranceDoorController;
	private DoorController exitDoorController;

	// Use this for initialization
	void Start () {
		player = Player.GetPlayerGameObject();
		entranceDoorController = entranceDoor.GetComponent<DoorController> ();
		exitDoorController = exitDoor.GetComponent<DoorController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject == player && enabled == false) {
			Debug.Log("lever: enabling doors");
			enabled = true;
			entranceDoorController.enabled = true;
			exitDoorController.enabled = true;
		} 
//		if (collision.gameObject == player && enabled == true){
//			Debug.Log("lever: disabling doors");
//
//			enabled = false;
//			entranceDoorController.enabled = false;
//			exitDoorController.enabled = false;
//		}		
	}
}
