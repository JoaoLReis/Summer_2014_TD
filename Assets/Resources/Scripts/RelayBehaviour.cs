using UnityEngine;
using System.Collections;

public class RelayBehaviour : MonoBehaviour {

    private GlobalData data;

    void Awake()
    {
        data = GameObject.FindWithTag("DataHolder").GetComponent<GlobalData>();
    }

    // Use this for initialization
	void Start () {
        data.addRelay(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
