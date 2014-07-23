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
        barrelEnd = FindChildWithTag("Weapon1");
        rotator = FindChildWithTag("Rotator");
        GameObject bulletprefab = Resources.Load("Projectiles/EarthProjectile") as GameObject;
        for (int i = 0; i < 5; i++)
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
        bullet.SetActive(true);
        bullet.rigidbody.velocity = Vector3.zero;
        bullet.rigidbody.angularVelocity = Vector3.zero;
        bullet.rigidbody.AddForce(rotator.transform.forward * 50, ForceMode.Impulse);
        bullet.GetComponent<EarthProjectileDamage>().returnToManager();
    }

    public void reaquireBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        firedbullets.Remove(bullet);
        bullets.Add(bullet);
    }

    protected Transform FindChildWithTag(string tag)
    {
        Transform[] trans = GetComponentsInChildren<Transform>(true);

        foreach (Transform t in trans)
        {
            if (t.gameObject.tag.Equals(tag))
                return t;
        }
        return null;
    }
}
