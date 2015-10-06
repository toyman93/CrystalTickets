using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed;
    public float jumpForce;
    [Tooltip("Empty GameObject representing the player's feet - used for detecting the ground")]
    public Transform groundCheck;
    [Tooltip("Radius of the overlap circle used to detect whether the player is in contact with the ground")]
    public float groundRadius = 0.1f;

    public bool isFrozen { get; private set; }
    private Rigidbody2D rigidBody;
    private RigidbodyConstraints2D savedConstraints;
    private Vector2 savedVelocity;

    // The moving thing is expected to be animated with idle and run states
    private Animator animator;

    public bool isFacingRight { get; private set; }
    bool grounded = false;

    // The horizontal direction this thing is moving in (left or right)
    public Vector2 movementDirection { get { return isFacingRight ? Vector2.right : Vector2.left; } private set { } }

    // Toggle this on to prevent movement while jumping
    [HideInInspector]
    public bool movementControlOff = false;

    void Awake() {
        isFacingRight = true;
    }

    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        bool isMovingDown = rigidBody.velocity.y <= 0;
        if (isMovingDown) {
            animator.SetBool(GameConstants.JumpState, false);
            movementControlOff = false;
            grounded = true;
        }
    }

    public void Jump() {

        if (grounded) {
            movementControlOff = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            animator.SetBool(GameConstants.JumpState, true);
			grounded = false;
        }
    }

    public void Flip() {
        if (movementControlOff)
            return;

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

    // Only allows the mob to move left/right to get to the point (else you end up with this odd bouncing)
    public void MoveTowardsPoint(Vector2 point) {
        Vector2 directionToPoint = point - (Vector2) transform.position;
        if (directionToPoint.x < 0) {
            MoveLeft();
        } else {
            MoveRight();
        }
    }

    public void Move() {
        Move(movementDirection);
    }

    public void MoveLeft() {
        if (isFacingRight)
            Flip();
        Move(Vector2.left);
    }

    public void MoveRight() {
        if (!isFacingRight)
            Flip();
        Move(Vector2.right);
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir) {
        float timer = 0;
        while (knockDur > timer) {
            timer += Time.deltaTime;
            rigidBody.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
        }
        yield return 0;
        Move(Vector2.left);
    }

    private void Move(Vector2 direction) {
        if (movementControlOff)
            return;

        animator.SetBool(GameConstants.RunState, true);
        Vector2 distanceToMove = direction * speed * Time.deltaTime;
        transform.Translate(distanceToMove);
    }
}
