using UnityEngine;
using System.Collections;

// Script that records starting position and allows the mob to move between points in the world
// Give it a better name if you can think of one...
public class Pathing : MonoBehaviour {

    // How far away from the starting point the mob is allowed to move before being forced to return to its origin
    public float maxRange; 

    private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position;
	}

    void Update () {
        float distanceFromStartingPoint = Vector3.Distance(transform.position, startingPosition);

        // Give up chasing the player (stops player from being chased by a horde of enemies around the level)
        if (distanceFromStartingPoint > maxRange)
            MoveToStartingPosition();
    }

    public void MoveToStartingPosition() {
        if (transform.position != startingPosition)
            MoveToPoint(startingPosition);
    }

    public void MoveToPoint(Vector2 point) {
        // This is going to be complicated to write...
    }
}
