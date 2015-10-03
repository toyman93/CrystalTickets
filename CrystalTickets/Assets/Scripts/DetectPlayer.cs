using System;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    public float detectionRange = 5; // Player must move into this range in order to be detected

    // Says what should be visible to this enemy. Easier to set in inspector than programmatically.
    public LayerMask enemyLayerMask;
    // You need to add the prefab referenced here to the CFX_SpawnSystem too, which pools it.
    // Any effects like this should be on the Effects sorting layer (which is in front of everything)
    // (On the particle system, go to Renderer -> Sorting Layer)
    public GameObject alertAnimation;

    protected GameObject gunObject; // Used as origin for raycast towards player
    private Health playerHealth; // Alows us to check whether the player is dead (and celebrate!)
    private Movement movement; // Provides data about / control of this object's movement
    private Animator animator;

    // Used to detect that the player was detected, but has since become hidden/invisible
    protected bool playerWasDetectedBefore;

    public Vector2 enemyPosition { get { return gunObject.transform.position; } private set { } }

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
    protected virtual void OnDrawGizmos () {
        Gizmos.color = Color.yellow;
        if (gunObject != null) {
            Gizmos.DrawWireSphere(gunObject.transform.position, detectionRange);
        }
    }

    void Update() {
        CheckPlayerVisibility();
    }

    private void StopAnimations() {
        animator.SetBool(GameConstants.RunState, false);
        animator.SetBool(GameConstants.ShootState, false);
    }

    private void CheckPlayerVisibility() {
        if (PlayerVisible()) {
            ReactToPlayer();
        } else if (playerWasDetectedBefore) {
            if (!PlayerInRange()) {
                // Player was in range but has moved out of range
                playerWasDetectedBefore = false;
                movement.Unfreeze();
            }
        }
    }

    private void ReactToPlayer() {
        TurnTowardsPlayer();

        // Play alert animation the first time the player is detected
        if (!playerWasDetectedBefore)
            PlayAlertAnimation();
        playerWasDetectedBefore = true;
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

    // Different from PlayerVisible() in that it returns whether the player is in the detection range without 
    // considering whether the player is actually visible
    public bool PlayerInRange() {
        float distanceToPlayer = Vector2.Distance(enemyPosition, Player.GetPlayerPosition());
        return distanceToPlayer <= detectionRange;
    }

    // Returns whether the player is visible (in line of sight, not dead, not hiding, etc.)
    public bool PlayerVisible() {
        bool playerVisible = false;

        if (playerHealth.isDead) {
            StopAnimations();
        } else {
            playerVisible = PlayerInLineOfSight();
        }

        return playerVisible;
    }

    private bool PlayerInLineOfSight() {
        // Raycast towards the player - may be stuff in the way.
        Vector2 directionToPlayer = GetDirectionToPlayer();
        RaycastHit2D hit = Physics2D.Raycast(enemyPosition, directionToPlayer, detectionRange, enemyLayerMask);

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

}
