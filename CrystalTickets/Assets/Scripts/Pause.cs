using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	bool paused = false;
	
	void Update()
	{

	}
	
	void OnGUI()
	{
		if(paused)
		{
			GUILayout.Label("Game is paused!");
			if(GUILayout.Button("Click me to unpause"))
				paused = togglePause();
			if(GUILayout.Button("Back to main menu"))
				paused = togglePause();
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
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
}

