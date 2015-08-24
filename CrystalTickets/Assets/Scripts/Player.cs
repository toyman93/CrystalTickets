using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	int speed = 1;
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
}
