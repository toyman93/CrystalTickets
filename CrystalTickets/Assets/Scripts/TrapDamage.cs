using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {
	private TestPlayerController character;
	Collider2D agent7;
	// Use this for initialization
	void Awake () {
		GameObject thePlayer = GameObject.Find("playerCharacter");
		character = thePlayer.GetComponent<TestPlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//character.LoseHealth();
		StartCoroutine(character.Knockback(0.02f, 350, character.transform.position));
		//Destroy (gameObject);
	}
}
