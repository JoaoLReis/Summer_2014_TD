using UnityEngine;
using System.Collections;

public class UIAndStats : MonoBehaviour {

    private GameManager gManager;
    private Level level;
    private GUISkin uiSkin;
    private GUISkin menuSkin;

    private bool paused;
    private float gameSpeed;
    private bool muted;

    //Textures
    private Texture2D play;
    private Texture2D faster;
    private Texture2D fastest;
    private Texture2D coin;
    private Texture2D life;
    private Texture2D pause;
    private Texture2D noSound;
    private Texture2D sound;
    
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
    private float ftFifteenWidth;
    private float ftFifteenHeight;
    private float twentiethWidth;
    private float twentiethHeight;
    private float nineTenthWidth;
    private float nineTenthHeight;
    private float nineteenTwentW;
    private float nineteenTwentH;

    void Awake()
    {
        uiSkin = Resources.Load("Skins/GeneralUI") as GUISkin;
        menuSkin = Resources.Load("Skins/InGameMenu") as GUISkin;
        score = 0;
        currentWave = 0;
        paused = false;
        gameSpeed = 1.0f;
        muted = false;

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
        ftFifteenWidth = width * 14 / 15;
        ftFifteenHeight = height * 14 / 15;
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
            play = Resources.Load("Textures/GUITextures/GameSpeed/16x9/Play") as Texture2D;
            faster = Resources.Load("Textures/GUITextures/GameSpeed/16x9/Faster") as Texture2D;
            fastest = Resources.Load("Textures/GUITextures/GameSpeed/16x9/Fastest") as Texture2D;
        }
        else
        {
            play = Resources.Load("Textures/GUITextures/GameSpeed/4x3/Play") as Texture2D;
            faster = Resources.Load("Textures/GUITextures/GameSpeed/4x3/Faster") as Texture2D;
            fastest = Resources.Load("Textures/GUITextures/GameSpeed/4x3/Fastest") as Texture2D;
        }
        
        coin = Resources.Load("Textures/GUITextures/Stats/Gold") as Texture2D;
        life = Resources.Load("Textures/GUITextures/Stats/Life") as Texture2D;
        pause = Resources.Load("Textures/GUITextures/MenuButtons/Pause") as Texture2D;
        sound = Resources.Load("Textures/GUITextures/MenuButtons/Sound") as Texture2D;
        noSound = Resources.Load("Textures/GUITextures/MenuButtons/NoSound") as Texture2D;
    }

    void OnGUI()
    {
        //PAUSE WINDOW
        if (paused)
        {
            pauseGame();
        }
        else
        {
            //SCORE
            GUI.BeginGroup(new Rect(0, 0, tenthWidth, twentiethHeight));

            GUI.Box(new Rect(0, 0, tenthWidth, twentiethHeight), "Score: " + score, uiSkin.box);

            GUI.EndGroup();

            //GAME SPEED + WAVES
            GUI.BeginGroup(new Rect(halfWidth - tenthWidth, 0, quarterWidth, tenthHeight));

            //CALL NEXT WAVE
            if (GUI.Button(new Rect(0, 0, twentiethWidth, tenthHeight), "Next Wave", uiSkin.button))
            {
            }

            //SPEED BUTTONS
            GUI.BeginGroup(new Rect(twentiethWidth, 0, fifthWidth, tenthHeight));
            if (GUI.Button(new Rect(0, 0, twentiethWidth, twentiethHeight), play, uiSkin.button))
            {
                Time.timeScale = 1;
            }

            if (GUI.Button(new Rect(twentiethWidth, 0, twentiethWidth, twentiethHeight), faster, uiSkin.button))
            {
                Time.timeScale = 2f;
            }

            if (GUI.Button(new Rect(tenthWidth, 0, twentiethWidth, twentiethHeight), fastest, uiSkin.button))
            {
                Time.timeScale = 3;
            }

            //Slider
            //gameSpeed = GUI.HorizontalSlider(new Rect(0, 0, fifthWidth, twentiethHeight), gameSpeed, 0.1f, 3.0f, uiSkin.horizontalSlider, uiSkin.horizontalSliderThumb);
            //Time.timeScale = gameSpeed;

            //WAVE INFO
            GUI.Box(new Rect(0, twentiethHeight, fifthWidth, twentiethHeight), "Wave: " + currentWave + " / " + maxWaves, uiSkin.box);

            GUI.EndGroup();
            GUI.EndGroup();

            //STATS
            GUI.BeginGroup(new Rect(fourFifthWidth, 0, fifthWidth, fifteenthHeight));

            GUI.Box(new Rect(0, 0, fifteenthHeight, fifteenthHeight), coin, uiSkin.box);
            GUI.Box(new Rect(fifteenthHeight, 0, tenthWidth - fifteenthHeight, fifteenthHeight), "" + gold, uiSkin.box);

            GUI.Box(new Rect(tenthWidth, 0, fifteenthHeight, fifteenthHeight), life, uiSkin.box);
            GUI.Box(new Rect(tenthWidth + fifteenthHeight, 0, tenthWidth - fifteenthHeight, fifteenthHeight), "" + lives, uiSkin.box);
            GUI.EndGroup();

            //PAUSE AND MENU BUTTONS
            GUI.BeginGroup(new Rect(fourFifthWidth, ftFifteenHeight, fifthWidth, fifteenthHeight));

            //Pause button
            if (GUI.Button(new Rect(0, 0, twentiethWidth, fifteenthHeight), pause, uiSkin.button))
            {
                paused = true;
                Time.timeScale = 0;
            }

            //Sound Button
            if (muted)
            {
                if (GUI.Button(new Rect(twentiethWidth, 0, twentiethWidth, fifteenthHeight), noSound, uiSkin.button))
                {
                    AudioListener.volume = 1.0f;
                    muted = false;
                }
            }
            else
            {
                if (GUI.Button(new Rect(twentiethWidth, 0, twentiethWidth, fifteenthHeight), sound, uiSkin.button))
                {
                    AudioListener.volume = 0.0f;
                    muted = true;
                }
            }

            GUI.EndGroup();
        }
    }

    private void pauseGame()
    {
        GUI.Window(0, new Rect(thirdWidth, thirdHeight, thirdWidth, thirdHeight), pausedWindow, "PAUSED", menuSkin.window);
    }

    private void pausedWindow(int windowID)
    {
        if (GUI.Button(new Rect(ninthWidth, ninthHeight, ninthWidth, ninthHeight), "GET BACK TO THE FIGHT!", menuSkin.button))
        {
            paused = false;
            Time.timeScale = 1;
        }
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
