﻿using UnityEngine;
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

			// Change the sprite of the doors to open
			entranceDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load ("doorOpen_top", typeof(Sprite)) as Sprite;
			exitDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load ("doorOpen_top", typeof(Sprite)) as Sprite;

			// Change the sprite of the lever to on
			GetComponent<SpriteRenderer>().sprite = Resources.Load ("LeverOn", typeof(Sprite)) as Sprite;
		} 

	}
}
