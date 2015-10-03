using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int startingHealth = 100;

    public bool isDead { get; private set; }

    protected int currentHealth;
    protected Animator animator;

    public bool healingEnabled = true;
    public bool damageEnabled = true;

    private Movement movement;

    void Awake() {
        currentHealth = startingHealth;
    }

    // Use this for initialization
    protected virtual void Start() {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    // Returns whether removing health was successful
    public virtual bool RemoveHealth(int damage) {
        bool healthRemoved = false;

        if (damageEnabled && currentHealth > 0) {
            currentHealth -= damage;
            if (currentHealth <= 0 && !isDead)
                DestroyCharacter(); // Kill the player/mob
            healthRemoved = true;
        }

        return healthRemoved;
    }

    // Returns whether adding health was successful
    public virtual bool AddHealth(int health) {
        bool healthAdded = false;

        // Checking whether health is at max is useful for medpacs, so that they're not consumed when unnecessary
        if (healingEnabled && !(currentHealth == startingHealth)) {
            currentHealth += health;
            healthAdded = true;
        }

        return healthAdded;
    }

    public virtual void DestroyCharacter() {
        isDead = true;
        animator.SetBool(GameConstants.DeadState, true); // Play the death animation
        movement.enabled = false;
    }

}