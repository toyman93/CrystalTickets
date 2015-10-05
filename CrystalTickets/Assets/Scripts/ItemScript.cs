using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public enum ItemTypes {Pistol, Grenade, HealthPack};

	public ItemTypes typeOfItem;

	public TestPlayerController playerController;

	// Use this for initialization
	void Start () {
		Debug.Log ("created");
		playerController = GameObject.FindWithTag("Player").GetComponent<TestPlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		// this object was clicked - do something
		Debug.Log ("clicked");
		Debug.Log ("Item: " + this.typeOfItem);

		playerController.currentItem = this.typeOfItem;
	}   

}
