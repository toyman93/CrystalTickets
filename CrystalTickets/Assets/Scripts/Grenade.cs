using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

    public int speed = 1000;


    void Start () {
        
    }

    void Update () {

    }

    public void Fire(bool isFacingRight) {
        Vector2 direction;

        if (isFacingRight) {
            direction = Vector2.right;
			direction = new Vector2(1, (float)(1.2));
        } else {
            direction = Vector2.left;
			direction = new Vector2(-1, (float)(1.2));

            // Flip the sprite/animation to face left
            Vector3 flippedScale = transform.localScale;
            flippedScale.x *= -1;
            transform.localScale = flippedScale;
        }


        GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    void OnBecameInvisible() {
        // TODO It'd be better to pool projectiles rather than destroy/instantiate them all the time.
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("Enemy"))
            Destroy(gameObject); // Destroys the bullet, not the enemy.
    }
}
