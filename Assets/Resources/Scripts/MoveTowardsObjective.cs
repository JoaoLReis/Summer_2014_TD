using UnityEngine;
using System.Collections;

public class MoveTowardsObjective : MonoBehaviour {

    private NavMeshAgent agent;
    private GlobalData data;

    private bool startMov; 

    void Awake()
    {
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
        agent = GetComponent<NavMeshAgent>();
        startMov = true;
    }

	// Use this for initialization
	void Start () {
	}
	
    private void startMovement()
    {
        agent.SetDestination(data.getRelay().transform.position);
    }

	// Update is called once per frame
	void Update () {
	    if(startMov)
        {
            startMov = false;
            startMovement();
        }
	}
}
