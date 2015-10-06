using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {
	
	public enum ItemTypes {left, right, jump, shoot, empty};
	public ItemTypes typeOfItem;
	public PlayerController playerController;
	
	// Use this for initialization
	void Start () {
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnMouseDown(){

		// set current movement item to the one clicked
		playerController.currentmovement = this.typeOfItem;

	}
	
	public void OnMouseUp(){

		// set current movement item to empty if unclicked
		playerController.currentmovement = Joystick.ItemTypes.empty;

	}
}