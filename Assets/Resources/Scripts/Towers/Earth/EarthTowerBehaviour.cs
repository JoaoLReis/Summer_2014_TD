using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EarthTowerBehaviour : TowerBehaviour {

    void Awake()
    {
        inRange = new List<GameObject>();
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

}
