using UnityEngine;

public class WallNode : Node {

    protected override bool ShouldTrigger(FollowPlayer follow) {
        // Turn off these walls when the enemy is following the player
        return !follow.isFollowing; 
    }

    protected override void Trigger(Movement movement) {
        movement.Flip();
    }
}
