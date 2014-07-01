using UnityEngine;
using System.Collections;

public abstract class TowerDamage : Imports {
    
    protected TowerStats stats;
    protected TowerBehaviour tBehaviour;
    protected GameObject weapon;
    protected EnemyStats target;
    protected bool stopped;

    public abstract void start();

    public void updateTarget(Transform t)
    {
        target = t.gameObject.GetComponent<EnemyStats>();
    }

    public void stop()
    {
        stopped = true;
    }

    public void updateWeapon()
    {
    }

}
