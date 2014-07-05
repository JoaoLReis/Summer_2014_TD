using UnityEngine;
using System.Collections;

public class LookAtEnemy : MonoBehaviour {

    Transform target;

	// Use this for initialization
	void Start () {
	    
	}
	
    public void setActiveTarget(Transform t)
    {
        target = t;
        transform.LookAt(target);
    }

    public void reset(Vector3 pos)
    {
        transform.LookAt(pos);
    }

	// Update is called once per frame
	void Update () {
        transform.LookAt(target);
	}
}
