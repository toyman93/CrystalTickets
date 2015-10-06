using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public bool startFacingRight = true; // Which way the enemy should face to begin with
    public int unitsToMove = 5; // How far the enemy can move, in whatever direction it faces at the start

    private float rightWall; // Enemy walks to and from here.
    private float leftWall; // 'Units To Move' units away from other wall
    private Health health;
    private Movement movement;
    private FollowPlayer follow;
    private Animator animator;

    void Start () {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        follow = GetComponent<FollowPlayer>();

        animator.SetBool(GameConstants.RunState, true);

        float startPosition = transform.position.x;
        float endPosition = startFacingRight ? startPosition + unitsToMove : startPosition - unitsToMove;

        // The rightmost limit gets to be the right wall
        rightWall = Mathf.Max(startPosition, endPosition);
        leftWall = Mathf.Min(startPosition, endPosition);

        if (!startFacingRight)
            movement.Flip(); // Get the sprite facing the correct way initially
    }
	
	void Update () {
        if (!follow.isFollowing) {
            if (ShouldTurnAround())
                movement.Flip();

            if (!movement.isFrozen) {
                animator.SetBool(GameConstants.RunState, true);
                movement.Move();
            } else {
                animator.SetBool(GameConstants.RunState, false);
            }
        }
	}

    private bool ShouldTurnAround () {
        float position = transform.position.x;

        // True if enemy is outside / on the edge of its patrol area
        bool facingWrongWay = movement.isFacingRight && position >= rightWall || !movement.isFacingRight && position <= leftWall;

        return facingWrongWay;
    }
}
