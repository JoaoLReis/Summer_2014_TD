using UnityEngine;
using System.Collections;

public abstract class TowerStats : MonoBehaviour {

    // BASE STATS
    protected int health;
    protected int xp;
    protected int damage;
    protected float speed;
    protected float armorPen;
    protected int rank;
    public int value;

    //#################  SPEED ####################
    public void increaseSpeed(float percentage)
    {
        speed = speed + speed * percentage;
    }

    public void decreaseSpeed(float percentage)
    {
        speed = speed - speed * percentage;
    }

    public float getSpeed() { return speed; }

    //################# HEALTH ###################
    public void decreaseHealth(int num)
    {
        health -= num;
    }

    public void increaseHealth(int num)
    {
        health += num;
    }

    //################ EXPERIENCE ################
    public void increaseXP(int num)
    {
        xp += num;
    }

    public void decreaseXP(int num)
    {
        xp -= num;
    }

    //############### ARMOR PENETRATION ##########
    public void increaseArmorPen(int num)
    {
        armorPen += num;
    }

    public void decreaseArmorPen(int num)
    {
        armorPen -= num;
    }

    public float getArmorPen() { return armorPen; }

    //############### DAMAGE ##########
    public void increaseDamage(int num)
    {
        damage += num;
    }

    public void decreaseDamage(int num)
    {
        damage -= num;
    }

    public int getDamage() { return damage; }

    //############### LEVELUP ##########
    public abstract void increaseRank();

    //############# SELLING ################
    public abstract void sell();

}
