using UnityEngine;
using System.Collections;

public class moveLeft : MonoBehaviour {

	private PlayerController control;
	private Movement movement;
	private Rigidbody2D rigidBody;
	
	// Use this for initialization
	void Start () {
		
		control = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
		movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
		rigidBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
		
	}

	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			Debug.Log ("move left");
			float move = -1;
			rigidBody.velocity = new Vector3(move * movement.speed, rigidBody.velocity.y);
		}
	
	}
	
}
