using UnityEngine;
using System.Collections;

public class RelayBehaviour : MonoBehaviour {

    private GameManager gManager;

    void Awake()
    {
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
    }

    // Use this for initialization
	void Start () {
        gManager.addRelay(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
