using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Spawner : MonoBehaviour {

    public abstract void startUp(List<GameObject>[] enemyList, ArrayList[] indexList);
    public abstract void spawn();
    
}
