using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerOne : Spawner {

    private const int numSpawners = 4;
    private const int numWaves = 10;

    //Positions of spawning
    private Transform spawn1;
    private Transform spawn2;
    private Transform spawn3;
    private Transform spawn4;

    private int[] prevIndex;

    private float deltaTime;
    private int currentWave;
    private bool hasStarted;

    private float levelDuration;
    private int timeToStart;

    // Spawning list
    private List<GameObject>[] enemyList;
    private ArrayList[] indexList;

    void Awake()
    {
    //    indexList = new ArrayList[numSpawners];
    //    for (int i = 0; i < numSpawners; i++)
    //    {
    //        indexList[i] = new ArrayList();
    //    }
        prevIndex = new int[numSpawners]{0, 0, 0, 0};
        currentWave = 0;
        hasStarted = false;
        levelDuration = 300f;
        timeToStart = 5;
        deltaTime = levelDuration / numWaves;
        UnityEngine.Object[] spawners = GameObject.FindGameObjectsWithTag("Spawn");
        spawn1 = ((GameObject)spawners[0]).transform;
        spawn2 = ((GameObject)spawners[1]).transform;
        spawn3 = ((GameObject)spawners[2]).transform;
        spawn4 = ((GameObject)spawners[3]).transform;
    }

    public override void startUp(List<GameObject>[] enemyList, ArrayList[] indexList)
    {
        this.enemyList = enemyList;
        Debug.Log("First");
        foreach (ArrayList i in indexList)
        {
            foreach (int item in i)
            {
                Debug.Log(item);
            }
        }
        this.indexList = accumulateList(indexList);
        Debug.Log("Second");
        foreach (ArrayList i in indexList)
        {
            foreach (int item in i)
            {
                Debug.Log(item);
            }
        }
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
            spawnerSpawn(i);
        }
        currentWave++;
    }

    private void spawnerSpawn(int spawner)
    {
        Debug.Log("SpawnerSpawn: " + spawner);
        int numEnemies = currentWave == 0 ? (int)indexList[spawner][0] : (int)indexList[spawner][currentWave] - (int)indexList[spawner][currentWave - 1];
        int startIndex = currentWave == 0 ? 0 : (int)indexList[spawner][currentWave - 1];
        Debug.Log("numEnemies: " + numEnemies);
        Debug.Log("startIndex: " + startIndex);
        
        Vector3 pos1;
        Vector3 pos2;
        Vector3 pos3;
        Vector3 pos4;

        
        switch (spawner)
        {
            case 0:
                pos1 = spawn1.position + spawn1.right * 1;
                pos2 = spawn1.position + spawn1.right * -1;
                pos3 = spawn1.position + spawn1.right * 3;
                pos4 = spawn1.position + spawn1.right * -3;
                for (int i = 0; i < numEnemies; i++)
                {
                    GameObject toSpawn = enemyList[spawner].Find(x => enemyList[spawner].IndexOf(x) == startIndex+i);
                    switch (i % 4)
                    {
                        case 0:
                            toSpawn.transform.position = pos1 + spawn1.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn1.transform.rotation;
                            break;
                        case 1:
                            toSpawn.transform.position = pos2 + spawn2.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn1.transform.rotation;
                            break;
                        case 2:
                            toSpawn.transform.position = pos3 + spawn3.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn1.transform.rotation;
                            break;
                        case 3:
                            toSpawn.transform.position = pos4 + spawn4.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn1.transform.rotation;
                            break;
                        default:
                            break;
                    }
                    toSpawn.SetActive(true);
                }
                break;
            case 1:
                pos1 = spawn2.position + spawn2.right * 1;
                pos2 = spawn2.position + spawn2.right * -1;
                pos3 = spawn2.position + spawn2.right * 3;
                pos4 = spawn2.position + spawn2.right * -3;
                for (int i = 0; i < numEnemies; i++)
                {
                    GameObject toSpawn = enemyList[spawner].Find(x => enemyList[spawner].IndexOf(x) == startIndex+i);
                    switch (i % 4)
                    {
                        case 0:
                            toSpawn.transform.position = pos1 + spawn1.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn2.transform.rotation;
                            break;
                        case 1:
                            toSpawn.transform.position = pos2 + spawn2.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn2.transform.rotation;
                            break;
                        case 2:
                            toSpawn.transform.position = pos3 + spawn3.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn2.transform.rotation;
                            break;
                        case 3:
                            toSpawn.transform.position = pos4 + spawn4.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn2.transform.rotation;
                            break;
                        default:
                            break;
                    }
                    toSpawn.SetActive(true);
                }
                break;
            case 2:
                pos1 = spawn3.position + spawn3.right * 1;
                pos2 = spawn3.position + spawn3.right * -1;
                pos3 = spawn3.position + spawn3.right * 3;
                pos4 = spawn3.position + spawn3.right * -3;
                for (int i = 0; i < numEnemies; i++)
                {
                    GameObject toSpawn = enemyList[spawner].Find(x => enemyList[spawner].IndexOf(x) == startIndex+i);
                    switch (i % 4)
                    {
                        case 0:
                            toSpawn.transform.position = pos1 + spawn1.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn3.transform.rotation;
                            break;
                        case 1:
                            toSpawn.transform.position = pos2 + spawn2.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn3.transform.rotation;
                            break;
                        case 2:
                            toSpawn.transform.position = pos3 + spawn3.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn3.transform.rotation;
                            break;
                        case 3:
                            toSpawn.transform.position = pos4 + spawn4.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn3.transform.rotation;
                            break;
                        default:
                            break;
                    }
                    toSpawn.SetActive(true);
                }
                break;
            case 3:
                pos1 = spawn4.position + spawn4.right * 1;
                pos2 = spawn4.position + spawn4.right * -1;
                pos3 = spawn4.position + spawn4.right * 3;
                pos4 = spawn4.position + spawn4.right * -3;
                for (int i = 0; i < numEnemies; i++)
                {
                    GameObject toSpawn = enemyList[spawner].Find(x => enemyList[spawner].IndexOf(x) == startIndex+i);
                    switch (i % 4)
                    {
                        case 0:
                            toSpawn.transform.position = pos1 + spawn1.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn4.transform.rotation;
                            break;
                        case 1:
                            toSpawn.transform.position = pos2 + spawn2.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn4.transform.rotation;
                            break;
                        case 2:
                            toSpawn.transform.position = pos3 + spawn3.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn4.transform.rotation;
                            break;
                        case 3:
                            toSpawn.transform.position = pos4 + spawn4.forward * 2 * (int)(i / 4);
                            toSpawn.transform.rotation = spawn4.transform.rotation;
                            break;
                        default:
                            break;
                    }
                    toSpawn.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    //private void spawn(int wave)
    //{
    //    int numEnemies = (int)Math.Truncate(level[(wave - 1) * 4 + myID - 1]);
    //    int enemyid = getDecimalAsInt(level[(wave - 1) * 4 + myID - 1]);
    //    GameObject enemytype = (GameObject)enemies[enemyid - 1];
    //    int aux = 1;

    //    for (int i = 0; i < numEnemies; i++)
    //    {
    //        switch (i % 4)
    //        {
    //            case 0:
    //                Instantiate(enemytype, pos1 + transform.forward * 2 * (int)(i / 4), transform.rotation);
    //                break;
    //            case 1:
    //                Instantiate(enemytype, pos2 + transform.forward * 2 * (int)(i / 4), transform.rotation);
    //                break;
    //            case 2:
    //                Instantiate(enemytype, pos3 + transform.forward * 2 * (int)(i / 4), transform.rotation);
    //                break;
    //            case 3:
    //                Instantiate(enemytype, pos4 + transform.forward * 2 * (int)(i / 4), transform.rotation);
    //                break;
    //            default:
    //                break;
    //        }

    //        aux = -aux;
    //    }

    //    currentWave++;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!hasStarted && gameOn && (Time.time - startTime) > timeToStart)
    //    {
    //        hasStarted = true;
    //        startTime = Time.time;
    //        spawn(1);
    //    }

    //    if (hasStarted && Time.time - startTime > deltaTime)
    //    {
    //        startTime = Time.time;
    //        spawn(currentWave);
    //    }
    //}

    //private int getDecimalAsInt(float i)
    //{
    //    string outPut = "0";
    //    if (i.ToString().Split('.').Length == 2)
    //    {
    //        outPut = i.ToString().Split('.')[1].Substring(0, i.ToString().Split('.')[1].Length);
    //    }
    //    return Convert.ToInt32(outPut);
    //}
}
