using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {

    public GameObject player;
    private Movement movement;
    private Health playerHealth;

	void Start () {
        playerHealth = player.GetComponent<Health>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject == player) {
			movement.Jump ();
		}
	}
}
