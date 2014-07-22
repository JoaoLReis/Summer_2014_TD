using UnityEngine;
using System.Collections;

public class UIAndStats : MonoBehaviour {

    private GameManager gManager;
    private Level level;
    private GUISkin skin;

    //Textures
    private Texture2D play;
    private Texture2D faster;
    private Texture2D fastest;
    private Texture2D coin;
    private Texture2D life;
    
    //Player Stats
    private int score;
    private int gold;
    private int currentWave;
    private int lives;
    private int maxWaves;

    //dimensions
    private float ratio;
    private bool wideScreen;

    private float width;
    private float height;
    private float halfWidth;
    private float halfHeight;
    private float thirdWidth;
    private float thirdHeight;
    private float quarterWidth;
    private float quarterHeight;
    private float fifthWidth;
    private float fifthHeight;
    private float fourFifthWidth;
    private float fourFifthHeight;
    private float sixthWidth;
    private float sixthHeight;
    private float ninthWidth;
    private float ninthHeight;
    private float twoNinthWidth;
    private float twoNinthHeight;
    private float tenthWidth;
    private float tenthHeight;
    private float twelvethWidth;
    private float twelvethHeight;
    private float fifteenthWidth;
    private float fifteenthHeight;
    private float twoFifteenthWidth;
    private float twoFifteenthHeight;
    private float twentiethWidth;
    private float twentiethHeight;
    private float nineTenthWidth;
    private float nineTenthHeight;
    private float nineteenTwentW;
    private float nineteenTwentH;

    void Awake()
    {
        skin = Resources.Load("Skins/GeneralUI") as GUISkin;
        score = 0;
        currentWave = 0;

        ratio = (float)Screen.width / (float)Screen.height;
        wideScreen = ratio > 1.5f ? true : false;

        width = Screen.width;
        height = Screen.height;
        halfWidth = width / 2;
        halfHeight = height / 2;
        thirdWidth = width /3;
        thirdHeight = height / 3;
        quarterWidth = width / 4;
        quarterHeight = height / 4;
        fifthWidth = width / 5;
        fifthHeight = height / 5;
        fourFifthWidth = width * 4 / 5;
        fourFifthHeight = height * 4 / 5;
        sixthWidth = width / 6;
        sixthHeight = height / 6;
        ninthWidth = width / 9;
        ninthHeight = height / 9;
        twoNinthWidth = width * 2 / 9;
        twoNinthHeight = height * 2 / 9;
        tenthWidth = width / 10;
        tenthHeight = height / 10;
        twelvethWidth = width / 12;
        twelvethHeight = height / 12;
        fifteenthWidth = width / 15;
        fifteenthHeight = height / 15;
        twoFifteenthWidth = width * 2 / 15;
        twoFifteenthHeight = height * 2 / 15;
        twentiethWidth = width / 20;
        twentiethHeight = height / 20;
        nineTenthWidth = 9* tenthWidth;
        nineTenthHeight = 9* tenthHeight;
        nineteenTwentW = 19* twentiethWidth;
        nineteenTwentH = 19* twentiethHeight;
    }

    void Start()
    {
        gManager = GetComponent<GameManager>();
        level = GetComponent<Level>();
        lives = level.getNumLives();
        maxWaves = level.getNumWaves();
        gold = level.getGold();

        if (wideScreen)
        {
            play = Resources.Load("Textures/GameSpeed/16x9/Play") as Texture2D;
            faster = Resources.Load("Textures/GameSpeed/16x9/Faster") as Texture2D;
            fastest = Resources.Load("Textures/GameSpeed/16x9/Fastest") as Texture2D;
        }
        else
        {
            play = Resources.Load("Textures/GameSpeed/4x3/Play") as Texture2D;
            faster = Resources.Load("Textures/GameSpeed/4x3/Faster") as Texture2D;
            fastest = Resources.Load("Textures/GameSpeed/4x3/Fastest") as Texture2D;
        }
        coin = Resources.Load("Textures/Stats/Gold") as Texture2D;
        life = Resources.Load("Textures/Stats/Life") as Texture2D;
    }

    void OnGUI()
    {
        //SCORE
        GUI.BeginGroup(new Rect(0, 0, tenthWidth, twentiethHeight));

        GUI.Box(new Rect(0, 0, tenthWidth, twentiethHeight), "Score: " + score, skin.box);

        GUI.EndGroup();

        //GAME SPEED + WAVES
        GUI.BeginGroup(new Rect(halfWidth - tenthWidth, 0, quarterWidth, tenthHeight));

        //CALL NEXT WAVE
        if (GUI.Button(new Rect(0, 0, twentiethWidth, tenthHeight), "Next Wave", skin.button))
        {
        }

        //SPEED BUTTONS AND WAVE INFO
        GUI.BeginGroup(new Rect(twentiethWidth, 0, fifthWidth, tenthHeight));
        if (GUI.Button(new Rect(0, 0, twentiethWidth, twentiethHeight), play, skin.button))
        {
            Time.timeScale = 1;
        }

        if (GUI.Button(new Rect(twentiethWidth, 0, twentiethWidth, twentiethHeight), faster, skin.button))
        {
            Time.timeScale = 1.5f;
        }

        if (GUI.Button(new Rect(tenthWidth, 0, twentiethWidth, twentiethHeight), fastest, skin.button))
        {
            Time.timeScale = 2;
        }

        GUI.Box(new Rect(0, twentiethHeight, fifthWidth, twentiethHeight), "Wave: " + currentWave + " / " + maxWaves, skin.box);

        GUI.EndGroup();
        GUI.EndGroup();

        //STATS
        GUI.BeginGroup(new Rect(fourFifthWidth, 0, fifthWidth, twentiethHeight));
        
        GUI.Box(new Rect(0, 0, twentiethHeight, twentiethHeight), coin, skin.box);
        GUI.Box(new Rect(twentiethHeight, 0, tenthWidth - twentiethHeight, twentiethHeight), "" + gold, skin.box);

        GUI.Box(new Rect(tenthWidth, 0, twentiethHeight, twentiethHeight), life, skin.box);
        GUI.Box(new Rect(tenthWidth + twentiethHeight, 0, tenthWidth - twentiethHeight, twentiethHeight), "" + lives, skin.box);
        GUI.EndGroup();

    }

    public void updateScore(int newscore)
    {
        score = newscore;
    }

    public void updateLives(int newlives)
    {
        lives = newlives;
    }

    public void updateGold(int newgold)
    {
        gold = newgold;
    }

    public void updateWave(int newWave)
    {
        currentWave = newWave;
    }
}
