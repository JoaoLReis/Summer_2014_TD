using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerOne : Spawner {

    private GameManager gManager;
    private Level level;

    private int numSpawners;
    private int numWaves;

    //Positions of spawning
    private Transform[] spawns;

    Vector3[] pos1;
    Vector3[] pos2;
    Vector3[] pos3;
    Vector3[] pos4;        

    private float deltaTime;
    private int currentWave;

    private float levelDuration;
    private int timeToStart;

    // Spawning list
    private List<GameObject>[] enemyList;
    private ArrayList[] indexList;

    void Awake()
    {
        currentWave = 0;
        levelDuration = 300f;
        timeToStart = 5;
    }

    void Start()
    {
        gManager = GetComponent<GameManager>();
        level = GetComponent<Level>();

        numSpawners = level.getNumSpawners();
        numWaves = level.getNumWaves();
        deltaTime = levelDuration / numWaves;

        UnityEngine.Object[] spawners = GameObject.FindGameObjectsWithTag("Spawn");
        spawns = new Transform[numSpawners];
        spawns[0] = ((GameObject)spawners[0]).transform;
        spawns[1] = ((GameObject)spawners[1]).transform;
        spawns[2] = ((GameObject)spawners[2]).transform;
        spawns[3] = ((GameObject)spawners[3]).transform;

        pos1 = new Vector3[numSpawners];
        pos2 = new Vector3[numSpawners];
        pos3 = new Vector3[numSpawners];
        pos4 = new Vector3[numSpawners];

        for (int i = 0; i < numSpawners; i++)
        {
            pos1[i] = spawns[i].position + spawns[i].right * 1;
            pos2[i] = spawns[i].position + spawns[i].right * -1;
            pos3[i] = spawns[i].position + spawns[i].right * 3;
            pos4[i] = spawns[i].position + spawns[i].right * -3;
        }
    }

    public override void startUp(List<GameObject>[] enemyList, ArrayList[] indexList)
    {
        this.enemyList = enemyList;
        this.indexList = accumulateList(indexList);
    }

    private ArrayList[] accumulateList(ArrayList[] array)
    {
        ArrayList[] aux = array;
        for (int j = 0; j < numSpawners; j++)
        {
            aux[j][0] = (int)aux[j][0];
            for (int i = 1; i < aux[j].Count; i++)
            {
                aux[j][i] = (int)aux[j][i] + (int)aux[j][i - 1];
            }
        }
        return aux;
    }

    public override void spawn()
    {
        for (int i = 0; i < numWaves; i++)
        {
            Invoke("spawnWaves", timeToStart + i*deltaTime);            
        }
    }

    private void spawnWaves()
    {
        for(int i = 0; i< enemyList.Length; i++)
        {
            StartCoroutine("spawnerSpawn", i);
        }
        currentWave++;
        gManager.increaseWave();
    }

    private IEnumerator spawnerSpawn(int spawner)
    {
        int numEnemies = currentWave == 0 ? (int)indexList[spawner][0] : (int)indexList[spawner][currentWave] - (int)indexList[spawner][currentWave - 1];
        int startIndex = currentWave == 0 ? 0 : (int)indexList[spawner][currentWave - 1];
        
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject toSpawn = enemyList[spawner].Find(x => enemyList[spawner].IndexOf(x) == startIndex+i);
            switch (i % 4)
            {
                case 0:
                    toSpawn.transform.position = pos1[spawner] + spawns[spawner].forward * 2 * (int)(i / 4);
                    toSpawn.transform.rotation = spawns[spawner].transform.rotation;
                    break;
                case 1:
                    toSpawn.transform.position = pos2[spawner] + spawns[spawner].forward * 2 * (int)(i / 4);
                    toSpawn.transform.rotation = spawns[spawner].transform.rotation;
                    break;
                case 2:
                    toSpawn.transform.position = pos3[spawner] + spawns[spawner].forward * 2 * (int)(i / 4);
                    toSpawn.transform.rotation = spawns[spawner].transform.rotation;
                    break;
                case 3:
                    toSpawn.transform.position = pos4[spawner] + spawns[spawner].forward * 2 * (int)(i / 4);
                    toSpawn.transform.rotation = spawns[spawner].transform.rotation;
                    break;
                default:
                    break;
            }
            toSpawn.SetActive(true);
            toSpawn.GetComponent<MoveTowardsObjective>().startMovement();
            yield return new WaitForSeconds(toSpawn.GetComponent<NavMeshAgent>().speed/5.0f);
        }
        
    }
}
