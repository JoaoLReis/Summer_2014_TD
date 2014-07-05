using UnityEngine;
using System.Collections;

public class EnemyGUI : MonoBehaviour {

    private Texture2D healthBar;
    private GUISkin skin;
    private GUIStyle box;
    private EnemyStats stats;
    private float maxHP;
    private int boxSizeX = 30;
    private int boxSizeY = 8;


    // Use this for initialization
	void Start () 
    {
        healthBar = Resources.Load("Textures/HealthBars/HealthBar") as Texture2D;
        skin = Resources.Load("Skins/HealthBars") as GUISkin;
        stats = GetComponent<EnemyStats>();
        maxHP = stats.getHealth();
        box = new GUIStyle(skin.box);
	}

    void OnGUI()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + transform.up);
        GUI.Box(new Rect(pos.x-(boxSizeX/2), Screen.height - pos.y, boxSizeX, boxSizeY), healthBar, box);
        box.padding.right = (int)((1.0f - (stats.getHealth() / maxHP)) * boxSizeX);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
