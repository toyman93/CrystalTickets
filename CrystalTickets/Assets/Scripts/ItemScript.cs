using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public enum ItemTypes {Pistol, Grenade, HealthPack};

	public ItemTypes typeOfItem;

	public Player player;

	// Use this for initialization
	void Start () {
		Debug.Log ("created");
		player = GameObject.FindWithTag("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		// this object was clicked - do something
		Debug.Log ("clicked");
		Debug.Log ("Item: " + this.typeOfItem);

		Debug.Log ("Player Health: " + player.health);
	}   

}
