using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Set initial values
    public string playerName = "John";
    public int score = 0;
    public int health = 100;
    public bool isHidden = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			print ("Up key is pressed");
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			print ("Down key is pressed");
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			print ("Left key is pressed");
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			print ("Right key is pressed");
		}
	}

    // Returns the player GameObject based on the player tag.
    public static GameObject GetPlayerGameObject() {
        // This'll fail if there are no objects or multiple objects with the player tag
        return GameObject.FindGameObjectWithTag(GameConstants.PlayerTag);
    }
}
