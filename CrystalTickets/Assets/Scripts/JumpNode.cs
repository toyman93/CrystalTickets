using UnityEngine;
using System.Collections;
using System;

public class JumpNode : Node {

    public enum Direction { Left, Right };

    public Direction jumpDirection;

    // Used to check whether to jump. If the player is above this height, make the enemy jump.
    public float heightOfPlatformAbove = 4;

    protected override bool ShouldTrigger(FollowPlayer follow) {
        // Only jump if the enemy is following the player
        return follow.isFollowing;
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
