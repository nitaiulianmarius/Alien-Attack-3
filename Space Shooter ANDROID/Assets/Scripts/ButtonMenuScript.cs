using UnityEngine;
using System.Collections;

public class ButtonMenuScript : MonoBehaviour {
	
	void OnGUI()
	{
		//while (true) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 30), "Start single player")) {
				Application.LoadLevel ("Main");
			}
		
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 30), "Start multy player")) {
				;
			}
		
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 30), "Quit")) {
				Application.Quit ();
			}
		//}
	}
}
