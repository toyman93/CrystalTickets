using UnityEngine;
using System.Collections;

// Replace this; it was just for testing the platforms.
// See https://www.youtube.com/watch?v=Xnyb2f6Qqzg
public class TestPlayerController : MonoBehaviour {

//	//This will be our maximum speed as we will always be multiplying by 1
//	public float maxSpeed = 2f;
//	//to check ground and to have a jumpforce we can change in the editor
//	bool grounded = false;
//	public Transform groundCheck;
//	float groundRadius = 0.2f;
//	public LayerMask whatIsGround;
//	public float jumpForce = 700f;

	float moveSpeed = 0.1f;
	private Rigidbody2D rigidBody;
	bool grounded;
	float GroundDistance;
	// Use this for initialization
	void Start () {
		// Create player on start
		Player mainChar = new Player();

		// Retrieve attached rigidbody object and store
		rigidBody = GetComponent<Rigidbody2D> ();

	}
	void OnCollisionStay(Collision collisionInfo){
		grounded = true;
		print ("hello");
	}
	// FixedUpdate is called every 0.2seconds regardless of frame processing time
	void FixedUpdate () {

	}

	// Update is called once every frame
	void Update(){

		if (Input.GetKey (KeyCode.RightArrow)) {
			rigidBody.velocity = new Vector2 (2.0f, 0.0f);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rigidBody.velocity = new Vector2 (-2.0f, 0.0f);
		}
		if (grounded && Input.GetKey(KeyCode.Space)){
			print ("Space pressed");
			rigidBody.AddForce(Vector3.up * 100);
		}
		print (grounded);
	}

}
