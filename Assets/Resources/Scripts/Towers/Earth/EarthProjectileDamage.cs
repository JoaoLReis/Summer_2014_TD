using UnityEngine;
using System.Collections;

public class EarthProjectileDamage : Imports {

    private TowerStats tower;
    private EarthProjectilesManager manager;
    private int damage;
    private float armorPen;
    private int type;


	// Use this for initialization
	void Start () {
        tower = transform.parent.GetComponent<TowerStats>();
        manager = transform.parent.GetComponent<EarthProjectilesManager>();
        damage = tower.getDamage();
        armorPen = tower.getArmorPen();
        type = tower.getType();
	}

    public void returnToManager()
    {
        Invoke("returnBullet", 1);
    }

    public void returnBullet()
    {
        manager.reaquireBullet(gameObject);
    }
	
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().decreaseHealth(damage, armorPen, type);
        }
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
