using UnityEngine;
using System.Collections;

public class FireElementalStats : EnemyStats {

    void Awake()
    {
        health = 20;
        damage = 1;
        atkSpeed = 1;
        buildingDamage = 1f;
        type = (int)EnemyTypes.FIRE;
        rank = 1;
        armor = 0;
        value = 10;
        init();
    }

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<NavMeshAgent>();
    }

    //############### LEVELUP ##########
    public override void increaseRank()
    {
        switch (rank)
        {
            case 1:
                increaseHealth(20);
                increaseATKSpeed(0.1f);
                increaseDamage(10);
                rank++;
                break;
            case 2:
                increaseHealth(40);
                increaseATKSpeed(0.15f);
                increaseDamage(30);
                increaseBuildingDamage(1);
                rank++;
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
