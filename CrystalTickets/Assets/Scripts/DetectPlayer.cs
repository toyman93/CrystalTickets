using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour {

    public Transform target;
    public float detectionRange = 5;

    private bool playerDetected;
    private EnemyPatrol patrol;

    void Awake () {
        playerDetected = false;
    }

    void Start () {
        patrol = GetComponent<EnemyPatrol>();
    }

    // Displays detection range as gizmo (circle)
    void OnDrawGizmos () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    void Update() {
        if (Vector2.Distance(target.position, transform.position) < detectionRange) {
            playerDetected = true;

            // Get a direction vector from us to the target
            Vector2 lineOfSight = target.position - transform.position;

            // Normalize it so that it's a unit direction vector
            lineOfSight.Normalize();

            // Stop and turn towards the 
            patrol.Freeze();
            bool isFacingRight = patrol.IsFacingRight();
            if (lineOfSight.x < 0 && isFacingRight || lineOfSight.x > 0 && !isFacingRight)
                patrol.Flip();

            // Play alert animation (should be one of those trigger things)
        } else if (playerDetected) {
            // Player was in range but has moved out of range
            playerDetected = false;
            patrol.Unfreeze();
        }
    }
}
