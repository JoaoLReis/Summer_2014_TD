using UnityEngine;
using System.Collections;

public class UberUi : MonoBehaviour {

    GameState game;

	void Start()
	{
        game = GameObject.Find("Game").GetComponent<GameState>();
	}
	
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0,0,200,60));

        GUILayout.BeginHorizontal();

        //if(GUILayout.RepeatButton("Tower"))
        //{
        //    //Enter build mode.

        //}

        GUILayout.BeginVertical();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

	void Update () 
    {
	
	}
}
