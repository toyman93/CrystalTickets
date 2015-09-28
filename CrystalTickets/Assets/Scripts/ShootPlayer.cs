using UnityEngine;
using System.Collections;

public class ShootPlayer : MonoBehaviour {

    public int testHealth = 5;
    public double firingIntervalInSeconds = 0.2f;
    public GameObject enemyBulletPrefab;

    private float timeLastFired;
    private DetectPlayer detectPlayer;
    private GameObject gunObject;
    private Animator animator;

    void Start() {
        detectPlayer = GetComponent<DetectPlayer>();
        animator = GetComponent<Animator>();

        // Gets the child GameObject that represents the gun's position
        foreach (Transform child in transform)
            if (child.CompareTag(GameConstants.GunTag))
                gunObject = child.gameObject;
    }

    void Update() {
        Vector2 gunPosition = gameObject.transform.position;

        float secondsSinceLastFired = Time.time - timeLastFired;

        // Only fire once every firingIntervalInSeconds seconds
        if (secondsSinceLastFired > firingIntervalInSeconds) {
            timeLastFired = Time.time;

            // Can we actually hit the player if we shoot?
            if (detectPlayer.IsPlayerInLineOfSight(gunPosition)) {
                FireBullet(gunPosition, detectPlayer.directionToPlayer);
            } else {
                StopFiring();
            }
        }
    }

    private void FireBullet(Vector2 position, Vector2 direction) {
        Vector2 gunPosition = gunObject.transform.position;
        GameObject bullet = (GameObject) Instantiate(enemyBulletPrefab, gunPosition, Quaternion.identity);
        bullet.GetComponent<Bullet>().FireInDirection(direction);
        animator.SetBool(GameConstants.ShootState, true);
    }

    private void StopFiring() {
        animator.SetBool(GameConstants.ShootState, false);
    }

    // Super-basic health system for testing - remove and replace.
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("TestDamage")) {
            if (testHealth-- == 0)
                Destroy(gameObject);
        }
    }
}