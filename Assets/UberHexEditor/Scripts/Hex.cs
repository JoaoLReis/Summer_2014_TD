using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Hex
{
    public int z, x;
    public Vector3 position;
    public GameObject instance;
    //Movement hazards....

    public Hex(Vector3 pos, int Z, int X)
    {
        z = Z;
        x = X;
        position = pos;
        instance = null;
    }
}