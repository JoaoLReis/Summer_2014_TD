using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FireTowerBehaviour : TowerBehaviour {

    void Awake()
    {
        inRange = new List<GameObject>();
        damager = GetComponent<FireTowerDamage>();
        firing = false;
    }

	// Use this for initialization
	void Start () {
        rotator = FindChildWithTag("Rotator").gameObject;
        currentWeapon = FindChildWithTag("Weapon1").gameObject;
        aim = rotator.GetComponent<LookAtEnemy>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            int enemyType = other.gameObject.GetComponent<EnemyStats>().getType();
            if (!(enemyType == (int)Element.FIRE || enemyType == (int)Element.LAVA || enemyType == (int)Element.LIGHTNING))
            {
                inRange.Add(other.gameObject);
                if (inRange.Count == 1)
                {
                    enableFiring(other.transform);
                }
            }
        }
    }

}
