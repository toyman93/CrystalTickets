using UnityEngine;
using System.Collections;

// Super-simple health pack script
public class Medpac : MonoBehaviour {

    public int healAmount = 30; // Usually out of 100 (max health)

    void OnTriggerEnter2D(Collider2D collider) {
        Health health = collider.gameObject.GetComponent<Health>();
        health.AddHealth(healAmount);
        Destroy(gameObject);
    }
}
