using UnityEngine;
using System.Collections;

public class UberCamera : MonoBehaviour {

    GameState game;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<GameState>();
    }
	
	void Update () {
	
	}
}
