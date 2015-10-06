using UnityEngine;
using System.Collections;

// Class for representing a basic 'node' in the environment that enemies can interact with
// Note that collisions with nodes are only turned on for enemies (via Physics2D's layer settings)
[RequireComponent(typeof (Collider2D))]
public abstract class Node : MonoBehaviour {

    public bool nodeEnabled = true;

    private Collider2D nodeCollider;

    void Start () {
        nodeCollider = GetComponent<Collider2D>();
    }

    void Update () {
        nodeCollider.enabled = nodeEnabled;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // Enemy movement script (allows this node to control enemy movement)
        GameObject enemy = collider.gameObject;
        FollowPlayer follow = enemy.GetComponent<FollowPlayer>();
        if (follow != null)
            if (ShouldTrigger(follow))
                Trigger(enemy.GetComponent<Movement>());
    }

    // Check conditions for triggering here. Should be overridden in subclasses.
    protected virtual bool ShouldTrigger(FollowPlayer follow) {
        return true;
    }

    protected abstract void Trigger(Movement movement);
}
