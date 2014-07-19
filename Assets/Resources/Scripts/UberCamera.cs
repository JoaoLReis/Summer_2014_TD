using UnityEngine;
using System.Collections;

public class UberCamera : MonoBehaviour 
{ 
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
    public bool lookForward = true;
    public bool jumping = false;

    private bool lookatbuild = false;

    Transform selectedHex;
    Transform standardTransform;			// the usual position for the camera, specified by a transform in the game when moving forward
    Transform lookAtTransform;			// the position to move the camera to when using head look when moving forward
    Transform secondaryTransform;           // the secondary position for the camera when the character is moving backwards
    Transform secondaryLookAtTransform;                 // the secondary lookat position for the camera when teh character is moving backwards
	GameState game;

	void Start()
	{
		// initialising references
        if (GameObject.Find("CamPos"))
        {
            standardTransform = GameObject.Find("CamPos").transform;
            secondaryTransform = GameObject.Find("SecondaryCamPos").transform;
        }
		    

        if (GameObject.Find("LookAtPos"))
        {
            lookAtTransform = GameObject.Find("LookAtPos").transform;
            secondaryLookAtTransform = GameObject.Find("SecondaryLookAtPos").transform;
        }

		game = GameObject.Find("Game").GetComponent<GameState>();
	}

	void FixedUpdate ()
	{
        if (!lookatbuild)
		{
            if (lookForward)
            {
                // lerp the camera position to the look at position, and lerp its forward direction to match 
                transform.position = Vector3.Lerp(transform.position, standardTransform.position, Time.deltaTime * smooth);
                //transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
                if(!jumping)
                {
                    Quaternion targetRotation = Quaternion.LookRotation((lookAtTransform.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 10.0f);
                }
                else
                {
                    Quaternion targetRotation = Quaternion.LookRotation((lookAtTransform.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 6.0f);
                }
            }
            else
            {
                // lerp the camera position 
                transform.position = Vector3.Lerp(transform.position, secondaryTransform.position, Time.deltaTime * smooth);
                if(!jumping)
                {
                    Quaternion targetRotation = Quaternion.LookRotation((secondaryLookAtTransform.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 10.0f);
                }
                else
                {
                    Quaternion targetRotation = Quaternion.LookRotation((secondaryLookAtTransform.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 6.0f);
                }
            }
            //transform.LookAt(lookAtPos);
		}
		else
		{
            if (lookForward)
            {
                // lerp the camera position to the look at position, and lerp its forward direction to match 
                transform.position = Vector3.Lerp(transform.position, standardTransform.position, Time.deltaTime * smooth);
                //transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
                if (!jumping)
                {
                    Quaternion targetRotation = Quaternion.LookRotation((selectedHex.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 10.0f);
                }
                else
                {
                    Quaternion targetRotation = Quaternion.LookRotation((selectedHex.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 6.0f);
                }
            }
            else
            {
                // lerp the camera position 
                transform.position = Vector3.Lerp(transform.position, secondaryTransform.position, Time.deltaTime * smooth);
                if (!jumping)
                {
                    Quaternion targetRotation = Quaternion.LookRotation((selectedHex.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 10.0f);
                }
                else
                {
                    Quaternion targetRotation = Quaternion.LookRotation((selectedHex.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth * 6.0f);
                }
            }
		}	
	}

    public void SetSelectedHex(Transform hex)
    {
        selectedHex = hex;
    }

    public void SetLookatBuild(bool val)
    {
        lookatbuild = val;
    }
}
