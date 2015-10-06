using UnityEngine;
using System.Collections;

// Script that records starting position and allows the mob to move between points in the world
[RequireComponent (typeof (Movement))]
public class Tether : MonoBehaviour {

    [Tooltip("How far away from the starting point the mob is allowed to move before being forced to return to its origin")]
    public float maxRange;
    [HideInInspector]
    public bool turnedOn; // Turns on/off tethering functionality. Disabling this script means Start() doesn't run.
    // Whether or not the mob is in the process of going to its starting position
    public bool returningHome { get; private set; }
    [Tooltip("Freaks out if we tell it to stand on an EXACT spot, so introduce a margin of error")]
    public float buffer = 0.2f;

    private Vector3 startingPosition;
    private Movement movement;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
        movement = GetComponent<Movement>();
        enabled = false;
	}

    void Update () {
        if (!turnedOn)
            return;

        // Give up chasing the player (stops player from being chased by a horde of enemies around the level)
        if (!returningHome && TooFarFromHome())
            returningHome = true;

        if (returningHome)
            MoveToStartingPosition();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startingPosition, maxRange);
    }

    public bool TooFarFromHome() {
        float distanceFromStartingPoint = Vector3.Distance(transform.position, startingPosition);
        return distanceFromStartingPoint > maxRange || returningHome;
    }

    public void MoveToStartingPosition() {
        movement.Unfreeze();
        if (transform.position.x > startingPosition.x + buffer || transform.position.x < startingPosition.x - buffer) { // Need to check y
            movement.MoveTowardsPoint(startingPosition);
        } else {
            returningHome = false;
            turnedOn = false;
        }
    }
}
