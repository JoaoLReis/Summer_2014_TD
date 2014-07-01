using UnityEngine;
using System.Collections;

public abstract class EnemyStats : Imports {

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

    //################# ATTACK SPEED ####################
    public void increaseATKSpeed(float percentage)
    {
        atkSpeed = atkSpeed + atkSpeed * percentage;
    }

    public void decreaseATKSpeed(float percentage)
    {
        atkSpeed = atkSpeed - atkSpeed * percentage;
    }

    //################# MOVEMENT SPEED ####################
    public void increaseMoveSpeed(float percentage)
    {
        atkSpeed = atkSpeed + atkSpeed * percentage;
    }

    public void decreaseMoveSpeed(float percentage)
    {
        atkSpeed = atkSpeed - atkSpeed * percentage;
    }

    //################# HEALTH ###################
    public bool decreaseHealth(int num, float armPen, int type)
    {
        health -= (int) (num - armor*(1-armPen));
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

    //############### BUILDING DAMAGE ##########
    public void increaseBuildingDamage(int num)
    {
        buildingDamage += num;
    }

    public void decreaseBuildingDamage(int num)
    {
        buildingDamage -= num;
    }

    //############### DAMAGE ##########
    public void increaseDamage(int num)
    {
        damage += num;
    }

    public void decreaseDamage(int num)
    {
        damage -= num;
    }

    //############### LEVELUP ##########
    public abstract void increaseRank();

    //############### TYPE #############
    public int getType() { return type; }
}
