using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EarthTowerDamage : TowerDamage {

    private EarthProjectilesManager bulletManager;

    void Awake()
    {
        stats = GetComponent<TowerStats>();
        tBehaviour = GetComponent<TowerBehaviour>();
        bulletManager = GetComponent<EarthProjectilesManager>();
        stopped = true;
    }

    // Use this for initialization
    void Start()
    {
        weapon = tBehaviour.getWeapon();
        target = tBehaviour.getTarget().gameObject.GetComponent<EnemyStats>();
    }

    public IEnumerator damageTarget()
    {
        stopped = false;
        while (true)
        {
            if (stopped)
                yield break;
            if (target == null)
                tBehaviour.recalculateTarget();
            else
            {
                bulletManager.fireBullet();
            }
            /*else if (target.decreaseHealth(1, stats.getArmorPen(), (int)Element.EARTH))
            {
                Debug.Log("RETURNED TRUE");
                yield return new WaitForEndOfFrame();
                tBehaviour.recalculateTarget();
            }*/
            yield return new WaitForSeconds(1 / stats.getSpeed());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
