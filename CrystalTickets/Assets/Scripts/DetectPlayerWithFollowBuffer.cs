using UnityEngine;
using System.Collections;

// This adds a 'follow buffer' in which the player won't be detected if they enter it, but if they move out of 
// detection range into this range the enemy will still try to follow them.
public class DetectPlayerWithFollowBuffer : DetectPlayer {

    // How far the player can stray outside the detection range and still be followed by this mob
    public float followBuffer = 2;

    private float followRange;

    void Awake() {
        followRange = detectionRange + followBuffer;
    }

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();
        if (gunObject != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(gunObject.transform.position, followRange);
        }
    }

    public bool PlayerInFollowRange() {
        float distanceToPlayer = Vector2.Distance(enemyPosition, Player.GetPlayerPosition());
        return playerWasDetectedBefore && (distanceToPlayer <= followRange);
    }
}
