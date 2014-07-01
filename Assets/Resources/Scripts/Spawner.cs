using UnityEngine;
using System.Collections;
using System;

public class Spawner : MonoBehaviour {

    private GlobalData data;
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;

    private UnityEngine.Object[] enemies;
    private float[] level;
    private float startTime;
    private float deltaTime;
    /*NOT SUPPOSED TO BE PUBLIC */public bool gameOn;
    private int myID;
    private int currentWave;
    private bool hasStarted;

    public float levelDuration;
    public int numWaves;
    public int timeToStart;
    
    void Awake()
    {
        startTime = 0;
        deltaTime = 0;
        gameOn = true;
        currentWave = 1;
        hasStarted = false;
        levelDuration = 180f;
        numWaves = 10;
        timeToStart = 5;
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
        level = data.getCurrentLevel();
        enemies = data.enemies;
        deltaTime = levelDuration / numWaves;
        switch (gameObject.name)
        {
            case "Spawn1":
                myID = 1;
                break;
            case "Spawn2":
                myID = 2;
                break;
            case "Spawn3":
                myID = 3;
                break;
            case "Spawn4":
                myID = 4;
                break;
            default:
                break;
        }
        pos1 = transform.position + transform.right * 1;
        pos2 = transform.position + transform.right * -1;
        pos3 = transform.position + transform.right * 3;
        pos4 = transform.position + transform.right * -3;
    }

	// Use this for initialization
	void Start () 
    {   

	}
	
    //Start countdown for first wave -> Called when all "flags" are set
    public void startCounting()
    {
        startTime = Time.time;
        gameOn = true;
    }

    private void spawn(int wave)
    {
        int numEnemies = (int)Math.Truncate(level[(wave - 1) * 4 + myID - 1]);
        int enemyid = getDecimalAsInt(level[(wave - 1) * 4 + myID - 1]);
        GameObject enemytype = (GameObject)enemies[enemyid-1];
        int aux = 1;

        for(int i = 0; i < numEnemies; i++)
        {
            switch (i%4)
            {
                case 0:
                    Instantiate(enemytype, pos1 + transform.forward *2* (int)(i / 4), transform.rotation);
                    break;
                case 1:
                    Instantiate(enemytype, pos2 + transform.forward * 2 * (int)(i / 4), transform.rotation);
                    break;
                case 2:
                    Instantiate(enemytype, pos3 + transform.forward * 2 * (int)(i / 4), transform.rotation);
                    break;
                case 3:
                    Instantiate(enemytype, pos4 + transform.forward * 2 * (int)(i / 4), transform.rotation);
                    break;
                default:
                    break;
            }
            
            aux = -aux;
        }

        currentWave++;
    }

	// Update is called once per frame
	void Update () {
	    if(!hasStarted && gameOn && (Time.time - startTime) > timeToStart)
        {
            hasStarted = true;
            startTime = Time.time;
            spawn(1);
        }

        if(hasStarted && Time.time - startTime > deltaTime)
        {
            startTime = Time.time;
            spawn(currentWave);
        }
	}

    private int getDecimalAsInt(float i)
    {
        string outPut = "0";
        if (i.ToString().Split('.').Length == 2)
        {
            outPut = i.ToString().Split('.')[1].Substring(0, i.ToString().Split('.')[1].Length);
        }
        return Convert.ToInt32(outPut);
    }
}
