using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	private PlayerController control;
	private Movement movement;

	// Use this for initialization
	void Start () {

		control = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
		movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void aaa(){
		Debug.Log ("jump on mouse down");
	}
}
