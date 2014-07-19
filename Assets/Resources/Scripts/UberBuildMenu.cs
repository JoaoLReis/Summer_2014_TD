using UnityEngine;
using System.Collections;

public class UberBuildMenu : MonoBehaviour {

    private Transform player;
    private bool activated;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if(activated)
        {
            Camera.main.GetComponent<UberCamera>().SetLookatBuild(true);
            Camera.main.GetComponent<UberCamera>().SetSelectedHex(transform);
        }
        transform.rotation = Quaternion.LookRotation((Camera.main.transform.position - transform.position).normalized);
	}

    public void SetActivated(bool val)
    {
        activated = val;
    }
}
