using UnityEngine;
using System.Collections;

public class ButtonMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (Screen.width/2-100, Screen.height/2-100, 200, 30), "Start single player")) {
			Application.LoadLevel("Main");
		}

		if (GUI.Button (new Rect (Screen.width/2-100, Screen.height/2-60, 200, 30), "Exit")) {
			Application.Quit();
		}
	}
}
