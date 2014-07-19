using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class HexTileMap : MonoBehaviour
{
    GameState game;

    public float tileSize = 1;
    public int gridSize = 4;
    public Hex[] Hexes;
    public List<Hex> checkedHexes = new List<Hex>();
    public Hex selectedTile;
    public float xOffset, zOffset;
    public float hexRadius;
    public Transform tilePrefab;
	public HexSet hexSet;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<GameState>();
    }

    void Update()
    {

    }

    #region Hex Functions
        public void updateHex(Hex changeTo)
        {
            var index = changeTo.x * gridSize + changeTo.z;
            Hexes[index] = changeTo;
            Debug.Log("CHECK [" + changeTo.z + "][" + changeTo.x + "]_> " + Hexes[index].instance);
        }

        public void debugSelectedHex()
        {
            //Debug.Log("****start****");
            Debug.Log("tile has?_> "+selectedTile.instance);
            /*Debug.Log(selectedTile.position);
            Debug.Log("****end****");
            Debug.Log("****start2****");
            Debug.Log(Hexes[selectedTile.z, selectedTile.x].instance);
            Debug.Log(Hexes[selectedTile.z, selectedTile.x].position);
            Debug.Log("****end2****");*/
        }

        public Hex HexFromWorld(Vector3 mousePosition)
        {
            float ix = mousePosition.z;
            float iy = mousePosition.x;

            var col_guess = Mathf.RoundToInt(ix / tileSize);
            var row_guess = Mathf.RoundToInt(iy / xOffset);
            Hex closest = new Hex(new Vector3(float.MaxValue, float.MaxValue, float.MaxValue), 0, 0);
            int fx = 0;
            int fz = 0;
            int hexChecks = 0;
            for (int z = col_guess - 1; z <= col_guess + 1; z++)
            {
                for (int x = row_guess - 1; x <= row_guess + 1; x++)
                {
                    //we dont work with negatives and must be carefull with the gridsize
                    if (x >= 0f && z >= 0f && x <= gridSize && z <= gridSize)
                    {
                        var index = x * gridSize + z;
                        if(index < Hexes.Length)
                        {
                            checkedHexes.Add(Hexes[index]);
                            hexChecks++;

                            if (Vector3.Distance(Hexes[index].position, mousePosition) < Vector3.Distance(closest.position, mousePosition))
                            {
                                fx = x;
                                fz = z;
                                closest = Hexes[index];
                            }
                        }
                    }
                }
            }
            //Do a performance check!
            //Debug.Log("hexChecks -> " + hexChecks);

            //Debug.Log(new Vector2(fx, fz));
            //Debug.Log(closest.position);
            

            return closest;
        }

        public Vector3 hex_round(Vector3 cube)
        {
            float rx = Mathf.Round(cube.x);
            float ry = Mathf.Round(cube.y);
            float rz = Mathf.Round(cube.z);

            float x_diff = Mathf.Abs(rx - cube.x);
            float y_diff = Mathf.Abs(ry - cube.y);
            float z_diff = Mathf.Abs(rz - cube.z);

            if (x_diff > y_diff && x_diff > z_diff)
                rx = -ry-rz;
            else if (y_diff > z_diff)
                ry = -rx-rz;
            else
                rz = -rx-ry;
            return new Vector3(rx, ry, rz);
        }
    #endregion

    #region dead code
    //pixel to axial coordinate system!
    /*float size = hexRadius; // cell size
    float q = (float)((1f / 3f * Mathf.Sqrt(3) * worldCoordinates.x - 1f / 3f * worldCoordinates.z) / size);
    float r = 2f / 3f * worldCoordinates.z / size;
    Debug.Log("ODD->"+r);*/
    //convert odd-r offset to cube
    /*float x = q - (r - ((int)r & 1)) / 2;
    float z = r;
    float y = -x - z;
    Vector3 cube = new Vector3 (x, y, z);
    */
    //Round it !
    /*cube = hex_round(cube);
    Debug.Log("Cube->" + cube.z);*/
    //convert cube to odd-r offset
    /*q = cube.x + (cube.z - ((int)cube.z & 1)) / 2;
    r = cube.z;
    return new Vector2(r, q);*/
    /*  //pixel to axial coordinate system!
                    float size = tileMap.hexRadius; // cell size

                    float q = ((1f / 3f * Mathf.Sqrt(3f) * ix - 1f / 3f * iy) / size);
                    float r = 2f / 3f * iy / size;
                    //q -= 6f;
                    //r -= 1.5f;
                    

                    //axial to cube
                    var x = q;
                    var z = r;
                    var y = -x-z;
                    Vector3 cube = new Vector3(x, y, z);
                    //Round it !
                    cube = tileMap.hex_round(cube);

                    //convert odd-r offset to cube
                    /*float x = (float)(q - (r - (r % 2)) / 2f);
                    float z = (float)r;
                    float y = -x - z;
                   
                    //convert cube to odd-r offset
                    var fq = cube.x + (cube.z - (cube.z % 2)) / 2f;
                    var fr = cube.z;
                    //fq += 6f;
                    //fr += 1.5f;
                    v =  new Vector2((float)fq, (float)fr);
                    //Debug.Log(Time.realtimeSinceStartup+"Left-Mouse Down");
                    Debug.Log(mousePosition);
                    Debug.Log(v);
                    if(fq >= 0f && fr >= 0f && fq <= tileMap.gridSize && fr <= tileMap.gridSize)
                    {
                        tileMap.selectedTile.position = tileMap.Hexes[(int)fq,(int)fr].position;
                    }*/
    #endregion
}
