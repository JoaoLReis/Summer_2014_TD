using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EarthProjectilesManager : MonoBehaviour {

    private List<GameObject> bullets;
    private List<GameObject> firedbullets;
    private Transform barrelEnd;
    private Transform rotator;

    void Awake()
    {
        bullets = new List<GameObject>();
        firedbullets = new List<GameObject>();
    }

    void Start()
    {
        barrelEnd = FindChild("BarrelEnd");
        rotator = GameObject.FindWithTag("Rotator").transform;
        GameObject bulletprefab = Resources.Load("Projectiles/EarthProjectile") as GameObject;
        for (int i = 0; i < 1; i++)
        {
            GameObject bullet = Instantiate(bulletprefab, barrelEnd.position, Quaternion.identity) as GameObject;
            bullet.SetActive(false);
            bullet.transform.parent = transform;
            bullets.Add(bullet);
        }
    }

    public void fireBullet()
    {
        GameObject bullet = bullets.First();
        firedbullets.Add(bullet);
        bullets.Remove(bullet);
        bullet.transform.position = barrelEnd.position;
        bullet.transform.rotation = rotator.rotation;
        bullet.SetActive(true);
        bullet.rigidbody.velocity = Vector3.zero;
        bullet.rigidbody.angularVelocity = Vector3.zero;
        bullet.rigidbody.AddForce(bullet.transform.forward * 50, ForceMode.Impulse);
        bullet.GetComponent<EarthProjectileDamage>().returnToManager();
    }

    public void reaquireBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        firedbullets.Remove(bullet);
        bullets.Add(bullet);
    }

    protected Transform FindChild(string name)
    {
        Transform[] trans = GetComponentsInChildren<Transform>(true);

        foreach (Transform t in trans)
        {
            if (t.gameObject.name.Equals(name))
                return t;
        }
        return null;
    }
}
