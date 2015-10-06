using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    // Just a note on design: the coupling between AI scripts seems like a bit of a bad smell, but at the moment it's
    // not too bad - DetectPlayer turns off EnemyPatrol, is used by ShootPlayer, and this one should only need to 
    // interact with DetectPlayer as well. Consider design changes if coupling between AI scripts becomes too tight.

    public bool isFollowing { get; private set; }

    public Vector2 playerPosition { get { return detectPlayer.playerPosition; } } // Player location
    public Vector2 enemyPosition { get { return transform.position; } }

    private DetectPlayerWithFollowBuffer detectPlayer; // Following is triggered when the player moves out of range (detected here)
    private Tether tether; // Allows the enemy to return to its starting position
    private Movement movement;

    void Awake () {
        isFollowing = false;
    }

	void Start () {
        detectPlayer = GetComponent<DetectPlayerWithFollowBuffer>();
        tether = GetComponent<Tether>();
        movement = GetComponent<Movement>();
    }
	
	void Update () {
        isFollowing = ShouldFollowPlayer ();

        if (isFollowing) {
            tether.turnedOn = true; // Stops the enemy from getting too far from its starting position
            movement.MoveTowardsPoint(playerPosition);
        } 
	}

    // If the player isn't in the range to be shot but is in the range to be followed, follow the player
    private bool ShouldFollowPlayer () {
        bool playerInRange = detectPlayer.PlayerInFollowRange() && !detectPlayer.PlayerInVisibilityRange();
        bool tooFarFromHome = tether.TooFarFromHome();
        bool playerHasBeenDetected = detectPlayer.playerWasDetectedBefore;
        return playerInRange && !tooFarFromHome && playerHasBeenDetected;
    }
}
