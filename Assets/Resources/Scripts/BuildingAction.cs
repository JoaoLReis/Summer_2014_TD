using UnityEngine;
using System.Collections;

public class BuildingAction : Imports {

    private bool buildMode;
    private bool selected;
    private Transform buildMenu;
    private UberBuildMenu buildMenuScript;
    private HexTileMap tileMapScript;               // a reference to the hex tilemap script
    private GameManager gManager;

	// Use this for initialization
	void Start () {
        buildMenu = GameObject.Find("BuildMenu").transform;
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
        tileMapScript = GameObject.Find("HexEditor").GetComponent<HexTileMap>();
        buildMenuScript = buildMenu.GetComponent<UberBuildMenu>();
        buildMenu.gameObject.SetActive(false);
        buildMode = false;
        selected = false;
	}

    void buildControl()
    {

        //probably can be optimised
        Hex intersectedTile = tileMapScript.RuntimeHexFromWorld();
        if (intersectedTile != null)
        {
            buildMenu.gameObject.SetActive(true);
            if (selected)
            {
                switch (gManager.getGameState())
                {
                    case GameState.Relay:
                        buildMenuScript.goRelay();
                        break;
                    case GameState.Defence:
                        buildMenuScript.goRegular();
                        break;
                    default:
                        break;
                }
            }
            else buildMenu.position = intersectedTile.findBuildPosition();
            //Camera.main.GetComponent<UberCamera>().SetLookatBuild(true);

        }

    }

	// Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Build"))
        {
            if (buildMode)
            {
                buildMenuScript.goHidden();
                buildMode = false;
                buildMenu.gameObject.SetActive(false);
                selected = false;
                Camera.main.GetComponent<UberCamera>().SetLookatBuild(false);
            }
            else
            {
                buildMode = true;
            }
        }

        if (buildMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selected)
                {
                    selected = false;
                    buildMenuScript.goHidden();
                }
                else selected = true;
            }

            buildControl();
        }

    }
}
