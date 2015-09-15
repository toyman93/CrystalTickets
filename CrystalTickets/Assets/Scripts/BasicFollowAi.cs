using UnityEngine;
using System.Collections;

public class BasicFollowAi : MonoBehaviour {

    public Transform target;
    public int moveSpeed;
    public int maxDist;
    public int minDist;
    public int testHealth = 5;

    void Start() {

    }

    void Update() {

        if (Vector3.Distance(target.position, transform.position) > maxDist) {
            // Get a direction vector from us to the target
            Vector3 dir = target.position - transform.position;

            // Normalize it so that it's a unit direction vector
            dir.Normalize();

            // Move ourselves in that direction
            transform.position += dir * moveSpeed * Time.deltaTime;
        }
    }

    // Super-basic health system for testing
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("TestDamage")) {
            if (testHealth-- == 0)
                Destroy(gameObject);
        }
    }
}