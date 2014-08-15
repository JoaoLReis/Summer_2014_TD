using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class HexKeyValueInt : ScriptableObject
{
    [SerializeField]
    private int Key;
    [SerializeField]
    private int Value;

    public void init(int key, int value)
    {
        Key = key;
        Value = value;
    }

    public int getKey()
    {
        return Key;
    }

    public int getValue()
    {
        return Value;
    }
}
