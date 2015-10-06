using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {

    public int damage = 25;

    private Movement movement;

	void OnCollisionEnter2D(Collision2D collision) {
        Movement movement = collision.gameObject.GetComponent<Movement>();
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null) {
            if (!health.isDead)
                if (movement != null)
                    movement.Jump();
            health.RemoveHealth(damage);
        }
	}
}
