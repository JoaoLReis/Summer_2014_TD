using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UberMenuUi : MonoBehaviour {

    //      Enum that dictates in which state we are in
    public enum MenuState {DEFAULT, LEVEL_SELECT, OPTIONS, CREDITS};
    private MenuState state;

    //      Base resolution - textures are designed for this resolution
    private float baseResolutionX = 1920;
    private float baseResolutionY = 1080;

    public List<string> OptionNames;
    public List<Texture> OptionSymbols;

    public List<string> Levels;
    public List<Texture> LevelTextures;
    public GUIStyle style, ButtonStyle;

    private float startX, startY;

    public float individualXsize, individualYsize;
    public float XpixelInlaySize;
    public float YpixelInlaySize;
    public Texture bigBox;

    private float pickPercentage = 0.6f;
    private float labelPercentage = 0.3f;
    private float descriptionPercentage = 0.1f;


    void Start()
    {
        var horizRatio = Screen.width / baseResolutionX;
        var vertRatio = Screen.height / baseResolutionY;

        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(horizRatio, vertRatio, 1));
        state = MenuState.DEFAULT;
    }

    void OnGUI()
    {
        switch(state)
        {
            case MenuState.DEFAULT:
                DrawDefault();
                break;
            case MenuState.LEVEL_SELECT:
                DrawLevelSelect();
                break;
            case MenuState.OPTIONS:
                DrawOptions();
                break;
            case MenuState.CREDITS:
                DrawCredits();
                break;
            default:
                break;
        }
    }

    void DrawDefault()
    {
        float startX = Screen.width / 2.0f - OptionNames.Count * individualXsize / 2.0f;
        float startY = Screen.height / 2.0f - individualYsize / 2.0f;
        float posX = startX, posY = startY;
        for (int i = 0; i < OptionNames.Count; i++)
        {
            DrawOptionArea(posX, posY, individualXsize, individualYsize, OptionNames[i], OptionSymbols[i]);
            posX += individualXsize;
        }  
    }

    void DrawLevelSelect()
    {
        float startX = Screen.width / 2.0f - Levels.Count * individualXsize / 2.0f;
        float startY = Screen.height / 2.0f - individualYsize / 2.0f;
        float posX = startX, posY = startY;
        for (int i = 0; i < Levels.Count; i++)
        {
            DrawLevelList(posX, posY, individualXsize, individualYsize, Levels[i], LevelTextures[i]);
            posX += individualXsize;
        }  
    }

    void DrawOptions()
    {
        float startX = Screen.width / 2.0f - OptionNames.Count * individualXsize / 2.0f;
        float startY = Screen.height / 2.0f - individualYsize / 2.0f;
        float posX = startX, posY = startY;
        Rect area = new Rect(posX, posY, individualXsize, individualYsize);
        if (GUI.Button(area, "back"))
        {
            state = MenuState.DEFAULT;
        }
    }

    void DrawCredits()
    {
        float startX = Screen.width / 2.0f - OptionNames.Count * individualXsize / 2.0f;
        float startY = Screen.height / 2.0f - individualYsize / 2.0f;
        float posX = startX, posY = startY;
        Rect area = new Rect(posX, posY, individualXsize, individualYsize);
        if (GUI.Button(area, "back"))
        {
            state = MenuState.DEFAULT;
        }
    }

    void DrawOptionArea(float x, float y, float Xsize, float Ysize, string text, Texture texture)
    {
        Vector2 stringSize = style.CalcSize(new GUIContent(text));
        Rect area = new Rect(x, y, Xsize, Ysize);
        GUILayout.BeginArea(area);
            Rect optionBox = new Rect(0.0f, 0.0f, Xsize, Ysize);
            GUI.DrawTexture(optionBox, bigBox, ScaleMode.StretchToFill); 
            GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    //      Pick box
                    Rect pickBox = new Rect(XpixelInlaySize, YpixelInlaySize, Xsize - 2 * XpixelInlaySize, pickPercentage * Ysize - YpixelInlaySize);
                    //GUI.Box(pickBox, ""); 
                    if (GUI.Button(pickBox, texture, ButtonStyle))
                    {
                        if(text.Equals("PLAY"))
                        {
                            state = MenuState.LEVEL_SELECT;
                        }
                        else if (text.Equals("OPTIONS"))
                        {
                            state = MenuState.OPTIONS;
                        }
                        else if (text.Equals("CREDITS"))
                        {
                            state = MenuState.CREDITS;
                        }
                    }
                        //      Option Label Name
                    GUI.Box(new Rect(Xsize / 2.0f - stringSize.x / 2.0f, pickPercentage * Ysize + YpixelInlaySize, Xsize, labelPercentage * Ysize), text, style);

                        //      Description
                    Rect description = new Rect(XpixelInlaySize, pickPercentage * Ysize + labelPercentage * Ysize, Xsize - 2 * XpixelInlaySize, descriptionPercentage * Ysize - YpixelInlaySize);
                    //GUI.DrawTexture(description, bigBox, ScaleMode.StretchToFill); 
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void DrawLevelList(float x, float y, float Xsize, float Ysize, string text, Texture texture)
    {
        Vector2 stringSize = style.CalcSize(new GUIContent(text));
        Rect area = new Rect(x, y, Xsize, Ysize);
        GUILayout.BeginArea(area);
            Rect optionBox = new Rect(0.0f, 0.0f, Xsize, Ysize);
            GUI.DrawTexture(optionBox, bigBox, ScaleMode.StretchToFill);
            GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    //      Pick box
                    Rect pickBox = new Rect(XpixelInlaySize, YpixelInlaySize, Xsize - 2 * XpixelInlaySize, pickPercentage * Ysize - YpixelInlaySize);
                    //GUI.Box(pickBox, ""); 
                    if (GUI.Button(pickBox, texture, ButtonStyle))
                    {
                        Application.LoadLevel(""+text+""); 
                    }
                    //      Option Label Name
                    GUI.Box(new Rect(Xsize / 2.0f - stringSize.x / 2.0f, pickPercentage * Ysize + YpixelInlaySize, Xsize, labelPercentage * Ysize), text, style);

                    //      Description
                    Rect description = new Rect(XpixelInlaySize, pickPercentage * Ysize + labelPercentage * Ysize, Xsize - 2 * XpixelInlaySize, descriptionPercentage * Ysize - YpixelInlaySize);
                    //GUI.DrawTexture(description, bigBox, ScaleMode.StretchToFill); 
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}