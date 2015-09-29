using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Class for any constants (tags, etc.) that are used throughout the game. Also used for documenting usages.
public static class GameConstants {

    /* --- TAGS --- */
    public const String GunTag = "Gun"; // Child objects of a mob/player with this tag are used to define where a bullet should move from.
    public const String EnemyTag = "Enemy";
    public const String PlayerTag = "Player"; // Anything with this tag should be treated as part of the player
    public const String PlatformEdgeTag = "PlatformEdge"; // When an enemy collides with an object with this tag, it'll treat it like a platform edge and turn around.

    /* --- ANIMATION PARAMETERS --- */
    public const String RunState = "Run"; // Set to false to go to idle state
    public const String JumpState = "Jump";
    public const String ShootState = "Shoot";
    public const String DeadState = "Dead";

    /* -- LAYERS (Not sorting layers) -- */
    public const String EnemyLayer = "Enemy";
}
