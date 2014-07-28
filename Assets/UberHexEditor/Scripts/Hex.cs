using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TileType
{
    NOT_ASSIGNED, RAMP, GROUND, WATER, NATURE, FIRE
}

[Serializable]
public class Hex
{
    public int z, x;
    public Vector3 position;
    public GameObject instance;
    public TileType type;

    private List<Hex> adjacentHexes;

    public Hex(Vector3 pos, int Z, int X)
    {
        z = Z;
        x = X;
        position = pos;
        instance = null;
        adjacentHexes = new List<Hex>();
        type = TileType.NOT_ASSIGNED;
    }

    public void addHex(Hex hex)
    {
        adjacentHexes.Add(hex);
    }

    public void removeHex(Hex hex)
    {
        adjacentHexes.Remove(hex);
    }

    public bool hasInstance()
    {
        if (instance != null)
        {
            return true;
        }
        else return false;
    }

    public List<Hex> getList()
    {
        return adjacentHexes;
    }

    public void genList()
    {
        adjacentHexes = new List<Hex>();
    }

    public Vector3 getBuildPosition()
    {
        return instance.transform.FindChild("buildPosition").position;
    }
}