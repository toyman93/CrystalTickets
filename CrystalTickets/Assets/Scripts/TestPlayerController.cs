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

	float moveSpeed = 1.0f;
	private Rigidbody2D rigidBody;
	float movex = 0.0f;
	float movey = 0.0f;

	float distGround = 0.0f;
	// Use this for initialization
	void Start () {
		// Create player on start
		Player mainChar = new Player();

		// Retrieve attached rigidbody object and store
		rigidBody = GetComponent<Rigidbody2D> ();

		// Distance from ground from ray casting
		distGround = GetComponent<Collider>().bounds.extents.y;
	}

	// FixedUpdate is called every 0.2seconds regardless of frame processing time
	void FixedUpdate () {
		print ("h" + Time.deltaTime);
		movex = Input.GetAxis ("Horizontal");
		movey = Input.GetAxis ("Vertical");
		
		rigidBody.velocity = new Vector2 (movex * moveSpeed, rigidBody.velocity.y);
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rigidBody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
		}
	}

	// Update is called once every frame
	void Update(){
		print (Time.deltaTime);
	}

}
