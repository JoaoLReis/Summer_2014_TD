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

    //#####################################################
    //Idealy we would set the order of spawning in an array
    //#####################################################

    //Something like (with 4 spawn points) {firstwave1, firstwave2, firstwave3, firstwave4, secondwave1, secondwave2, secondwave3, secondwave4...}

    //Then you would have like firstwave1 = 5.1 -> 5 enemies of type 1

    public float[] levelOne;
    
    /*#################################################
     * ################### METHODS ####################
     * ################################################*/

    void Awake()
    {
        levelOne = new float[] { 1.1f, 1.1f, 1.1f, 1.1f, 1.2f, 1.2f, 1.2f, 1.2f, 1.3f, 1.3f, 1.3f, 1.3f, 3.1f, 3.1f, 2.2f, 1.2f, 5.1f, 2.2f, 3.1f, 3.1f };
        currentLevel = 1;
        enemies = Resources.LoadAll("Enemies");
        relays = new List<GameObject>();
    }

    public float[] getVulnerability()
    {
        return vulnerability;
    }

    public float[] getCurrentLevel()
    {
        switch (currentLevel)
        {
            case 1:
                return levelOne;
            case 2:
            default:
                return levelOne;
        }
    }

    public void addRelay(GameObject relay)
    {
        relays.Add(relay);
    }

    public GameObject getRelay()
    {
        return relays.First();
    }


}