using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;

	public static bool activateDoor = false;

	public ItemScript.ItemTypes currentItem = ItemScript.ItemTypes.Pistol;

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

    void FixedUpdate() {
        float move = Input.GetAxis("Horizontal");//Gives us of one if we are moving via the arrow keys
                                                 //move our Players rigidbody

        rigidBody.velocity = new Vector3(move * movement.speed, rigidBody.velocity.y);
    }

    void Update() {

        if (Input.GetButtonDown("Jump")) 
            movement.Jump();

        /* Putting the animation transitions here seems to make it more responsive than when it's in FixedUpdate(),
           but this could use a bit more testing. */

        float move = Input.GetAxis("Horizontal");

        // Update the animations to show running if the player is moving
        bool isRunning = move == 0 ? false : true;
        animator.SetBool(GameConstants.RunState, isRunning);

        // Update animations to reflect which way the player is moving
        bool changedDirection = move > 0 && !movement.isFacingRight || move < 0 && movement.isFacingRight;
        if (changedDirection)
            movement.Flip();

        // Only shoot a bullet if a sane amount of time has passed
        float secondsSinceLastFired = Time.time - timeLastFired;

        // Shooting - doesn't work if you just set 'Shoot' to the value of Input.GetKeyDown(KeyCode.Q) (hence second condition)
        if (Input.GetButton("Fire") && secondsSinceLastFired > firingIntervalInSeconds) {
            timeLastFired = Time.time;
            animator.SetBool(GameConstants.ShootState, true);
            FireBullet(gun.transform.position);
        }
        if (Input.GetButtonUp("Fire"))
            animator.SetBool(GameConstants.ShootState, false);
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
