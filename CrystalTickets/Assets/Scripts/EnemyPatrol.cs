using UnityEngine;
using System.Collections;
using System;

public class EnemyPatrol : MonoBehaviour {

    private Animator animator;

    public bool startFacingRight = true; // Which way the enemy should face to begin with
    public int unitsToMove = 5; // How far the enemy can move, in whatever direction it faces at the start

    private float rightWall; // Enemy walks to and from here.
    private float leftWall; // 'Units To Move' units away from other wall

    private Movement movement;
    private Vector2 distanceToWalk;
    private bool atPlatformEdge;
    private float directionMultiplier {
        get { return movement.isFacingRight ? 1.0f : -1.0f; }
    }

    void Awake () {
        atPlatformEdge = false;
    }

	void Start () {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();

        animator.SetBool(GameConstants.RunState, true);

        float startPosition = transform.position.x;
        float endPosition = startFacingRight ? startPosition + unitsToMove : startPosition - unitsToMove;

        // The rightmost limit gets to be the right wall
        rightWall = Math.Max(startPosition, endPosition);
        leftWall = Math.Min(startPosition, endPosition);

        if (!startFacingRight)
            movement.Flip(); // Get the sprite facing the correct way initially
    }
	
	void Update () {
        distanceToWalk.x = directionMultiplier * movement.speed * Time.deltaTime;

        if (ShouldTurnAround()) {
            movement.Flip();
            atPlatformEdge = false;
        }

        // TODO move animations to movement? Must be consistent between enemy and player, if so
        if (!movement.isFrozen) {
            animator.SetBool(GameConstants.RunState, true);
            transform.Translate(distanceToWalk);
        } else {
            animator.SetBool(GameConstants.RunState, false);
        }
	}

    // Note: I think it might be better to attach this to the platform edge GameObjects - called more than necessary.
    // Will look into this; it's just an efficiency thing
    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == GameConstants.PlatformEdgeTag)
            atPlatformEdge = true;
    }

    private bool ShouldTurnAround () {
        bool facingWrongWay = false;
        float position = transform.position.x;

        // True if enemy is outside / on the edge of its patrol area
        bool reachedBoundary = movement.isFacingRight && position >= rightWall || !movement.isFacingRight && position <= leftWall;

        if (reachedBoundary || atPlatformEdge)
            facingWrongWay = true;

        return facingWrongWay;
    }
}
