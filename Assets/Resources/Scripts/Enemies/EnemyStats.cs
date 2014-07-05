﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class EnemyStats : Imports {

    protected int numElements;
    protected int numEnemies;

    // BASE STATS
    protected int health;
    protected float damage;
    protected float atkSpeed;
    protected NavMeshAgent movement;
    protected float buildingDamage;
    protected int type;
    protected int armor;
    /* IN CASE WE HAVE A SURVIVAL MODE */protected int rank;
    public int value;

    // TABLE CONTAINING DAMAGE FROM DIFFERENT SOURCES
    protected float[] vulnerability;



    // INIT 
    protected void init()
    {
        vulnerability = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>().getVulnerability();
        numElements = sizeOfElements();
        numEnemies = sizeOfEnemyTypes();
    }

//##########################################
//################ MODIFIERS ###############
//##########################################

    //############ ATTACK SPEED 
    public void increaseATKSpeed(float percentage)
    {
        atkSpeed = atkSpeed + atkSpeed * percentage;
    }

    public void decreaseATKSpeed(float percentage)
    {
        atkSpeed = atkSpeed - atkSpeed * percentage;
    }

    //############ MOVEMENT SPEED
    public void increaseMoveSpeed(float percentage)
    {
        atkSpeed = atkSpeed + atkSpeed * percentage;
    }

    public void decreaseMoveSpeed(float percentage)
    {
        atkSpeed = atkSpeed - atkSpeed * percentage;
    }

    //############# HEALTH 
    public bool decreaseHealth(int num, float armPen, int type)
    {
        int index = this.type * numElements + type;
        health -= (int)((num - armor * (1 - armPen)) * vulnerability[index]);
        if(health < 1)
        {
            //Atribuir valor ao jogador
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public void increaseHealth(int num)
    {
        health += num;
    }

    public int getHealth() { return health; }

    //############# BUILDING DAMAGE 
    public void increaseBuildingDamage(int num)
    {
        buildingDamage += num;
    }

    public void decreaseBuildingDamage(int num)
    {
        buildingDamage -= num;
    }

    //############# DAMAGE 
    public void increaseDamage(int num)
    {
        damage += num;
    }

    public void decreaseDamage(int num)
    {
        damage -= num;
    }

    //############# LEVELUP 
    public abstract void increaseRank();

    //############# TYPE 
    public int getType() { return type; }


//#################################################
//################## UPDATES ######################
//#################################################
    
    
}
