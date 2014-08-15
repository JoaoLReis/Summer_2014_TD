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
    public List<HexKeyValueInt> checkedHexes = new List<HexKeyValueInt>();
    public Hex selectedTile;
    public float xOffset, zOffset;
    public float hexRadius;
    public Transform tilePrefab;
	public HexSet hexSet;

    //Search lists
    public List<Hex> searchedHexes;
    public List<Hex> toBeEvaluated;
    //private List<Hex> navigatedHexes;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<GameState>();
        initSearchLists();
    }

    #region Search Functions
        public void initSearchLists()
        {
            searchedHexes = new List<Hex>();
            toBeEvaluated = new List<Hex>();
            //navigatedHexes = new List<Hex>();
        }

        public void clearSearchLists()
        {
            searchedHexes.Clear();
            toBeEvaluated.Clear();
            //navigatedHexes.Clear();
        }

        float heuristic_cost_estimate(Hex origin, Hex destination)
        {
            return (destination.position - origin.position).sqrMagnitude;
        }

        Hex getLowestFScore(List<Hex> list)
        {
            Hex lowest = toBeEvaluated[0];
            for (int i = 0; i < list.Count; i++)
            {
                Hex hex = list[i];
                if (hex.f_score <= lowest.f_score)
                    lowest = hex;
            }
            return lowest;
        }

        // Reconstructs a path list leading to a goal.
        List<Hex> reconstruct_path(Hex goal)
        {
            List<Hex> path = new List<Hex>();
            Hex hex = goal;
            while(hex != null)
            {
                path.Add(hex);
                hex = hex.cameFrom;
            }
            return path;
        }

        public List<Hex> searchPath(Hex origin, Hex destination)
        {
            clearSearchLists();

            origin.g_score = 0;
            origin.f_score = origin.g_score + heuristic_cost_estimate(origin, destination);
            toBeEvaluated.Add(origin);

            while(toBeEvaluated.Count != 0)
            {
                Hex current = getLowestFScore(toBeEvaluated);
                if(Vector3.Distance(current.position, destination.position) <= 0.01f)
                {
                    return reconstruct_path(current);
                }

                toBeEvaluated.Remove(current);
                searchedHexes.Add(current);
                Debug.Log("searched: "+current.position);
                Debug.Log("neighborcount: " + current.getPathList().Count);
                foreach (HexKeyValueInt zx in current.getPathList())
                {
                    Hex neighbor = Hexes[zx.getValue() *gridSize + zx.getKey()];
                    Debug.Log("searched: " + neighbor.position);
                    if(searchedHexes.Contains(neighbor))
                    {
                        continue;
                    }
                    float tentative_g_score = current.g_score + Vector3.Distance(current.position, neighbor.position);

                    if(!toBeEvaluated.Contains(neighbor) || tentative_g_score < neighbor.g_score)
                    {
                        neighbor.cameFrom = current;
                        neighbor.g_score = tentative_g_score;
                        neighbor.f_score = neighbor.g_score + heuristic_cost_estimate(neighbor, destination);
                        if (!toBeEvaluated.Contains(neighbor))
                            toBeEvaluated.Add(neighbor);
                    }
                }
            }
            throw new Exception("A* search failure!");
        }

        /*public List<Hex> getPathToRelay()
        {
            searchPath
        }*/
    #endregion

    #region Hex Functions
        public void updateHex(Hex changeTo)
        {
            var index = changeTo.zx.getValue() * gridSize + changeTo.zx.getKey();
            Hexes[index] = changeTo;
            //Debug.Log("CHECK [" + changeTo.z + "][" + changeTo.x + "]_> " + Hexes[index].instance);
        }

        public void debugSelectedHex()
        {
            //Debug.Log("****start****");
            Debug.Log(selectedTile.getList().Count);
            Debug.Log(selectedTile.getPathList().Count);
            Debug.Log("tile has?_> "+selectedTile.instance);
            Debug.Log(selectedTile.type);
            /*Debug.Log(selectedTile.position);
            Debug.Log("****end****");
            Debug.Log("****start2****");
            Debug.Log(Hexes[selectedTile.z, selectedTile.x].instance);
            Debug.Log(Hexes[selectedTile.z, selectedTile.x].position);
            Debug.Log("****end2****");*/
        }

        //Function to be invoked at game runtime
        public Hex RuntimeHexFromWorld()
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 400.0f))
            {
                //Debug.DrawLine(hit.point, ray.origin , Color.red);
                //Debug.Log(hit.point);
                //Instantiate (particle, hit.point, transform.rotation); // Create a particle if hit
            }

            //checkedHexes.Clear();
            var mousePosition = hit.point;
            
            //Debug.Log(mousePosition);
            //Debug.Log(hit.point);
            float ix = mousePosition.z;
            float iy = mousePosition.x;

            var col_guess = Mathf.RoundToInt(ix / tileSize);
            var row_guess = Mathf.RoundToInt(iy / xOffset);
            Hex closest = ScriptableObject.CreateInstance("Hex") as Hex;
            closest.init(new Vector3(float.MaxValue, float.MaxValue, float.MaxValue), 0, 0);
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
                        if (index < Hexes.Length)
                        {
                            //checkedHexes.Add(Hexes[index]);
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

            if (closest.instance != null)
            {
                selectedTile = closest;
                checkedHexes = closest.getList();
                return closest;
            }
            else return null;              
        }

        public Hex HexFromWorld(Vector3 mousePosition)
        {
            float ix = mousePosition.z;
            float iy = mousePosition.x;

            var col_guess = Mathf.RoundToInt(ix / tileSize);
            var row_guess = Mathf.RoundToInt(iy / xOffset);
            Hex closest = ScriptableObject.CreateInstance("Hex") as Hex;
            closest.init(new Vector3(float.MaxValue, float.MaxValue, float.MaxValue), 0, 0);
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
                        if (index < Hexes.Length)
                        {
                            //checkedHexes.Add(Hexes[index]);
                            
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
            checkedHexes = closest.getPathList();

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
