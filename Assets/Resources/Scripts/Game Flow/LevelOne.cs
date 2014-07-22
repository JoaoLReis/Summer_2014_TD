using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LevelOne : Level {

    private const int numSpawners = 4;
    private const int numWaves = 10;
    private const int numLives = 20;
    private const int startGold = 250;

    //
    public Wave[][] levelSpawn = new Wave[numSpawners][]{ 
                   new Wave[numWaves] {
                        new Wave(new int[1, 2] {{ 1, 1 }}),
                        new Wave(new int[1, 2] {{ 1, 2 }}),
                        new Wave(new int[1, 2] {{ 1, 3 }}),
                        new Wave(new int[1, 2] {{ 3, 1 }}),
     /*Spawner 1*/      new Wave(new int[1, 2] {{3, 2}}),
                        new Wave(new int[1, 2] {{3, 3}}),
                        new Wave(new int[1, 2] {{5, 1}}),
                        new Wave(new int[1, 2] {{5, 2}}),
                        new Wave(new int[1, 2] {{5, 3}}),
                        new Wave(new int[1, 2] {{10, 1}})}
                    ,

                   new Wave[numWaves] {
                        new Wave(new int[1, 2] {{ 1, 1 }}),
                        new Wave(new int[1, 2] {{ 1, 2 }}),
                        new Wave(new int[1, 2] {{ 1, 3 }}),
                        new Wave(new int[1, 2] {{ 3, 1 }}),
    /*Spawner 2*/       new Wave(new int[1, 2] {{3, 2}}),
                        new Wave(new int[1, 2] {{3, 3}}),
                        new Wave(new int[1, 2] {{5, 1}}),
                        new Wave(new int[1, 2] {{5, 2}}),
                        new Wave(new int[1, 2] {{5, 3}}),
                        new Wave(new int[1, 2] {{10, 1}})}
                    ,

                   new Wave[numWaves] {
                        new Wave(new int[1, 2] {{ 1, 1 }}),
                        new Wave(new int[1, 2] {{ 1, 2 }}),
                        new Wave(new int[1, 2] {{ 1, 3 }}),
                        new Wave(new int[1, 2] {{ 3, 1 }}),
    /*Spawner 3*/       new Wave(new int[1, 2] {{3, 2}}),
                        new Wave(new int[1, 2] {{3, 3}}),
                        new Wave(new int[1, 2] {{5, 1}}),
                        new Wave(new int[1, 2] {{5, 2}}),
                        new Wave(new int[1, 2] {{5, 3}}),
                        new Wave(new int[1, 2] {{10, 1}})}
                    ,

                   new Wave[numWaves] {
                        new Wave(new int[1, 2] {{ 1, 1 }}),
                        new Wave(new int[1, 2] {{ 1, 2 }}),
                        new Wave(new int[1, 2] {{ 1, 3 }}),
                        new Wave(new int[1, 2] {{ 3, 1 }}),
    /*Spawner 4*/       new Wave(new int[1, 2] {{3, 2}}),
                        new Wave(new int[1, 2] {{3, 3}}),
                        new Wave(new int[1, 2] {{5, 1}}),
                        new Wave(new int[1, 2] {{5, 2}}),
                        new Wave(new int[1, 2] {{5, 3}}),
                        new Wave(new int[1, 2] {{10, 1}})}
                    };


    //##################################
    //######### ATTRIBUTES #############
    //##################################

    private GlobalData data;

    //List of enemies
    private List<GameObject>[] enemyList;
    private ArrayList[] indexList;
    private int enemyCounter;

    private UnityEngine.Object[] enemies;

    void Awake()
    {
        enemyCounter = 0;
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
        enemyList = new List<GameObject>[numSpawners];
        indexList = new ArrayList[numSpawners];
        for(int i = 0; i < numSpawners; i++)
        {
            enemyList[i] = new List<GameObject>();
            indexList[i] = new ArrayList();
        }
    }

    // Use this for initialization
    void Start()
    {
        enemies = data.enemies;
        for (int i = 0; i < numSpawners; i++)
            for (int k = 0; k < numWaves; k++)
            {
                enemyList[i].AddRange(createEnemies(levelSpawn[i][k].getToBeSpawned()));
                indexList[i].Add(enemyCounter);
            }
    }

    public override List<GameObject>[] getEnemyList()
    {
        return enemyList;
    }

    public override ArrayList[] getIndexList()
    {
        return indexList;
    }

    private List<GameObject> createEnemies(int[,] toBeSpawned)
    {
        List<GameObject> list = new List<GameObject>();
        //Number of different enemies
        for (int i = 0; i < toBeSpawned.GetLength(0); i++)
        {
            //Number of enemies
            for (int l = 0; l < toBeSpawned[i, 0]; l++)
            {
                GameObject it = Transform.Instantiate(enemies[toBeSpawned[i, 1] - 1], Vector3.zero, Quaternion.identity) as GameObject;
                it.SetActive(false);
                list.Add(it);
            }
        }
        enemyCounter = list.Count;
        return list;
    }

    public override int getNumWaves()
    {
        return numWaves;
    }

    public override int getNumSpawners()
    {
        return numSpawners;
    }

    public override int getNumLives()
    {
        return numLives;
    }

    public override int getGold()
    {
        return startGold;
    }

}
