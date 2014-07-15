using UnityEngine;
using System.Collections;

public class MoveTowardsObjective : MonoBehaviour {

    private NavMeshAgent agent;
    private GameManager gManager;

    private bool startMov; 

    void Awake()
    {
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        startMov = true;
    }

	// Use this for initialization
	void Start () {
	}
	
    public void startMovement()
    {
        agent.SetDestination(gManager.getRelay().transform.position);
    }

}
