using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	bool _BM = false;
    bool _tileUpdate = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	public bool get_BM()
	{
		return _BM;
	}

    public void switch_BM()
    {
        _tileUpdate = true;
        if (_BM)
            _BM = false;
        else _BM = true;
    }

	public void set_BM(bool val)
	{
        _tileUpdate = true;
        _BM = val;
	}

    public void setTileUpdate(bool val)
    {
        _tileUpdate = val;
    }

    public bool tileUpdate()
    {
        return _tileUpdate;
    }
}
