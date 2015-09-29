using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int speed = 1000;
    public int damageOnHit = 35;

    // Objects with this tag can be hit/damaged by the bullet
    public string hostileTag;

    void Start () {

    }

    void Update() {

    }

    // Fires horizontally. Used by player.
    public void Fire(bool isFacingRight) {
        Vector2 direction;

        if (isFacingRight) {
            direction = Vector2.right;
        } else {
            direction = Vector2.left;

            // Flip the sprite/animation to face left
            Vector3 flippedScale = transform.localScale;
            flippedScale.x *= -1;
            transform.localScale = flippedScale;
        }

        FireInDirection(direction);
    }

    public void FireInDirection(Vector2 direction) {
        // For whatever reason this wasn't working as a field
        RotateTowards(direction);
        GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    public void RotateTowards(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 10000);
    }

    void OnBecameInvisible() {
        // TODO It'd be better to pool projectiles rather than destroy/instantiate them all the time.
        Destroy(gameObject);
    }

    // Using OnTriggerEnter instead of OnCollisionEnter stops pushback (by preventing the physical collision)
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag(hostileTag)) {
            Destroy(gameObject); // Destroys the bullet, not the enemy.
            Health health = collider.gameObject.GetComponent<Health>();
            if (health != null)
                health.Damage(damageOnHit); // Damage the enemy
        }
    }
}
