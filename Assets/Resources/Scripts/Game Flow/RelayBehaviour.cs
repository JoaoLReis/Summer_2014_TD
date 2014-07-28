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
	
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            gManager.enemyFinish(other.gameObject);
        }
    }
}
