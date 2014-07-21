using UnityEngine;
using System.Collections;

public class UberSelectionScript : MonoBehaviour {

    public Transform tower;
    private UberBuildMenu buildMenuScript;
    private Color color;

    void Start()
    {
        color = renderer.material.color;
        buildMenuScript = GameObject.Find("BuildMenu").GetComponent<UberBuildMenu>();
    }

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
        renderer.material.color = color;
        buildMenuScript.Instantiate(tower);
    }
}
