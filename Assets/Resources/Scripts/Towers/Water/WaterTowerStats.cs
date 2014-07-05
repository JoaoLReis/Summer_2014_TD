using UnityEngine;
using System.Collections;

public class WaterTowerStats : TowerStats {

    void Awake()
    {
        health = 20;
        xp = 0;
        damage = 10;
        speed = 0.75f;
        armorPen = 0.2f;
        rank = 1;
        value = 100;
        type = (int)Element.WATER;
    }

    //############### LEVELUP ##########
    public override void increaseRank()
    {
        TowerBehaviour tBehaviour = gameObject.GetComponent<TowerBehaviour>();

        switch (rank)
        {
            case 1:
                increaseHealth(20);
                increaseSpeed(0.1f);
                increaseDamage(10);
                xp = 0;
                rank++;
                tBehaviour.upgradeWeapon(rank);
                break;
            case 2:
                increaseHealth(40);
                increaseSpeed(0.15f);
                increaseDamage(30);
                xp = 0;
                rank++;
                tBehaviour.upgradeWeapon(rank);
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    //############# SELLING ################
    public override void sell()
    {
        Destroy(gameObject);
    }
}
