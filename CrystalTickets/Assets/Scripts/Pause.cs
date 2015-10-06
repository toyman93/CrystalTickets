using System.Collections;
using UnityEngine;

public class Pause : MonoBehaviour {

	public bool paused = false;
	public PlayerController playercontroller;
	
	void Update()
	{

	}
	
	void OnGUI()
	{
		if(paused)
		{
			if (GUI.Button ((new Rect (Screen.width*0.4f, Screen.height*0.1f, Screen.width*0.2f,Screen.height*0.2f)), "Resume")) {

			paused = togglePause();
			
			}

			if (GUI.Button ((new Rect (Screen.width * 0.4f, Screen.height * 0.35f, Screen.width * 0.2f, Screen.height * 0.2f)), "Restart")){
				Application.LoadLevel("LevelOne");
				paused = togglePause();
			}

			if (GUI.Button ((new Rect (Screen.width*0.4f, Screen.height*0.6f, Screen.width*0.2f,Screen.height*0.2f)), "Back to Main Menu")) {
				Application.LoadLevel("GameStartScene");
				paused = togglePause();
			}


		}
	}

	public void OnMouseDown(){
		paused = togglePause();
	}

	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			playercontroller.isPause = false;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			playercontroller.isPause = true;
			return(true);    
		}
	}
}

