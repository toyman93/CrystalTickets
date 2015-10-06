using UnityEngine;
using System.Collections;

public class MoveRight : MonoBehaviour {

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
	
	//		public void OnMouseDown(){
	//			// this object was clicked - do something
	//			Debug.Log ("Item: " + this.typeOfItem);
	//	
	//		if (this.typeOfItem == ItemTypes.left) {
	//			playerController.currentmovement = this.typeOfItem;
	//		} else if (this.typeOfItem == ItemTypes.right) {
	//			playerController.currentmovement2 = this.typeOfItem;
	//		} else if (this.typeOfItem == ItemTypes.jump) {
	//			playerController.currentmovement3 = this.typeOfItem;
	//		} else if (this.typeOfItem == ItemTypes.shoot) {
	//			playerController.currentmovement4 = this.typeOfItem;
	//		}
	//	}
	//	
	//	public void OnMouseUp(){
	//		Debug.Log ("Mouse up");
	//		if (this.typeOfItem == ItemTypes.left) {
	//			playerController.currentmovement = ItemTypes.empty;
	//		} else if (this.typeOfItem == ItemTypes.right) {
	//			playerController.currentmovement2 = ItemTypes.empty;
	//		} else if (this.typeOfItem == ItemTypes.jump) {
	//			playerController.currentmovement3 = ItemTypes.empty;
	//		} else if (this.typeOfItem == ItemTypes.shoot) {
	//			playerController.currentmovement4 = ItemTypes.empty;
	//		}
	//	}

	public void OnMouseDown(){
		// this object was clicked - do something
		Debug.Log ("right");
		playerController.currentmovement2 = this.typeOfItem;
		
	}
	
	public void OnMouseUp(){
		playerController.currentmovement2 = ItemTypes.empty;
		
	}
}
