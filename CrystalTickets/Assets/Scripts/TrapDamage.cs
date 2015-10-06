using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {

    public int damage = 25;

    private Movement movement;
    private Health playerHealth;

	void Start () {
        GameObject player = Player.GetPlayerGameObject();
        playerHealth = player.GetComponent<Health>();
        movement = player.GetComponent<Movement>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag(GameConstants.PlayerTag)) {
            if (playerHealth.isDead)
			    movement.Jump ();
            playerHealth.RemoveHealth(damage);
		}
	}
}
