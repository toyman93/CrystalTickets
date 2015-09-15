using UnityEngine;
using System.Collections;

public class doorEntrance : MonoBehaviour {

	public GameObject exitDoor;
	private GameObject playerCharacter;

	void OnCollisionEnter2D(Collision2D col){
		print ("hi");
		if (col.gameObject.name == "playerCharacter" && TestPlayerController.activateDoor) {
			print ("Collided");
			print (exitDoor.transform.position);
			Vector3 newPosition = exitDoor.transform.position;
			playerCharacter = GameObject.Find("playerCharacter");
			playerCharacter.transform.position = newPosition;
		}

	}


}