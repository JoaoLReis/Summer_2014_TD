﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

    private UIAndStats uiStats;

    //Level
    public int currentLevel;
    private int relaysNumber;
    private List<GameObject> relays;
    private List<GameObject>[] enemyList;
    private ArrayList[] enemyIndex; 
    private Level level;
    private Spawner spawner;


    //PlayerStats
    private int gold;
    private int score;
    private int currentWave;
    private int lives;


    //#####################################################
    //
    //#####################################################

    

    // #################################################
    // #################### METHODS ####################
    // #################################################

    void Awake()
    {
        currentLevel = 1;
        relays = new List<GameObject>();
        level = GetComponent<Level>();
        spawner = GetComponent<Spawner>();
        relaysNumber = 1;
    }

    void Start()
    {
        uiStats = GetComponent<UIAndStats>();
        lives = level.getNumLives();
        StartCoroutine("finalize");
    }

    private IEnumerator finalize()
    {
        while (true)
        {
            if (!Application.isLoadingLevel)
            {
                enemyList = level.getEnemyList();
                enemyIndex = level.getIndexList();
                spawner.startUp(enemyList, enemyIndex);
                break;
            }
            yield return new WaitForSeconds(5);
        }
    }

    public void addRelay(GameObject relay)
    {
        Debug.Log("addRelay");
        relays.Add(relay);
        if(relays.Count == relaysNumber)
        {
            startWaves();
        }
    }

    private void startWaves()
    {
        Debug.Log("StartWaves");
        spawner.spawn();
    }

    public GameObject getRelay()
    {
        return relays.First();
    }

    public void destroyEnemy(GameObject o)
    {
        int enemyvalue = o.GetComponent<EnemyStats>().value;
        increaseGold(enemyvalue);
        increaseScore(enemyvalue);
        Destroy(o);
    }

    private void increaseGold(int amount)
    {
        gold += amount;
        uiStats.updateGold(gold);
    }

    private void increaseScore(int amount)
    {
        score += amount;
        uiStats.updateScore(score);
    }

    public void increaseWave()
    {
        currentWave++;
        uiStats.updateWave(currentWave);
    }
}
