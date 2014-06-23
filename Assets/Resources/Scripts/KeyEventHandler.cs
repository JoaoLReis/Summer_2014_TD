using UnityEngine;
using System.Collections;

public class KeyEventHandler : MonoBehaviour {

    GameState game;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<GameState>();
    }
	
	void Update () 
    {
        if (Input.GetKeyDown("b"))
            game.switch_BM();
	}
}
