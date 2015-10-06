using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private Animator animator;
	
	public static bool activateDoor = false;
	
	public ItemScript.ItemTypes currentItem = ItemScript.ItemTypes.Pistol;
	public Joystick.ItemTypes currentmovement = Joystick.ItemTypes.empty;
	public bool isPause = false;
	
	private Rigidbody2D rigidBody;
	
	// Shooting stuff. gun = position of the gun; where bullets will start from.
	public GameObject bulletPrefab, gun, grenadePrefab;
	public float firingIntervalInSeconds = 0.1f; // How often can we fire a bullet
	private float timeLastFired;
	
	PlayerStatsUI statsUI;
	
	private Movement movement;
	
	// Controls lever conditions
	public GameObject lever;
	public Sprite leverOn, leverOff;
	
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
	
	// TODO: Move this. Should be in own script, not in player controller
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "lever") {
			if (activateDoor == false) {
				print("Switch On");
				activateDoor = true;
				lever.GetComponent<SpriteRenderer>().sprite = leverOn;
			} else {
				print("Switch Off");
				activateDoor = false;
				lever.GetComponent<SpriteRenderer>().sprite = leverOff;
			}
		}
	}
}