using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
public class GameFinisher : MonoBehaviour {

    private GUISkin skin;
    private Texture2D tex;
    private RelayBehaviour relay;
    private GameManager gManager;
    private bool finishedAnim;
    private bool isStopped;

    private Color color;

    void Awake()
    {
        skin = Resources.Load("Skins/EndGame") as GUISkin;
        finishedAnim = false;
        isStopped = false;
    }

    // Use this for initialization
    void Start()
    {
        tex = Resources.Load("Textures/GUITextures/EndGame/GreyBackground") as Texture2D;
        gManager = GetComponent<GameManager>();
        color = Color.white;
        color.a = 0.0f;
        skin.window.normal.background = tex;
        skin.window.onNormal.background = tex;
    }

    public void finishGame(bool isVictory)
    {
        relay = GameObject.FindWithTag("Relay").GetComponent<RelayBehaviour>();
        Debug.Log(isVictory);
        Debug.Log(relay.name);
        relay.finishGame(isVictory);
        Invoke("endAnim", 5);
    }

    private void endAnim()
    {
        finishedAnim = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (finishedAnim)
        {
            GUI.color = color;
            if (color.a > 0.8f && !isStopped)
            {
                Time.timeScale = 0;
                isStopped = true;
            }
            if (color.a < 0.8f)
                color.a += 0.001f;
            GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), endGame, "", skin.window);

        }
    }

    private void endGame(int windowID)
    {

        GUI.Box(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 10, Screen.width / 3, Screen.height / 5), "GAME OVER", skin.box);

        int groupWidth = Screen.width / 8;
        int groupHeight = Screen.height / 4;

        GUI.BeginGroup(new Rect(Screen.width / 2 - groupWidth / 2, Screen.height / 2 - Screen.height / 10, groupWidth, groupHeight));
        if (GUI.Button(new Rect(0, 0, groupWidth, groupHeight / 3), "Continue", skin.button))
        {
        }

        if (GUI.Button(new Rect(0, groupHeight / 3, groupWidth, groupHeight / 3), "Restart", skin.button))
        {
            Time.timeScale = 1;
            Application.LoadLevel(gManager.getLevel());
        }

        if (GUI.Button(new Rect(0, groupHeight * 2 / 3, groupWidth, groupHeight / 3), "Main Menu", skin.button))
        {
            Time.timeScale = 1;
            Application.LoadLevel(0);
        }

        GUI.EndGroup();
    }

    
}
