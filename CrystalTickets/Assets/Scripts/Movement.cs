using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;

    public bool isFrozen { get; private set; }
    private Rigidbody2D rigidBody;
    private RigidbodyConstraints2D savedConstraints;
    private Vector2 savedVelocity;

    // The moving thing is expected to be animated with idle and run states
    private Animator animator;

    public bool isFacingRight { get; private set; }

    // The horizontal direction this thing is moving in (left or right)
    public Vector2 movementDirection { get { return isFacingRight ? Vector2.right : Vector2.left; } private set { } }

    void Awake () {
        isFacingRight = true;
    }

    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        // Set animation state
        animator.SetBool(GameConstants.RunState, false);
    }

    public void Unfreeze() {
        // Restore old movement
        rigidBody.velocity = savedVelocity;
        rigidBody.constraints = savedConstraints;
        animator.SetBool(GameConstants.RunState, true);
        isFrozen = false;
    }
}
