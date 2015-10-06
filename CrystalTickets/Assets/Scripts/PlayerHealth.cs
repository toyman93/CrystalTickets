using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : Health {

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [Tooltip("Time in seconds to wait after the player dies before returning to the main menu.")]
    public float delayAfterDeath = 4f;

    // Required when the player dies (change state / disable)
    private PlayerController movementController;
    private SpriteRenderer firstHeart;
    private SpriteRenderer secondHeart;
    private SpriteRenderer thirdHeart;

	protected override void Start () {
        base.Start();
        movementController = GetComponent<PlayerController>();
        SetHeartReferences();
	}

    void Update () {
        // Set the hearts in the health panel
        SetHeartsFromHealth();

        if (isDead)
            StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu() {
        yield return new WaitForSeconds(delayAfterDeath);
        Application.LoadLevel("GameStartScene");
    }

    // Gets references to the sprite renderers for the health hearts in the UI so that we can change the sprites
    private void SetHeartReferences () {
        GameObject healthPanel = GameObject.FindGameObjectWithTag(GameConstants.HealthPanelTag);
        firstHeart = healthPanel.transform.GetChild(0).GetComponent<SpriteRenderer>();
        secondHeart = healthPanel.transform.GetChild(1).GetComponent<SpriteRenderer>();
        thirdHeart = healthPanel.transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    public override bool RemoveHealth(int damage) {
        bool damaged = base.RemoveHealth(damage);
        return damaged;
    }

    public override bool AddHealth(int health) {
        bool healed = base.AddHealth(health);
        return healed;
    }

    public override void DestroyCharacter() {
        base.DestroyCharacter();
        movementController.enabled = false; // Turn off the controls
    }

    // Ugly! 
    private void SetHeartsFromHealth () {
        // Amount of health equal to half a heart (there are 3 hearts)
        float halfHeartValue = startingHealth / 6;

        if (currentHealth <= 0) {
            SetHearts(emptyHeart, emptyHeart, emptyHeart);
        } else if (currentHealth > 0 && currentHealth <= halfHeartValue) {
            SetHearts(halfHeart, emptyHeart, emptyHeart);
        } else if (currentHealth <= halfHeartValue * 2) {
            SetHearts(fullHeart, emptyHeart, emptyHeart);
        } else if (currentHealth <= halfHeartValue * 3) {
            SetHearts(fullHeart, halfHeart, emptyHeart);
        } else if (currentHealth <= halfHeartValue * 4) {
            SetHearts(fullHeart, fullHeart, emptyHeart);
        } else if (currentHealth <= halfHeartValue * 5) {
            SetHearts(fullHeart, fullHeart, halfHeart);
        } else if (currentHealth <= halfHeartValue * 6) {
            SetHearts(fullHeart, fullHeart, fullHeart);
        } 
    }

    private void SetHearts(Sprite first, Sprite second, Sprite third) {
        firstHeart.sprite = first;
        secondHeart.sprite = second;
        thirdHeart.sprite = third;
    }
}
