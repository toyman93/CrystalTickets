using UnityEngine;
using System.Collections;
using System;

public class JumpNode : Node {

    public enum Direction { Left, Right };

    public Direction jumpDirection;

    [Tooltip("Used to check whether to jump. If the player is above this height, make the enemy jump.")]
    public float heightOfPlatformAbove = 4;

    void OnDrawGizmos() {
        Vector2 pos = transform.position;
        Gizmos.DrawLine(pos + new Vector2(-1, heightOfPlatformAbove), pos + new Vector2(1, heightOfPlatformAbove));
    }

    protected override bool ShouldTrigger(FollowPlayer follow) {
        // Only jump if the enemy is following the player and if the player is actually above the enemy
        bool playerAboveEnemy = follow.playerPosition.y > (follow.enemyPosition.y + heightOfPlatformAbove);
        return follow.isFollowing && playerAboveEnemy;
    }

    protected override void Trigger(Movement movement) {
        // Make the mob jump in the right direction
        if (jumpDirection == Direction.Left) {
            movement.MoveLeft();
        } else {
            movement.MoveRight();
        }

        movement.Jump();
    }
}
