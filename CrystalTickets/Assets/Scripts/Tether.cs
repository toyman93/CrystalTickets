using UnityEngine;
using System.Collections;

// Script that records starting position and allows the mob to move between points in the world
[RequireComponent (typeof (Movement))]
public class Tether : MonoBehaviour {

    [Tooltip("How far away from the starting point the mob is allowed to move before being forced to return to its origin")]
    public float maxRange;

    // Whether or not the mob is in the process of going to its starting position
    public bool returningHome { get; private set; } 

    private Vector3 startingPosition;
    private Movement movement;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
        movement = GetComponent<Movement>();
	}

    void Update () {
        // Give up chasing the player (stops player from being chased by a horde of enemies around the level)
        if (!returningHome && TooFarFromHome())
            returningHome = true;

        if (returningHome)
            MoveToStartingPosition();
    }

    public bool TooFarFromHome() {
        float distanceFromStartingPoint = Vector3.Distance(transform.position, startingPosition);
        return distanceFromStartingPoint > maxRange;
    }

    public void MoveToStartingPosition() {
        if (transform.position != startingPosition) {
            movement.MoveTowardsPoint(startingPosition);
        } else {
            returningHome = false;
            enabled = false; // Disable this script - not intended to be on by default
        }
    }
}
