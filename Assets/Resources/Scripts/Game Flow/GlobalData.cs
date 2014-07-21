using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GlobalData : MonoBehaviour{

    public int currentLevel;

    public Object[] enemies;
    private List<GameObject> relays;

    // TABLE CONTAINING DAMAGE FROM DIFFERENT SOURCES
    private float[] vulnerability = {
    //                                          Enemy type
    //                                  /**/   Fire    /**/      Earth    /**/     Water   /**/
    /*                        FIRE      /**/     0,    /**/        1,     /**/      2,     /**/  
    /*                        EARTH     /**/     2,    /**/        0,     /**/     1.5f,   /**/
    /*                        WATER     /**/     0,    /**/        1,     /**/      2,     /**/
    /*           Tower Type   ICE       /**/     1,    /**/        1,     /**/      1,     /**/
    /*                        LAVA      /**/     2,    /**/       1.5f,   /**/     0.5f,   /**/
    /*                        ARCANE    /**/     1,    /**/        1,     /**/      1,     /**/
    /*                        LIGHTNING /**/     0,    /**/        0,     /**/      1,     /**/
    /*                        WIND      /**/    1.5f,  /**/        3,     /**/      1      /**/
    };


    // #################################################
    // #################### METHODS ####################
    // #################################################

    void Awake()
    {
        currentLevel = 1;
        enemies = Resources.LoadAll("Enemies");
        relays = new List<GameObject>();
    }

    public float[] getVulnerability()
    {
        return vulnerability;
    }
    
}