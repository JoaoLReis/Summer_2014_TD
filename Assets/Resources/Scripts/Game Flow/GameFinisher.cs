using UnityEngine;
using System.Collections;

public class GameFinisher : MonoBehaviour {

    private GUISkin skin;

    void Awake ()
    {
        skin = Resources.Load("Skins/EndGame") as GUISkin;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
       // GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), endGame, "EndGame", skin.window);
    }

    private void endGame(int windowID)
    {
        //if (GUI.Button(new Rect(Screen.width / 8, Screen.height / 2 - Screen.height / 10, Screen.width / 4, Screen.height/10), "Restart", skin.button))
        //{
        //    paused = false;
        //    Time.timeScale = 1;
        //}

        //if (GUI.Button(new Rect(Screen.width / 8, Screen.height / 2 + Screen.height / 10, Screen.width / 4, Screen.height / 10), "Restart", skin.button))
        //{
        //    paused = false;
        //    Time.timeScale = 1;
        //}
    }
}
