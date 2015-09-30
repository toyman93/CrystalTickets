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
    public bool playerDetected { get; private set; }

    private GameObject gunObject; // Used as origin for raycast towards player
    private Health playerHealth; // Alows us to check whether the player is dead (and celebrate!)
    private Movement movement; // Provides data about / control of this object's movement
    private Animator animator;

    public Vector2 enemyPosition { get { return gunObject.transform.position; } private set { } }

    void Awake () {
        playerDetected = false;
    }

    void Start () {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        playerHealth = Player.GetPlayerGameObject().GetComponent<Health>();

        // Gets the child GameObject that represents the gun's position
        foreach (Transform child in transform)
            if (child.CompareTag(GameConstants.GunTag))
                gunObject = child.gameObject;
    }

    // Displays detection range as gizmo (circle)
    void OnDrawGizmos () {
        Gizmos.color = Color.yellow;
        if (gunObject != null)
            Gizmos.DrawWireSphere(gunObject.transform.position, detectionRange);
    }

    void Update() {
        if (playerHealth.isDead) {
            playerDetected = false; // Turn off reactions to the player
            StopAnimations();
        } else {
            CheckPlayerVisibility();
        }
    }

    private void StopAnimations() {
        animator.SetBool(GameConstants.RunState, false);
        animator.SetBool(GameConstants.ShootState, false);
    }

    private void CheckPlayerVisibility() {
        if (PlayerVisibleFromPosition(enemyPosition)) {
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

    // Should return whether the player is in the line and range of sight of the enemy
    private bool PlayerVisibleFromPosition(Vector2 origin) {
        // Raycast towards the player - may be stuff in the way.
        Vector2 directionToPlayer = GetDirectionToPlayer();
        RaycastHit2D hit = Physics2D.Raycast(origin, directionToPlayer, detectionRange, enemyLayerMask);

        // Can we see the player?
        bool playerInLineOfSight = false;
        if (hit && hit.collider.CompareTag(GameConstants.PlayerTag))
            playerInLineOfSight = true;
        return playerInLineOfSight;
    }

    public Vector2 GetDirectionToPlayer() {
        // Get a direction vector from us to the target
        Vector2 directionToPlayer = Player.GetPlayerPosition() - enemyPosition;

        // Normalize it so that it's a unit direction vector
        directionToPlayer.Normalize();

        return directionToPlayer;
    }

    private void TurnTowardsPlayer() {
        // Stop movement
        movement.Freeze();

        // Turn towards the player
        bool isFacingRight = movement.isFacingRight;
        Vector2 directionToPlayer = GetDirectionToPlayer();
        if (directionToPlayer.x < 0 && isFacingRight || directionToPlayer.x > 0 && !isFacingRight)
            movement.Flip();
    }
    
    private void PlayAlertAnimation() {
        GameObject alert = CFX_SpawnSystem.GetNextObject(alertAnimation); // More efficient than Instantiate() - pools objects.
        alert.transform.position = enemyPosition;
    }
}
