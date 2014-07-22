using UnityEngine;
using System.Collections;

public class UberSelectionScriptEarth : UberSelectionScript{

    void Start()
    {
        color = renderer.material.color;
        buildMenuScript = GameObject.Find("BuildMenu").GetComponent<UberBuildMenu>();
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
        tower = Resources.Load("Towers/EarthTower") as GameObject;
        towerValue = data.getPriceTable()[(int)Element.EARTH];
    }

}
