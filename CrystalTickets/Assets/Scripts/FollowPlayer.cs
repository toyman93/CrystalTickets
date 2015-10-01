using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    // Just a note on design: the coupling between AI scripts seems like a bit of a bad smell, but at the moment it's
    // not too bad - DetectPlayer turns off EnemyPatrol, is used by ShootPlayer, and this one should only need to 
    // interact with DetectPlayer as well. Consider design changes if coupling between AI scripts becomes too tight.

    private DetectPlayer detectPlayer; // Following is triggered when the player moves out of range (detected here)
    private Pathing pathing; // Used to do the actual pathing/moving

	// Use this for initialization
	void Start () {
        detectPlayer = GetComponent<DetectPlayer>();
        pathing = GetComponent<Pathing>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
