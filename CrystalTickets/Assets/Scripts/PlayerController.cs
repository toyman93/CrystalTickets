using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	private Animator animator;
	
	public static bool activateDoor = false;
	
	public ItemScript.ItemTypes currentItem = ItemScript.ItemTypes.Pistol;
	
	public List<Joystick.ItemTypes> movementPressed; // Array to allow concurrent input

	public Joystick.ItemTypes currentmovement = Joystick.ItemTypes.empty;
	public bool isPause = false;
	
	private Rigidbody2D rigidBody;
	
	// Shooting stuff. gun = position of the gun; where bullets will start from.
	public GameObject bulletPrefab, gun, grenadePrefab;
	public float firingIntervalInSeconds = 0.1f; // How often can we fire a bullet
	private float timeLastFired;
	
	PlayerStatsUI statsUI;
	
	private Movement movement;
	
	void Awake() {
		timeLastFired = -firingIntervalInSeconds;
	}
	
	void Start() {
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		movement = GetComponent<Movement>();
		timeLastFired = -firingIntervalInSeconds;
		statsUI = GetComponent<PlayerStatsUI>();
	}
	
	void Update() {
		
		// Only shoot a bullet if a sane amount of time has passed
		float secondsSinceLastFired = Time.time - timeLastFired;

		// Handle item click with actions
		if (this.currentmovement == Joystick.ItemTypes.jump) {
			movement.Jump();
		}
		if (this.currentmovement == Joystick.ItemTypes.shoot && secondsSinceLastFired > firingIntervalInSeconds) {
			Debug.Log ("shoot");
			timeLastFired = Time.time;
			animator.SetBool(GameConstants.ShootState, true);
			FireBullet(gun.transform.position);
		} else {
			animator.SetBool(GameConstants.ShootState, false);
		}
		if (this.currentmovement == Joystick.ItemTypes.left) {
			movement.MoveLeft ();
			animator.SetBool(GameConstants.RunState, true);
		}
		if (this.currentmovement == Joystick.ItemTypes.right) {
			movement.MoveRight ();
			animator.SetBool (GameConstants.RunState, true);
		}
		if (this.currentmovement == Joystick.ItemTypes.empty) {
			animator.SetBool (GameConstants.RunState, false);
		}

		// Input key for testing purpose
		float move = Input.GetAxis("Horizontal");//Gives us of one if we are moving via the arrow keys
		//move our Players rigidbody
		
		rigidBody.velocity = new Vector3(move * movement.speed, rigidBody.velocity.y);
	}
	
	// This should probably be elsewhere. Enemies can reuse this too.
	private void FireBullet(Vector3 position) {
		// Bullet script in prefab should take care of actually moving the bullet once it's instantiated...
		
		GameObject prefab = bulletPrefab;
		
		if (this.currentItem == ItemScript.ItemTypes.Grenade) {
			prefab = grenadePrefab;
			GameObject bullet = (GameObject)Instantiate (prefab, position, Quaternion.identity);
			bullet.GetComponent<Grenade>().Fire(movement.isFacingRight); // ... but we need to tell it which way to move
		} else {
			GameObject bullet = (GameObject) Instantiate(prefab, position, Quaternion.identity);
			bullet.GetComponent<Bullet>().Fire(movement.isFacingRight); // ... but we need to tell it which way to move
		}
	}

}