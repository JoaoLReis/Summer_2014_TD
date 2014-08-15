using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

[CustomEditor(typeof(HexTileMap))]
public class UberHexEditor : Editor
{
    HexTileMap tileMap;

    //Vector of vectors3 to use in mathematical operations
    Vector3[] rect = new Vector3[6];

    Matrix4x4 worldToLocal;

    bool editing;
    bool showPath = false;

    int direction;

    Hex KselectedHex;
    Hex LselectedHex;

    List<Hex> path = new List<Hex>();

    void Awake()
    {
        KselectedHex = ScriptableObject.CreateInstance("Hex") as Hex;
        KselectedHex.init(new Vector3(0,0,0),0,0);
        LselectedHex = ScriptableObject.CreateInstance("Hex") as Hex;
        LselectedHex.init(new Vector3(0, 0, 0), 0, 0);
        tileMap = (HexTileMap)target;
    }

    #region Inspector GUI
        public override void OnInspectorGUI()
        {
            //Get tilemap
            if (tileMap == null)
            {
                //tileMap = GameObject.Find("UberTiles").GetComponent<TileMap>();
                tileMap = (HexTileMap)target;
                Caching.CleanCache();
                tileMap.Hexes = new Hex[tileMap.gridSize * tileMap.gridSize];
            }

            //Toggle editing mode
            if (editing)
            {
                if (GUILayout.Button("Stop Editing"))
                {
                    editing = false;
                    //StaticBatchingUtility.Combine(tileMap.gameObject);
                    Caching.CleanCache();
                    EditorUtility.SetDirty(tileMap);                  
                }
                if (GUILayout.Button("Generate Hex matrix clears everything"))
                {
                    Caching.CleanCache();
                    tileMap.Hexes = new Hex[tileMap.gridSize * tileMap.gridSize];
                    GenerateHexGrid();
                }
                if (GUILayout.Button("Generate Hex neighbours"))
                {
                    GenerateHexNeighbours();
                }
                if (GUILayout.Button("Generate HexPath neighbours"))
                {
                    GenerateHexPathNeighbours();
                }
            }
            else if (GUILayout.Button("Edit TileMap"))
            {
                editing = true;
                //tileMap.Hexes = new Hex[tileMap.gridSize + 1, tileMap.gridSize + 1];
                //GenerateHexGrid();
            }             

            //Tile Size
		    EditorGUI.BeginChangeCheck();
		    var newTileSize = EditorGUILayout.FloatField("Tile Size", tileMap.tileSize);
		    if (EditorGUI.EndChangeCheck())
		    {
			    tileMap.tileSize = newTileSize;
		    }

            //Grid Size
		    EditorGUI.BeginChangeCheck();
		    var newGridSize = EditorGUILayout.IntField("Grid Size", tileMap.gridSize);
		    if (EditorGUI.EndChangeCheck())
		    {
			    tileMap.gridSize = newGridSize;
                tileMap.Hexes = new Hex[newGridSize * newGridSize];
		    }

            //Tile Prefab
            EditorGUI.BeginChangeCheck();
            var newTilePrefab = (Transform)EditorGUILayout.ObjectField("Tile Prefab", tileMap.tilePrefab, typeof(Transform), false);
            if (EditorGUI.EndChangeCheck())
            {
                tileMap.tilePrefab = newTilePrefab;
            }

            //Tile Map
            EditorGUI.BeginChangeCheck();
            var newTileSet = (HexSet)EditorGUILayout.ObjectField("Tile Set", tileMap.hexSet, typeof(HexSet), false);
            if (EditorGUI.EndChangeCheck())
            {
                tileMap.hexSet = newTileSet;
            }

            //Tile Prefab selector
            if (tileMap.hexSet != null)
            {
                EditorGUI.BeginChangeCheck();
                var names = new string[tileMap.hexSet.prefabs.Length + 1];
                var values = new int[names.Length + 1];
                names[0] = tileMap.tilePrefab != null ? tileMap.tilePrefab.name : "";
                values[0] = 0;
                for (int i = 1; i < names.Length; i++)
                {
                    names[i] = tileMap.hexSet.prefabs[i - 1] != null ? tileMap.hexSet.prefabs[i - 1].name : "";
                    //if (i < 10)
                    //	names[i] = i + ". " + names[i];
                    values[i] = i;
                }
                var index = EditorGUILayout.IntPopup("Select Tile", 0, names, values);
                if (EditorGUI.EndChangeCheck() && index > 0)
                {
                    tileMap.tilePrefab = tileMap.hexSet.prefabs[index - 1];
                }
            }

            //Selecting direction
            EditorGUILayout.BeginHorizontal(GUILayout.Width(60));
            EditorGUILayout.PrefixLabel("Direction");
            EditorGUILayout.BeginVertical(GUILayout.Width(20));
            GUILayout.Space(20);
            if (direction == 3)
                GUILayout.Box("<", GUILayout.Width(20));
            else if (GUILayout.Button("<"))
                direction = 3;
            GUILayout.Space(20);
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(GUILayout.Width(20));
            if (direction == 0)
                GUILayout.Box("^", GUILayout.Width(20));
            else if (GUILayout.Button("^"))
                direction = 0;
            if (direction == -1)
                GUILayout.Box("?", GUILayout.Width(20));
            else if (GUILayout.Button("?"))
                direction = -1;
            if (direction == 2)
                GUILayout.Box("v", GUILayout.Width(20));
            else if (GUILayout.Button("v"))
                direction = 2;
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(GUILayout.Width(20));
            GUILayout.Space(20);
            if (direction == 1)
                GUILayout.Box(">", GUILayout.Width(20));
            else if (GUILayout.Button(">"))
                direction = 1;
            GUILayout.Space(20);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (GUI.changed)
                EditorUtility.SetDirty(tileMap); 
        }
    #endregion

    #region Scene GUI
        void OnSceneGUI()
        {
            if (tileMap == null)
                tileMap = (HexTileMap)target;
            if (editing)
            {
                UpdateMatrices();
                HexUpdate();
                MouseUpdate();    
            }
        }
    #endregion

    #region Update
        void UpdateMatrices()
        {
            Handles.matrix = tileMap.transform.localToWorldMatrix;
            worldToLocal = tileMap.transform.worldToLocalMatrix;
        }

        void HexUpdate()
        {
            DrawHexgrid();
            DrawCheckedTiles();
            DrawSelectedTile();
            DrawPathTestingHexesAndPath();
            HandleUtility.Repaint();
        }

        void MouseUpdate()
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                var ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x, Camera.current.pixelHeight - Event.current.mousePosition.y));
                float hit;
                Plane plane = new Plane(tileMap.transform.up, tileMap.transform.position);
                if (!plane.Raycast(ray, out hit))
                {
                    Debug.Log("Couldnt intersect plane");
                    return;
                }
                //tileMap.checkedHexes.Clear();
                var mousePosition = worldToLocal.MultiplyPoint(ray.GetPoint(hit));

                //Carefull with C# copy operations!
                Hex closest = tileMap.HexFromWorld(mousePosition);
                //Debug.Log(closest.position);
                tileMap.selectedTile = closest;
            }
            else if (Event.current.keyCode == (KeyCode.B) && Event.current.type == EventType.KeyDown)
            {
                GenerateAndPlaceTile(tileMap.selectedTile);
                Event.current.Use();
            }
            else if (Event.current.keyCode == (KeyCode.D) && Event.current.type == EventType.KeyDown)
            {
                DeleteSelectedHexTile();
                Event.current.Use();
            }
            else if (Event.current.keyCode == (KeyCode.J) && Event.current.type == EventType.KeyDown)
            {
                Debug.Log(tileMap.Hexes.Length);
                tileMap.debugSelectedHex();
                Event.current.Use();
            }
            else if (Event.current.keyCode == (KeyCode.K) && Event.current.type == EventType.KeyDown)
            {
                KselectedHex = tileMap.selectedTile;
                Event.current.Use();
            }
            else if (Event.current.keyCode == (KeyCode.L) && Event.current.type == EventType.KeyDown)
            {
                LselectedHex = tileMap.selectedTile;
                Event.current.Use();
            }
            else if (Event.current.keyCode == (KeyCode.P) && Event.current.type == EventType.KeyDown)
            {
                showPath = !showPath;
                tileMap.initSearchLists();
                path = tileMap.searchPath(KselectedHex, LselectedHex);
                Event.current.Use();
            }
        }
    #endregion

    #region DeleteFunctions
        void DeleteSelectedHexTile()
        {
            Debug.Log("am i deleating anything?_> "+tileMap.selectedTile.instance);
            DestroyImmediate(tileMap.selectedTile.instance);
            tileMap.updateHex(tileMap.selectedTile);
        }
    #endregion

    #region GenerateFunctions
        void GenerateAndPlaceTile(Hex hex)
        {
            //check if the tile is already built
            if (hex.hasInstance())
                return;
            //Place the tile
            var instance = (Transform)PrefabUtility.InstantiatePrefab(tileMap.tilePrefab);
            //instance.gameObject.isStatic = true;
            instance.parent = tileMap.transform;
            instance.localPosition = hex.position;
            hex.instance = instance.gameObject;
            defineHexTileType(hex);
            tileMap.updateHex(hex);
            
            //instance.localRotation = Quaternion.identity;//*Quaternion.Euler(0, tileMap.directions[index] * 90, 0);
        }

        void GenerateHexGrid()
        {
            var currX = tileMap.transform.position.x;
            var currZ = tileMap.transform.position.z;

            bool pair = true;

            var b = tileMap.tileSize;
            var a = tileMap.tileSize;

            var r = a / 2.0f;
            var h = r * Mathf.Sin(30 * Mathf.PI / 180);
            var sizeS = h / Mathf.Sin(30 * Mathf.PI / 180);
            var xoffset = b / 2.0f + sizeS / 2.0f;
            var zoffset = currZ + a / 2.0f;
            tileMap.xOffset = xoffset;
            tileMap.zOffset = zoffset;
            for (int x = 0, z = 0; x < tileMap.gridSize; x++)
            {
                for (; z < tileMap.gridSize; z++)
                {
                    DrawHex(currX, currZ, Color.white, sizeS, h, r, a);
                    var index = x * tileMap.gridSize + z;

                    tileMap.Hexes[index] = ScriptableObject.CreateInstance("Hex") as Hex;
                    tileMap.Hexes[index].init(new Vector3(currX, 0, currZ), z, x);
                    currZ += tileMap.tileSize;
                }

                if (pair)
                {
                    currX += xoffset;
                    currZ = zoffset;
                    pair = false;
                }
                else
                {
                    currZ = tileMap.transform.position.z;
                    currX += xoffset;
                    pair = true;
                }
                //Atention using "odd-r" horizontal layout
                
                z = 0;
            }
        }

        bool isLow(Hex hex)
        {
            return (hex.type == TileType.WATER_L || hex.type == TileType.NATURE_L || hex.type == TileType.FIRE_L);
        }

        bool isMed(Hex hex)
        {
            return (hex.type == TileType.WATER_M || hex.type == TileType.NATURE_M || hex.type == TileType.FIRE_M);
        }

        bool isHigh(Hex hex)
        {
            return (hex.type == TileType.WATER_H || hex.type == TileType.NATURE_H || hex.type == TileType.FIRE_H);
        }

        bool isRamp(Hex hex)
        {
            return (hex.type == TileType.RAMP_H || hex.type == TileType.RAMP_L);
        }

        bool existsConnection(Hex curr, Hex next)
        {
            if(isLow(curr))
            {
                if(isLow(next))
                {
                    return true;
                }
                else if(next.type == TileType.RAMP_L)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(isMed(curr))
            {
                if(isMed(next))
                {
                    return true;
                }
                else if (isRamp(next))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (isHigh(curr))
            {
                if(isHigh(next))
                {
                    return true;
                }
                else if (next.type == TileType.RAMP_H)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (curr.type == TileType.RAMP_L)
            {
                if (isMed(next))
                {
                    return true;
                }
                else if (next.type == TileType.RAMP_H)
                {
                    return true;
                }
                else if (isLow(next))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (curr.type == TileType.RAMP_H)
            {
                if (isHigh(next))
                {
                    return true;
                }
                else if (next.type == TileType.RAMP_L)
                {
                    return true;
                }
                else if (isMed(next))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        void defineHexTileType(Hex hex)
        {
            if(hex.instance != null)
            {
                if (hex.instance.name.Contains("ramp_L"))
                {
                    hex.type = TileType.RAMP_L;
                }
                else if (hex.instance.name.Contains("ramp_H"))
                {
                    hex.type = TileType.RAMP_H;
                }
                else if (hex.instance.name.Contains("Ice"))
                {
                    if (hex.instance.name.Contains("low"))
                    {
                        hex.type = TileType.WATER_L;
                    }
                    else if (hex.instance.name.Contains("med"))
                    {
                        hex.type = TileType.WATER_M;
                    }
                    else
                    {
                        hex.type = TileType.WATER_H;
                    }
                }
                else if (hex.instance.name.Contains("Fire"))
                {
                    if (hex.instance.name.Contains("low"))
                    {
                        hex.type = TileType.FIRE_L;
                    }
                    else if (hex.instance.name.Contains("med"))
                    {
                        hex.type = TileType.FIRE_M;
                    }
                    else
                    {
                        hex.type = TileType.FIRE_H;
                    }
                }
                else if (hex.instance.name.Contains("Nature"))
                {
                    if (hex.instance.name.Contains("low"))
                    {
                        hex.type = TileType.NATURE_L;
                    }
                    else if (hex.instance.name.Contains("med"))
                    {
                        hex.type = TileType.NATURE_M;
                    }
                    else
                    {
                        hex.type = TileType.NATURE_H;
                    }
                }
            }
        }

        /*List<Hex> GeneratePathNeighborListFromHex(Hex hex)
        {
            //Regen tile type just in case....
            defineHexTileType(hex);
            List<Hex> pathList = new List<Hex>();
            for (int i = 0; i < tileMap.Hexes[currentI].getList().Count; i++)
            {
                if (existsConnection(curHex, tileMap.Hexes[currentI].getList()[i]))
                {
                    pathList.Add(tileMap.Hexes[currentI].getList()[i]);
                }
            }
        }*/

        void GenerateHexPathNeighbours()
        {
           var currX = tileMap.transform.position.x;
            var currZ = tileMap.transform.position.z;

            bool pair = true;

            for (int x = 0, z = 0; x < tileMap.gridSize; x++)
            {
                for (; z < tileMap.gridSize; z++)
                {
                    var currentI = x * tileMap.gridSize + z;
                    Hex curHex = tileMap.Hexes[currentI];


                    //Regen tile type just in case....
                    defineHexTileType(curHex);

                    /*if (tileMap.Hexes[currentI].getPathList() != null)
                    {
                        tileMap.Hexes[currentI].getPathList().Clear();
                    }
                    else
                    {
                        tileMap.Hexes[currentI].genPathList();
                    }*/

                    for(int i = 0; i < tileMap.Hexes[currentI].getList().Count; i++)
                    {
                        var testedI = tileMap.Hexes[currentI].getList()[i].getValue()* tileMap.gridSize + tileMap.Hexes[currentI].getList()[i].getKey();
                        Hex testedHex = tileMap.Hexes[testedI];
                        if (existsConnection(curHex, testedHex))
                        {
                            curHex.addPathHex(tileMap.Hexes[currentI].getList()[i]);
                        }
                    }
                    /*foreach (Hex hex in )
                    {
                        if (existsConnection(curHex, hex))
                        {
                            curHex.addPathHex(hex);
                        }
                    }*/
                    tileMap.updateHex(curHex);
                }
                if (pair)
                {
                    pair = false;
                }
                else
                {
                    pair = true;
                }

                //Atention using "odd-r" horizontal layout...

                z = 0;
            }
        }

        void GenerateHexNeighbours()
        {
            Debug.Log("SIZE");
            Debug.Log(tileMap.Hexes.Length);
            //Debug.Log("Total allocated memory : " + System.GC.GetTotalMemory(true) + " bytes");
            var currX = tileMap.transform.position.x;
            var currZ = tileMap.transform.position.z;

            bool pair = true;

            for (int x = 0, z = 0; x < tileMap.gridSize; x++)
            {
                for (; z < tileMap.gridSize; z++)
                {
                    var currentI = x * tileMap.gridSize + z;
                    
                    Hex tmp = tileMap.Hexes[currentI];
                    if (tmp.getList().Count > 0)
                    {
                        tmp.getList().Clear();
                    }                      
                    else
                    {
                        if(tmp.getList() == null)
                            tmp.genList();
                    }
                    
                    if (pair)
                    {
                        var index = (z +1) + tileMap.gridSize * x;
                        if (x < tileMap.gridSize && index < tileMap.Hexes.Length)
                            tmp.addHex(tileMap.Hexes[index].zx);

                            index = z + tileMap.gridSize * (x - 1);
                            if (x > 0.0f && index < tileMap.Hexes.Length)
                                tmp.addHex(tileMap.Hexes[index].zx);

                                index = (z - 1) + tileMap.gridSize * (x - 1);
                                if (x > 0.0f && z > 0.0f && index < tileMap.Hexes.Length)
                                    tmp.addHex(tileMap.Hexes[index].zx);

                                    index = (z - 1) + tileMap.gridSize * x;
                                    if (z > 0.0f && index < tileMap.Hexes.Length)
                                        tmp.addHex(tileMap.Hexes[index].zx);

                                        index = (z - 1) + tileMap.gridSize * (x + 1);
                                        if (x < tileMap.gridSize && z > 0.0f && index < tileMap.Hexes.Length)
                                            tmp.addHex(tileMap.Hexes[index].zx);

                                            index = z + tileMap.gridSize * (x + 1);
                                            if (x < tileMap.gridSize && index < tileMap.Hexes.Length)
                                                tmp.addHex(tileMap.Hexes[index].zx);              
                    }
                    else
                    {
                        var index = (z + 1) + tileMap.gridSize * x;
                        if (z < tileMap.gridSize && index < tileMap.Hexes.Length)
                            tmp.addHex(tileMap.Hexes[index].zx);

                            index = (z + 1) + tileMap.gridSize * (x - 1);
                            if (x > 0.0f && z < tileMap.gridSize && index < tileMap.Hexes.Length)
                                tmp.addHex(tileMap.Hexes[index].zx);

                                index = z + tileMap.gridSize * (x - 1);
                                if (x > 0.0f && x > 0.0f && index < tileMap.Hexes.Length)
                                    tmp.addHex(tileMap.Hexes[index].zx);

                                    index = (z - 1) + tileMap.gridSize * x;
                                    if (z > 0.0f && index < tileMap.Hexes.Length)
                                        tmp.addHex(tileMap.Hexes[index].zx);

                                        index = (z + 1) + tileMap.gridSize * (x + 1);
                                        if (x < tileMap.gridSize && z < tileMap.gridSize && index < tileMap.Hexes.Length)
                                            tmp.addHex(tileMap.Hexes[index].zx);

                                            index = z + tileMap.gridSize * (x + 1);
                                            if (x < tileMap.gridSize && index < tileMap.Hexes.Length)
                                                tmp.addHex(tileMap.Hexes[index].zx);

                    }
                    //Debug.Log("SIZE-neighbor!!:");
                    //Debug.Log(tmp.getList().Count);
                    //tileMap.updateHex(tmp);
                }

                if (pair)
                {
                    pair = false;
                }
                else
                {
                    pair = true;
                }

                //Atention using "odd-r" horizontal layout

                z = 0;
            }
            //Debug.Log("FINISHED!!!!!");
           // Debug.Log("SIZE");
            //Debug.Log(tileMap.Hexes.Length);
            //Debug.Log("Total allocated memory : " + System.GC.GetTotalMemory(true) + " bytes");
            //System.GC.Collect();
        }
    #endregion

    #region DrawFunctions
        void DrawCheckedTiles()
        {
            if(tileMap.checkedHexes != null)
                foreach (HexKeyValueInt zx in tileMap.checkedHexes)
                {
                    Hex hex = tileMap.Hexes[zx.getValue() * tileMap.gridSize + zx.getKey()];
                    DrawRect(hex.position.x, hex.position.z, tileMap.tileSize, tileMap.tileSize, Color.white, Color.red);     
                }           
        }

        void DrawPathTestingHexesAndPath()
        {
            if (showPath)
            {
                DrawRect(KselectedHex.position.x, KselectedHex.position.z, tileMap.tileSize, tileMap.tileSize, Color.white, Color.blue);
                DrawRect(LselectedHex.position.x, LselectedHex.position.z, tileMap.tileSize, tileMap.tileSize, Color.white, Color.green);

                if (tileMap.searchedHexes != null)
                    foreach (Hex hex in tileMap.searchedHexes)
                {
                    DrawRect(hex.position.x, hex.position.z, tileMap.tileSize, tileMap.tileSize, Color.white, Color.yellow);
                } 
                /*if (path != null)
                    foreach (Hex hex in path)
                    {
                        DrawRect(hex.position.x, hex.position.z, tileMap.tileSize, tileMap.tileSize, Color.white, Color.yellow);
                    }  */  
            }
        }

        void DrawSelectedTile()
        {
            DrawRect(tileMap.selectedTile.position.x, tileMap.selectedTile.position.z, tileMap.tileSize, tileMap.tileSize, Color.white, Color.green);
        }

        void DrawRect(float x, float z, float sizeX, float sizeZ, Color outline, Color fill)
        {
            Handles.color = Color.white;
            var size = 0.2f * tileMap.tileSize;
            rect[0].Set(x - size, 0, z - size);
            rect[1].Set(x + size, 0, z - size);
            rect[2].Set(x + size, 0, z + size);
            rect[3].Set(x - size, 0, z + size);
            Handles.DrawSolidRectangleWithOutline(rect, fill, outline);
        }

        void DrawHexgrid()
        {
            var currX = tileMap.transform.position.x;
            var currZ = tileMap.transform.position.z;

            bool pair = true;

            
            var a = tileMap.tileSize;

            var r = a / 2.0f;
            var h = r * Mathf.Sin(30 * Mathf.PI / 180);
            var sizeS = h / Mathf.Sin(30 * Mathf.PI / 180);
            var b = sizeS + 2 * h;
            var xoffset =  b / 2.0f + sizeS / 2.0f;
            var zoffset = currZ + a / 2.0f;
            tileMap.xOffset = xoffset;
            tileMap.zOffset = zoffset;
            for (int x = 0; x < tileMap.gridSize; x++)
            {
                for (int z = 0; z < tileMap.gridSize; z++)
                {
                    DrawHex(currX, currZ, Color.white, sizeS, h, r, a);
                    currZ += tileMap.tileSize; 
                }
                if(pair)
                {
                    currX += xoffset;
                    currZ = zoffset;
                    pair = false;
                }
                else
                {
                    currZ = tileMap.transform.position.z;
                    currX += xoffset;
                    pair = true;
                }
            }
        }

        void DrawHex(float x, float z, Color fill, float sizeS, float h, float r, float a)
        {
            Handles.color = fill;
            
            var xf = (float)x  - sizeS / 2;
            var zf = (float)z  - a / 2.0f;

            rect[0].Set(xf, 0, zf);
            rect[1].Set(xf + sizeS, 0, zf);
            rect[2].Set(xf + sizeS + h, 0, zf + r);
            rect[3].Set(xf + sizeS, 0, zf + r + r);
            rect[4].Set(xf, 0, zf + r + r);
            rect[5].Set(xf - h, 0, zf + r);

            Handles.DrawLine(rect[0], rect[1]);
            Handles.DrawLine(rect[1], rect[2]);
            Handles.DrawLine(rect[2], rect[3]);
            Handles.DrawLine(rect[3], rect[4]);
            Handles.DrawLine(rect[4], rect[5]);
            Handles.DrawLine(rect[5], rect[0]);

            //This update should not be here, hexRadius must be updated sooner on the pipeline.
            tileMap.hexRadius = Vector3.Distance(new Vector3(x, 0, z) ,rect[3]);
        }
    #endregion
}
