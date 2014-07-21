using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave{

    int[,] toBeSpawned;

    public Wave(int[,] enemies)
    {
        toBeSpawned = enemies;
    }

    public int[,] getToBeSpawned()
    {
        return toBeSpawned;
    }
}
