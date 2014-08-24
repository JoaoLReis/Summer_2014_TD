using UnityEngine;
using System.Collections;

public abstract class TowerDamage : Imports {
    
    protected TowerStats stats;
    protected TowerBehaviour tBehaviour;
    protected GameObject weapon;
    protected EnemyStats target;
    protected bool stopped;

    public void start()
    {
        if (stopped)
        {
            StartCoroutine("damageTarget");
            Debug.Log("started damage coroutine");
        }
        else Debug.Log("ugh it isnt stopped");
          
    }

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
