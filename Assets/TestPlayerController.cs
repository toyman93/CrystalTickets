using UnityEngine;
using System.Collections;

// See https://www.youtube.com/watch?v=Xnyb2f6Qqzg
public class ControllerFromTutorial : MonoBehaviour {

	//This will be our maximum speed as we will always be multiplying by 1
	public float maxSpeed = 2f;
	//to check ground and to have a jumpforce we can change in the editor
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		//set our grounded bool
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis ("Horizontal");//Gives us of one if we are moving via the arrow keys
		//move our Players rigidbody
		rigidBody.velocity = new Vector3 (move * maxSpeed, rigidBody.velocity.y);	
	}

	void Update(){
		//if we are on the ground and the space bar was pressed, change our ground state and add an upward force
		if(grounded && Input.GetKeyDown (KeyCode.Space)){
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0,jumpForce));
		}
	}
}
