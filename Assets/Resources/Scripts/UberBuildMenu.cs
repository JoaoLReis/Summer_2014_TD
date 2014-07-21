using UnityEngine;
using System.Collections;

public class UberBuildMenu : MonoBehaviour {

    public enum State { Hidden, Regular, Built };
    private State state;
    private Transform player;
    private Transform Built, Regular, greenArea;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Camera.main.GetComponent<UberCamera>().SetSelectedObject(transform);
        Built = transform.FindChild("Built");
        Regular = transform.FindChild("Regular");
        greenArea = transform.FindChild("greenArea");
        Regular.gameObject.SetActive(false);
        Built.gameObject.SetActive(false);
        state = State.Hidden;
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
        }
	}

    public void goBuilt()
    {
        greenArea.gameObject.SetActive(false);
        Regular.gameObject.SetActive(false);
        Built.gameObject.SetActive(true);
        state = State.Built;
    }

    public void goRegular()
    {
        greenArea.gameObject.SetActive(false);
        Regular.gameObject.SetActive(true);
        Built.gameObject.SetActive(false);
        state = State.Regular;
    }

    public void goHidden()
    {
        transform.rotation = Quaternion.identity;
        greenArea.gameObject.SetActive(true);
        Regular.gameObject.SetActive(false);
        Built.gameObject.SetActive(false);
        state = State.Hidden;
    }

    public void Instantiate(Transform tower)
    {
        Debug.Log("instantiating---!!!");
        Instantiate(tower, transform.position, Quaternion.identity);
    }
}
