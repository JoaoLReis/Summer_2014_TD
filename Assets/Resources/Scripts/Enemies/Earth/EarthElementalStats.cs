using UnityEngine;
using System.Collections;

public class EarthElementalStats : EnemyStats {

    void Awake()
    {
        health = 20;
        damage = 1;
        atkSpeed = 1;
        buildingDamage = 2f;
        type = (int)EnemyTypes.EARTH;
        rank = 1;
        armor = 0;
        value = 10;
        init();
    }

	// Use this for initialization
	void Start () {
        movement = GetComponent<NavMeshAgent>();
	}

    //############### LEVELUP ##########
    public override void increaseRank()
    {
        switch (rank)
        {
            case 1:
                increaseHealth(20);
                increaseATKSpeed(0.1f);
                increaseDamage(10);
                rank++;
                break;
            case 2:
                increaseHealth(40);
                increaseATKSpeed(0.15f);
                increaseDamage(30);
                increaseBuildingDamage(1);
                rank++;
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    void OnGUI()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + transform.up*2);
        GUI.Box(new Rect(pos.x, Screen.height - pos.y, 20, 5), "OLA");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
