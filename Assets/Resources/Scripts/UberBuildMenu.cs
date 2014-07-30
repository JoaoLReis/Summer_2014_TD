using UnityEngine;
using System.Collections;

public class UberBuildMenu : MonoBehaviour {
    
    protected enum State { Hidden, Regular, Built, Relay };

    private State state;
    private GameManager gManager;
    private Transform player;
    private Transform Built, Regular, greenArea, dropRelayMenu;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Camera.main.GetComponent<UberCamera>().SetSelectedObject(transform);
        Built = transform.FindChild("Built");
        Regular = transform.FindChild("Regular");
        dropRelayMenu = transform.FindChild("DropRelay");
        greenArea = transform.FindChild("greenArea");
        Regular.gameObject.SetActive(false);
        Built.gameObject.SetActive(false);
        dropRelayMenu.gameObject.SetActive(false);
        state = State.Hidden;
        gManager = GameObject.FindWithTag("DataHolder").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {     
        switch(state)
        {
            case State.Hidden:
                break;
            case State.Regular:
                transform.rotation = Quaternion.LookRotation((Camera.main.transform.position - transform.position).normalized);
                break;
            case State.Built:
                transform.rotation = Quaternion.LookRotation((Camera.main.transform.position - transform.position).normalized);
                break;
            case State.Relay:
                transform.rotation = Quaternion.LookRotation((Camera.main.transform.position - transform.position).normalized);
                break;
        }
	}

    public void goBuilt()
    {
        greenArea.gameObject.SetActive(false);
        dropRelayMenu.gameObject.SetActive(false);
        Regular.gameObject.SetActive(false);
        Built.gameObject.SetActive(true);
        state = State.Built;
    }

    public void goRegular()
    {
        greenArea.gameObject.SetActive(false);
        dropRelayMenu.gameObject.SetActive(false);
        Regular.gameObject.SetActive(true);
        Built.gameObject.SetActive(false);
        state = State.Regular;
    }

    public void goHidden()
    {
        transform.rotation = Quaternion.identity;
        dropRelayMenu.gameObject.SetActive(false);
        greenArea.gameObject.SetActive(true);
        Regular.gameObject.SetActive(false);
        Built.gameObject.SetActive(false);
        state = State.Hidden;
    }

    public void goRelay()
    {
        transform.rotation = Quaternion.identity;
        dropRelayMenu.gameObject.SetActive(true);
        Regular.gameObject.SetActive(false);
        Built.gameObject.SetActive(false);
        greenArea.gameObject.SetActive(false);
        state = State.Relay;
    }

    public void dropRelay(GameObject relay)
    {
        Instantiate(relay, transform.position, Quaternion.identity);
    }

    public void Instantiate(GameObject tower)
    {
        Debug.Log("instantiating---!!!");
        gManager.createdTower(Instantiate(tower, transform.position, Quaternion.identity) as GameObject);
    }
}
