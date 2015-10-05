using UnityEngine;
using System.Collections;

// Super-simple health pack script
public class Medpac : MonoBehaviour {

    public int healAmount = 30; // Usually out of 100 (max health)

    void OnTriggerEnter2D(Collider2D collider) {
        Health health = collider.gameObject.GetComponent<Health>();
        bool healed = health.AddHealth(healAmount);

        // Don't use up the medpac unless the player was actually healed (not at max health)
        if (healed)
            Destroy(gameObject);
    }
}
