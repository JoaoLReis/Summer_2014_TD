using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTowardsObjective : MonoBehaviour {

    //private NavMeshAgent agent;
    public float speed = 2.0f;
    private GameManager gManager;
    private HexTileMap tileMap;

    private bool reloadMov;

    private Stack<Hex> path;
    private Hex target;

    void Awake()
    {
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        tileMap = GameObject.Find("HexEditor").GetComponent<HexTileMap>();
    }

	// Use this for initialization
	void Start () 
    {
        reloadMov = true;
        path = new Stack<Hex>();
        target = null;
	}

    void reloadPath()
    {
        Stack<Hex> tmpPath = tileMap.getPathToRelay(transform.position, gManager.getRelay().transform.position);
        if (tmpPath != null)
        {
            path = tmpPath;
            //Inits the target with the first hex.
            path.Pop();
            target = path.Pop();
        }
        else
        {
            Debug.Log("sticking with previous path");
        }
    }

    /*IEnumerator movementControl()
    {
        do
        {
            if(tileMap.researchBestPath)
            {
                reloadPath();
            }
            
        } while (true);
    }*/

    void Update()
    {
        //Gets a path to the relay.
        if (reloadMov)
        {
            reloadPath();
            reloadMov = false;
            //reloadMov = false;
        } 
        var step = speed * Time.deltaTime;
        if(target != null)
        {
            //Rotates on only one axis
            Vector3 point = target.buildPosition;
            point.y = transform.position.y;
            transform.LookAt(point);

            transform.position = Vector3.MoveTowards(transform.position, target.buildPosition, step);
            if (Vector3.Distance(transform.position, target.buildPosition) <= 0.01f)
            {
                if(path.Count > 0)
                {
                    Debug.Log("new target");
                    target = path.Pop();
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = target.buildPosition;
                } 
            }
        }
    }
	
    public void startMovement()
    {
        /*reloadMov = true;
        Debug.Log("start movement");*/
        //StartCoroutine("movementControl");

        //agent.SetDestination(gManager.getRelay().transform.position);
    }
}