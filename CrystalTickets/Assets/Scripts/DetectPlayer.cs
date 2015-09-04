using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    public Transform target;
    public float detectionRange = 5;

    // You need to add the prefab referenced here to the CFX_SpawnSystem too, which pools it.
    // Any effects like this should be on the Effects sorting layer (which is in front of everything)
    // (On the particle system, go to Renderer -> Sorting Layer)
    public GameObject alertAnimation;

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

            // Get a direction vector from us to the target
            Vector2 lineOfSight = target.position - transform.position;

            // Normalize it so that it's a unit direction vector
            lineOfSight.Normalize();

            // Stop and turn towards the 
            patrol.movement.Freeze();
            bool isFacingRight = patrol.movement.isFacingRight;
            if (lineOfSight.x < 0 && isFacingRight || lineOfSight.x > 0 && !isFacingRight)
                patrol.movement.Flip();

            // Play alert animation the first time the player is detected
            if (!playerDetected) {
                GameObject alert = CFX_SpawnSystem.GetNextObject(alertAnimation); // More efficient than Instantiate() - pools objects.
                alert.transform.position = transform.position;
            }

            playerDetected = true;

        } else if (playerDetected) {
            // Player was in range but has moved out of range
            playerDetected = false;
            patrol.movement.Unfreeze();
        }
    }
}
