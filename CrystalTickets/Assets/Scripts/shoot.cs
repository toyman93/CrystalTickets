using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour {

	private Animator animator;
	public GameObject bulletPrefab, gun;
	private PlayerController control;
	private Movement movement;
	private float timeLastFired;
	public float firingIntervalInSeconds = 0.1f;

	void Awake() {
		timeLastFired = -firingIntervalInSeconds;
	}
	
	// Use this for initialization
	void Start () {
		
		control = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
		movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			Debug.Log ("shoot");
			
		}

	}
}
