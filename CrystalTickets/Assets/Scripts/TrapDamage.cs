using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {
	private PlayerController character;
	Collider2D agent7;
	// Use this for initialization
	void Awake () {
		GameObject thePlayer = GameObject.Find("playerCharacter");
		character = thePlayer.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//agent7 = other; //obtain reference to agent7
		character.LoseHealth();
		//Destroy (gameObject);
	}
}
