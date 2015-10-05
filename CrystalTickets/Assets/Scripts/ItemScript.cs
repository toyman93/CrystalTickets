using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour {

	public enum ItemTypes {Pistol, Grenade, HealthPack};

	public ItemTypes typeOfItem;

	public PlayerController playerController;

	private Image currentWeaponImage;
	private GameObject currentWeapon;

	// Use this for initialization
	void Start () {
		Debug.Log ("created");
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
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
