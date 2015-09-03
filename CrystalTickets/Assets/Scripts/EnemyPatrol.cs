using UnityEngine;
using System.Collections;
using System;

public class EnemyPatrol : MonoBehaviour {

    public bool startFacingRight = true; // Which way the enemy should face to begin with
    public int unitsToMove = 5; // How far the enemy can move, in whatever direction it faces at the start
    public float speed = 4;

    private float rightWall; // Enemy walks to and from here.
    private float leftWall; // 'Units To Move' units away from other wall

    private Vector2 distanceToWalk;
    private bool isFacingRight; // Syntactic sugar...
    private float directionMultiplier {
        get { return isFacingRight ? 1.0f : -1.0f; } // => syntax not compiling?
    }
    private bool atPlatformEdge;

    // Note: take this stuff out to a movement class
    private bool isFrozen;
    private Rigidbody2D rigidBody;
    private RigidbodyConstraints2D savedConstraints;
    private Vector2 savedVelocity;

    // Note: Exposes this to DetectPlayer. Perhaps create a movement class / data structure?
    public bool IsFacingRight () {
        return isFacingRight;
    }

    void Awake () {
        isFacingRight = true;
        atPlatformEdge = false;
    }

	void Start () {
        float startPosition = transform.position.x;
        float endPosition = startFacingRight ? startPosition + unitsToMove : startPosition - unitsToMove;

        // The rightmost limit gets to be the right wall
        rightWall = Math.Max(startPosition, endPosition);
        leftWall = Math.Min(startPosition, endPosition);

        if (!startFacingRight)
            Flip(); // Get the sprite facing the correct way initially

        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        distanceToWalk.x = directionMultiplier * speed * Time.deltaTime;

        if (ShouldTurnAround())
            Flip();

        if (!isFrozen)
            transform.Translate(distanceToWalk);
	}

    // Note: I think it might be better to attach this to the platform edge GameObjects - called more than necessary.
    // Will look into this; it's just an efficiency thing
    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "PlatformEdge")
            atPlatformEdge = true;
    }

    private bool ShouldTurnAround () {
        bool facingWrongWay = false;
        float position = transform.position.x;

        // True if enemy is outside / on the edge of its patrol area
        bool reachedBoundary = isFacingRight && position >= rightWall || !isFacingRight && position <= leftWall;

        if (reachedBoundary || atPlatformEdge)
            facingWrongWay = true;

        return facingWrongWay;
    }

    // Note: duplicated from PlayerController...
    public void Flip () {
        isFacingRight = !isFacingRight;
        Vector3 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;

        atPlatformEdge = false; // ...except for this line
    }

    // Note: refactor out to a movement script with Flip()?
    public void Freeze () {
        // Save current movement, so that it can be restored
        savedVelocity = rigidBody.velocity;
        savedConstraints = rigidBody.constraints;

        // Stop right there, criminal scum!
        rigidBody.velocity = Vector3.zero;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        isFrozen = true;
    }

    public void Unfreeze () {
        // Restore old movement
        rigidBody.velocity = savedVelocity;
        rigidBody.constraints = savedConstraints;

        isFrozen = false;
    }
}
