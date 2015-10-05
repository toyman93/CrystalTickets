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
		// this object was clicked - do something
		Debug.Log ("Item: " + this.typeOfItem);

		if (playerController.currentmovement == ItemTypes.empty) {
			playerController.currentmovement = this.typeOfItem;
		} else if (playerController.currentmovement2 == ItemTypes.empty) {
			playerController.currentmovement2 = this.typeOfItem;
		} else if (playerController.currentmovement3 == ItemTypes.empty) {
			playerController.currentmovement3 = this.typeOfItem;
		}
	}
	
	public void OnMouseUp(){
		Debug.Log ("Mouse up");
		playerController.currentmovement = ItemTypes.empty;
		playerController.currentmovement2 = ItemTypes.empty;
		playerController.currentmovement3 = ItemTypes.empty;
	}
}
