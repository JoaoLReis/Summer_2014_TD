using UnityEngine;
using System.Collections;

public abstract class UberSelectionScript : Imports {

    protected GameObject tower;
    protected GameManager gManager;
    protected GlobalData data;
    protected UberBuildMenu buildMenuScript;
    protected Color color;
    protected int towerValue;


    void OnMouseEnter()
    {
        renderer.material.color = Color.green;
    }

    void OnMouseExit()
    {
        renderer.material.color = color;
    }

    void OnMouseDown()
    {
        if (gManager.getGold() > towerValue)
        {
            renderer.material.color = color;
            buildMenuScript.Instantiate(tower);
        }
        else
        {
            notEnoughMoney();
        }
    }

    private void notEnoughMoney()
    {
        renderer.material.color = Color.red;
        Invoke("restore", 0.5f);
    }

    private void restore()
    {
        renderer.material.color = color;
    }
}
