using UnityEngine;
using System.Collections;

// Just temporary! Decreases health every x seconds.
public class TestHealthScript : MonoBehaviour {

    // Health to damage
    private PlayerHealth playerHealth;
    public int damage = 20;

    public float damageIntervalInSeconds = 3f;
    private float timeLastDamaged;

    void Awake() {
        timeLastDamaged = 0;
    }

	void Start () {
        playerHealth = GetComponent<PlayerHealth>();
	}
	
	void Update () {
        float secondsSinceLastFired = Time.time - timeLastDamaged;

        if (secondsSinceLastFired > damageIntervalInSeconds) {
            timeLastDamaged = Time.time;
            playerHealth.damage(damage);
        }
    }
}
