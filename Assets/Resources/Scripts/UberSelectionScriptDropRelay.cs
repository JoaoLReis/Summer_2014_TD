using UnityEngine;
using System.Collections;

public class UberSelectionScriptDropRelay : UberSelectionScript{

    private GameObject relay;

    void Start()
    {
        color = renderer.material.color;
        buildMenuScript = GameObject.Find("BuildMenu").GetComponent<UberBuildMenu>();
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
        relay = Resources.Load("Prefabs/relay") as GameObject;
    }

    void OnMouseDown()
    {
        renderer.sharedMaterial.color = color;
        buildMenuScript.dropRelay(relay);
    }

}
