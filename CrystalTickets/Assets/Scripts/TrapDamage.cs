using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {

    private GameObject player;
    private Movement playerMovement;
    private Health playerHealth;

	void Awake () {
        player = Player.GetPlayerGameObject();
        playerMovement = player.GetComponent<Movement>();
        playerHealth = player.GetComponent<Health>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		StartCoroutine(playerMovement.Knockback(0.02f, 350, player.transform.position));
        // Uncomment to damage player (set to whatever - out of 100)
        //playerHealth.RemoveHealth(30);
	}
}
