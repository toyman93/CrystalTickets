using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;

    public bool isFrozen { get; private set; }
    private Rigidbody2D rigidBody;
    private RigidbodyConstraints2D savedConstraints;
    private Vector2 savedVelocity;

    public bool isFacingRight { get; private set; } // Syntactic sugar...

    void Awake () {
        isFacingRight = true;
    }

    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }

    public void Freeze() {
        // Save current movement, so that it can be restored
        savedVelocity = rigidBody.velocity;
        savedConstraints = rigidBody.constraints;

        // Stop right there, criminal scum!
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = 0;
        rigidBody.velocity = newVelocity;
        // We need to mask the old constraints with the frozen X axis (want to keep rotation turned off)
        rigidBody.constraints = rigidBody.constraints | RigidbodyConstraints2D.FreezePositionX;
        isFrozen = true;
    }

    public void Unfreeze() {
        // Restore old movement
        rigidBody.velocity = savedVelocity;
        rigidBody.constraints = savedConstraints;

        isFrozen = false;
    }
}
