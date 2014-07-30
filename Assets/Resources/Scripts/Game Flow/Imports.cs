using UnityEngine;
using System;
using System.Collections;

public abstract class Imports : MonoBehaviour {

    public enum GameState { Relay, Defence, Success, Failure}

    protected enum Element { FIRE, EARTH, WATER, ICE, LAVA, ARCANE, LIGHTNING, WIND }

    protected enum EnemyTypes { FIRE, EARTH, WATER }

    protected int sizeOfElements() { return Enum.GetNames(typeof(Element)).Length; }
    protected int sizeOfEnemyTypes() { return Enum.GetNames(typeof(EnemyTypes)).Length; }
}
