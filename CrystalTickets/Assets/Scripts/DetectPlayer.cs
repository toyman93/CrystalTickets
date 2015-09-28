using System;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    public float detectionRange = 5;

    // Says what should be visible to this enemy. Easier to set in inspector than programmatically.
    public LayerMask enemyLayerMask;

    // You need to add the prefab referenced here to the CFX_SpawnSystem too, which pools it.
    // Any effects like this should be on the Effects sorting layer (which is in front of everything)
    // (On the particle system, go to Renderer -> Sorting Layer)
    public GameObject alertAnimation;

    public bool playerDetected { get; private set; } // Player is in detection range (not necessarily in LoS)
    public bool playerInLineOfSight { get; private set; } // Player can be seen (and therefore shot at)

    private Renderer playerRenderer; // Use the renderer rather than transform to get the centre of the object
    public Vector2 directionToPlayer { get; private set; } // Direction from this object to the player

    private Movement movement; // Provides data about / control of this object's movement

    void Awake () {
        playerDetected = false;
    }

    void Start () {
        movement = GetComponent<Movement>();
        playerRenderer = Player.GetPlayerGameObject().GetComponent<Renderer>();
        directionToPlayer = movement.movementDirection; // Arbitrarily, whatever direction we are facing
    }

    // Displays detection range as gizmo (circle)
    void OnDrawGizmos () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    void Update() {
        // Refreshes the vector that points towards the player
        SetDirectionToPlayer();

        if (Vector2.Distance(playerRenderer.bounds.center, transform.position) < detectionRange) {
            TurnTowardsPlayer();

            // Play alert animation the first time the player is detected
            if (!playerDetected)
                PlayAlertAnimation();
            playerDetected = true;

        } else if (playerDetected) {
            // Player was in range but has moved out of range
            playerDetected = false;
            movement.Unfreeze();
        }
    }

    private void SetDirectionToPlayer() {
        // Get a direction vector from us to the target
        directionToPlayer = playerRenderer.bounds.center - transform.position;

        // Normalize it so that it's a unit direction vector
        directionToPlayer.Normalize();
    }

    // Hiding spaces will later be another factor in payer visibility. For now, just raycast.
    public bool IsPlayerInLineOfSight(Vector2 origin) {
        bool playerInLineOfSight = false;

        // Raycast towards the player - may be stuff in the way.
        RaycastHit2D hit = Physics2D.Raycast(origin, directionToPlayer, detectionRange, enemyLayerMask);

        // Can we see the player?
        if (hit && hit.collider.CompareTag(GameConstants.PlayerTag))
            playerInLineOfSight = true;

        return playerInLineOfSight;
    }

    private void TurnTowardsPlayer() {
        // Stop movement
        movement.Freeze();

        // Turn towards the player
        bool isFacingRight = movement.isFacingRight;
        if (directionToPlayer.x < 0 && isFacingRight || directionToPlayer.x > 0 && !isFacingRight)
            movement.Flip();
    }

    private void PlayAlertAnimation() {
        GameObject alert = CFX_SpawnSystem.GetNextObject(alertAnimation); // More efficient than Instantiate() - pools objects.
        alert.transform.position = transform.position;
    }
}
