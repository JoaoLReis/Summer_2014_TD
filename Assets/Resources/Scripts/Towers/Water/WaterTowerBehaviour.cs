using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaterTowerBehaviour : TowerBehaviour {

    void Awake()
    {
        inRange = new List<GameObject>();
        notInRange = new List<GameObject>();
        damager = GetComponent<TowerDamage>();
        firing = false;
    }

    // Use this for initialization
    void Start()
    {
        rotator = FindChildWithTag("Rotator").gameObject;
        currentWeapon = FindChildWithTag("Weapon1").gameObject;
        aim = rotator.GetComponent<LookAtEnemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            int enemyType = other.gameObject.GetComponent<EnemyStats>().getType();
            if (!(enemyType == (int)Element.WATER || enemyType == (int)Element.ICE))
            {
                inRange.Add(other.gameObject);
                if (inRange.Count == 1)
                {
                    StartCoroutine("enableFiring", other.transform);
                }
            }
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("OTE CALLED!: " + "This: " + gameObject + " With That: " + other.name);
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        int enemyType = other.gameObject.GetComponent<EnemyStats>().getType();
    //        if (!(enemyType == (int)Element.WATER || enemyType == (int)Element.ICE))
    //        {
    //            inRange.RemoveAll(item => item == null);
    //            inRange.Remove(other.gameObject);


    //            if (target.gameObject == other.gameObject)
    //            {
    //                if (inRange.Count > 0)
    //                {
    //                    target = inRange.First().transform;
    //                    aim.setActiveTarget(target);
    //                    damager.updateTarget(target);
    //                }
    //                else
    //                {
    //                    disableFiring();
    //                }
    //            }
    //        }
    //    }
    //}
}
