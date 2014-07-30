using UnityEngine;
using System.Collections;

public abstract class UberSelectionScript : Imports {

    protected GameManager gManager;
    protected GlobalData data;
    protected UberBuildMenu buildMenuScript;
    protected Color color;


    void OnMouseEnter()
    {
        renderer.sharedMaterial.color = Color.green;
    }

    void OnMouseExit()
    {
        renderer.sharedMaterial.color = color;
    }

    protected void notEnoughMoney()
    {
        renderer.sharedMaterial.color = Color.red;
        Invoke("restore", 0.5f);
    }

    private void restore()
    {
        renderer.material.color = color;
    }
}
