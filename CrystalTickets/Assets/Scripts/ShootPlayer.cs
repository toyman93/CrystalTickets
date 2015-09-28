using UnityEngine;
using System.Collections;

public class ShootPlayer : MonoBehaviour {

    public int testHealth = 5;

    private DetectPlayer detectPlayer;
    public GameObject enemyBulletPrefab;

    private GameObject gunObject;

    void Start() {
        detectPlayer = GetComponent<DetectPlayer>();

        // Gets the child GameObject that represents the gun's position
        foreach (Transform child in transform)
            if (child.CompareTag(GameConstants.GunTag))
                gunObject = child.gameObject;
    }

    void Update() {
        Vector2 gunPosition = gameObject.transform.position;

        // Can we actually hit the player if we shoot?
        if (detectPlayer.IsPlayerInLineOfSight(gunPosition))
            FireBullet(gunPosition, detectPlayer.directionToPlayer);
    }

    private void FireBullet(Vector3 position, Vector3 direction) {
        Vector3 gunPosition = gunObject.transform.position;
        GameObject bullet = (GameObject) Instantiate(enemyBulletPrefab, gunPosition, Quaternion.identity);
        // Need a delay here
        bullet.GetComponent<Bullet>().FireInDirection(direction);
    }

    // Super-basic health system for testing - remove and replace.
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("TestDamage")) {
            if (testHealth-- == 0)
                Destroy(gameObject);
        }
    }
}