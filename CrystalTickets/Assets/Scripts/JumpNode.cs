using UnityEngine;
using System.Collections;
using System;

public class JumpNode : Node {

    public enum Direction { Left, Right };

    public Direction jumpDirection;

    // Used to check whether to jump. If the player is above this height, make the enemy jump.
    public float heightOfPlatformAbove = 4;

    // Either left or right
    private Vector2 direction {  get { return new Vector2(jumpDirection == Direction.Left ? -1 : 1, 0); } }

    protected override bool ShouldTrigger(FollowPlayer follow) {
        // Only jump if the enemy is following the player
        return follow.isFollowing;
    }

    protected override void Trigger(Movement movement) {
        movement.Move(direction); // Get the enemy moving in the right direction, at the right speed
        movement.Jump();
    }
}
