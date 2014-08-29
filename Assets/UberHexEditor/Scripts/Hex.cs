using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Attention instead of using types use an integer and compare altitude values for a more generic altitude behaviour
//Or still use types but add altitude as a comparable value, like there is low medium and high for altitude 0.0, the same for 1.0 etc...
public enum TileType
{
    NOT_ASSIGNED, RAMP_L, RAMP_H , WATER_L, WATER_M, WATER_H, NATURE_L, NATURE_M, NATURE_H, FIRE_L, FIRE_M, FIRE_H, BLOCKED
}

public enum TowerType
{
    NOT_ASSIGNED, FIRE, WATER, NATURE
}

[Serializable]
public class Hex : ScriptableObject
{
    public HexKeyValueInt zx;
    public Vector3 position;
    public GameObject instance;
    public TileType type;
    public TowerType p_type;
    public Vector3 buildPosition;

    public float f_score;
    public float g_score;

    public Hex cameFrom;

    [SerializeField]
    private List<HexKeyValueInt> adjacentHexes = new List<HexKeyValueInt>(6);

    [SerializeField]
    private List<HexKeyValueInt> pathHexes = new List<HexKeyValueInt>(6);

    public void init(Vector3 pos, int Z, int X)
    {
        g_score = 0;
        f_score = 0;
        zx = ScriptableObject.CreateInstance("HexKeyValueInt") as HexKeyValueInt;
        zx.init(Z, X);
        position = pos;
        instance = null;
        type = TileType.NOT_ASSIGNED;
        p_type = TowerType.NOT_ASSIGNED;
        cameFrom = null;
    }

    public void addPathHex(HexKeyValueInt hex)
    {
        pathHexes.Add(hex);
    }

    public void removePathHex(HexKeyValueInt hex)
    {
        pathHexes.Remove(hex);
    }

    public void addHex(HexKeyValueInt hex)
    {
        adjacentHexes.Add(hex);
    }

    public void removeHex(HexKeyValueInt hex)
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

    public List<HexKeyValueInt> getList()
    {
        return adjacentHexes;
    }

    public List<HexKeyValueInt> getPathList()
    {
        return pathHexes;
    }

    public void genPathList()
    {
        pathHexes = new List<HexKeyValueInt>(6);
    }

    public void genList()
    {
        adjacentHexes = new List<HexKeyValueInt>(6);      
    }

    public Vector3 findBuildPosition()
    {
        buildPosition = instance.transform.FindChild("buildPosition").position;
        return buildPosition;
    }

    public Hex simplifiedClone()
    {
        Hex tmp = ScriptableObject.CreateInstance("Hex") as Hex;
        tmp.init(position, zx.getKey(), zx.getValue());
        return tmp;
    }
}