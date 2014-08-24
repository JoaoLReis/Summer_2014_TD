using UnityEngine;
using System.Collections;

public class UberSelectionScriptDestroy : UberSelectionScript{

    void Start()
    {
        color = renderer.material.color;
        buildMenuScript = GameObject.Find("BuildMenu").GetComponent<UberBuildMenu>();
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
    }

    void OnMouseDown()
    {
    }

}
