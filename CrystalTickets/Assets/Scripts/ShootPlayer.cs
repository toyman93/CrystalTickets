using UnityEngine;
using System.Collections;

public class ShootPlayer : MonoBehaviour {

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
        Vector2 gunPosition = gunObject.transform.position;

        float secondsSinceLastFired = Time.time - timeLastFired;

        // Only fire once every firingIntervalInSeconds seconds
        if (secondsSinceLastFired > firingIntervalInSeconds) {
            timeLastFired = Time.time;

            // Can we actually hit the player if we shoot?
            if (detectPlayer.PlayerVisible()) {
                FireBullet(gunPosition, detectPlayer.GetDirectionToPlayer());
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

}