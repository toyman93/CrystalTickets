using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable {

    public int startingHealth = 100;
    private int currentHealth;
    private bool isDead;

    public Slider healthBar;

    // Required when the player dies (change state / disable)
    private PlayerController movementController;
    private Animator animator;

    void Awake() {
        currentHealth = startingHealth;
        isDead = false;
    }

	void Start () {
        movementController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {
	
	}

    public void damage(int damage) {
        currentHealth -= damage;
        
        // Update the UI
        if (healthBar != null)
            healthBar.value = Math.Max(currentHealth, 0); // Don't let the health bar drop below 0

        // Kill the player
        if (currentHealth <= 0 && !isDead)
            destroy();
    }

    public void heal(int health) {
        currentHealth += health;

        if (healthBar != null)
            healthBar.value = Math.Min(currentHealth, startingHealth); // Health shouldn't exceed the max
    }

    public void destroy() {
        isDead = true;
        animator.SetBool("Dead", true); // Play the death animation
        movementController.enabled = false; // Turn off the controls
    }
}
