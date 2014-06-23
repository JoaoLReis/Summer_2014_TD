using UnityEngine;
using System.Collections;

public class UberCamera : MonoBehaviour 
{ 
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	Transform lookAtPos;			// the position to move the camera to when using head look
	GameState game;

	void Start()
	{
		// initialising references
		standardPos = GameObject.Find ("CamPos").transform;

		game = GameObject.Find("Game").GetComponent<GameState>();

		if(GameObject.Find ("LookAtPos"))
			lookAtPos = GameObject.Find ("LookAtPos").transform;
	}
	
	void FixedUpdate ()
	{
		// if we hold Alt
		if(lookAtPos)
		{
			// lerp the camera position to the look at position, and lerp its forward direction to match 
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);
			//transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
            transform.LookAt(lookAtPos);
		}
		else
		{	
			// return the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
		}
		
	}
}
