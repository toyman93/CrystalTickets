using UnityEngine;
using System.Collections;

public class EnemyHealth : Health {

    // Also note that some of these animation prefabs have components that need to be moved to the Effects sorting layer as well.
    public GameObject deathAnimation; // One of the cartoony ones

    private DetectPlayer detectPlayer;
    private float deathAnimLength;

    protected override void Start() {
        base.Start();
        detectPlayer = GetComponent<DetectPlayer>();
        deathAnimLength = deathAnimation.GetComponent<ParticleSystem>().duration;
    }

    public override void DestroyCharacter() {
        base.DestroyCharacter();
        detectPlayer.enabled = false;
        PlayDeathAnimation();
        StartCoroutine(WaitAndDestroy());
    }

    private void PlayDeathAnimation() {
        GameObject deathAnim = CFX_SpawnSystem.GetNextObject(deathAnimation);
        deathAnim.transform.position = transform.position;
    }

    // Waits until the death animation has finished before destroying the enemy
    private IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(deathAnimLength);

        // This does mean that the spritesheet death animation doesn't have time to play. Not sure whether we want it or not.
        Destroy(gameObject);
    }
}
