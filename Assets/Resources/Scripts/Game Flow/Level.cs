using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Level : MonoBehaviour {

    public abstract List<GameObject>[] getEnemyList();

    public abstract ArrayList[] getIndexList();

    public abstract int getNumWaves();

    public abstract int getNumSpawners();

    public abstract int getNumLives();

    public abstract int getGold();
}
