using UnityEngine;
using System.Collections;

public class UberSelectionScriptWater : UberSelectionScript{

    protected GameObject tower;
    protected int towerValue;

    void Start()
    {
        color = renderer.material.color;
        buildMenuScript = GameObject.Find("BuildMenu").GetComponent<UberBuildMenu>();
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
        tower = Resources.Load("Towers/WaterTower") as GameObject;
        towerValue = data.getPriceTable()[(int)Element.WATER];
    }

    void OnMouseDown()
    {
        if (gManager.getGold() > towerValue)
        {
            renderer.sharedMaterial.color = color;
            buildMenuScript.Instantiate(tower);
        }
        else
        {
            notEnoughMoney();
        }
    }

}
