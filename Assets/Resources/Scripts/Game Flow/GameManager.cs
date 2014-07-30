using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(Level))]
[RequireComponent(typeof(GameFinisher))]
public class GameManager : Imports {


    private GameState gameState;
    private UIAndStats uiStats;

    //Level
    public int currentLevel;
    private int relaysNumber;
    private List<GameObject> relays;
    private List<GameObject>[] enemyList;
    private ArrayList[] enemyIndex; 
    private Level level;
    private Spawner spawner;
    private GameFinisher finisher;


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
        finisher = GetComponent<GameFinisher>();
        relaysNumber = 1;
        gameState = GameState.Relay;
    }

    void Start()
    {
        uiStats = GetComponent<UIAndStats>();
        lives = level.getNumLives();
        gold = level.getGold();
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
            gameState = GameState.Defence;
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

    public void enemyFinish(GameObject o)
    {
        int damage = o.GetComponent<EnemyStats>().getDamage();
        lives -= damage;
        uiStats.updateLives(lives);
        if(lives < 1)
        {
            //Finish game -> get script that finishes and activate. Stop game.
        }
        Destroy(o);



    }

    public void destroyEnemy(GameObject o)
    {
        int enemyvalue = o.GetComponent<EnemyStats>().value;
        updateGold(enemyvalue);
        updateScore(enemyvalue);
        Destroy(o);
    }

    private void updateGold(int amount)
    {
        gold += amount;
        uiStats.updateGold(gold);
    }

    private void updateScore(int amount)
    {
        score += amount;
        uiStats.updateScore(score);
    }

    public void increaseWave()
    {
        currentWave++;
        uiStats.updateWave(currentWave);
    }

    public int getGold()
    {
        return gold;
    }

    public GameState getGameState()
    {
        return gameState;
    }

    public void createdTower(GameObject tower)
    {
        int value = tower.GetComponent<TowerStats>().getValue();
        updateGold(-value);
    }
}
