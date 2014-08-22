using UnityEngine;
using System.Collections;

public class FireTowerDamage : TowerDamage
{
    private int type;

    void Awake()
    {
        stats = GetComponent<TowerStats>();
        tBehaviour = GetComponent<TowerBehaviour>();
        stopped = true;
    }

    // Use this for initialization
    void Start()
    {
        weapon = tBehaviour.getWeapon();
        target = tBehaviour.getTarget().gameObject.GetComponent<EnemyStats>();
        type = stats.getType();
    }

    public IEnumerator damageTarget()
    {
        stopped = false;
        while (true)
        {
            if (stopped)
                yield break;
            else if (target == null || target.decreaseHealth(1, stats.getArmorPen(), type))
            {
                yield break;
            }
            yield return new WaitForSeconds(1 / stats.getSpeed() / stats.getDamage());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
