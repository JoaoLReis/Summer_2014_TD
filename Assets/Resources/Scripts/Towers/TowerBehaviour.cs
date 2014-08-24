using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class TowerBehaviour : Imports {

    protected Transform target;
    protected GameObject rotator;
    protected GameObject currentWeapon; 
    protected List<GameObject> inRange;
    protected List<GameObject> notInRange;
    protected TowerDamage damager;
    protected LookAtEnemy aim;
    protected bool firing;

    //############## METHODS ##################

    public void upgradeWeapon(int rank)
    {
        Transform weapon = FindChildWithTag("Weapon" + rank);
        if (weapon == null)
            return;
        currentWeapon.SetActive(false);
        currentWeapon = weapon.gameObject;
        currentWeapon.SetActive(true);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && inSight(other.transform))
        {
            inRange.Add(other.gameObject);
            if (inRange.Count == 1)
            {
                StartCoroutine("enableFiring", other.transform);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //try
            //{
            inRange.RemoveAll(item => item == null);
            inRange.Remove(other.gameObject);
            notInRange.Remove(other.gameObject);
            notInRange.RemoveAll(item => item == null);

            if (target != null)
            {
                if (target.gameObject == other.gameObject)
                {
                    if (inRange.Count > 0)
                    {
                        target = inRange.First().transform;
                        aim.setActiveTarget(target);
                        damager.updateTarget(target);
                    }
                    else
                    {
                        disableFiring();
                    }
                }
            }
            //}
            //catch (Exception e)
            //{
            //    Debug.LogError("Something Went wrong -> trigger exit tower behaviour: " + "This:" + gameObject.name + "Other: " + other.name);
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // #######################
    //  AUXILIARY FUNCTIONS
    // #######################

    public IEnumerator checkForNotInRange()
    {
        while (notInRange.Count > 0)
        {
            notInRange.RemoveAll(item => item == null);
            for (int i = 0; i < notInRange.Count; i++)
            {
                Transform t = notInRange[i].transform;
                if (inSight(t))
                {
                    notInRange.Remove(t.gameObject);
                    StartCoroutine("enableFiring", t);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator checkTarget()
    {
        while(true)
        {
            updateTarget();
            if (!firing)
                yield break;  
            yield return new WaitForSeconds(0.2f);    
        }
    }

    Transform getFirstInSight()
    {
        for (int i = 0; i < inRange.Count; i++)
        {
            Transform t = inRange[i].transform;
            if(inSight(t))
            {
                return t;
            }
        }
        return null;
    }

    private void updateTarget()
    {
        if (target == null)
        {
            inRange.RemoveAll(item => item == null);
            damager.stop();
            Debug.Log("Oh ive destroyed something");

            if (inRange.Count > 0)
            {
                inRange.RemoveAll(item => item == null);
                target = getFirstInSight();
                if (target != null)
                {
                    Debug.Log("Fire!");
                    StartCoroutine("enableFiring", target);
                }
                else
                {
                    Debug.Log("disable");
                    disableFiring();
                }
            }
            else
            {
                disableFiring();
            }
        }
        else if (!inSight(target))
        {
            disableFiring();
            Debug.Log(target.gameObject);
            notInRange.Add(target.gameObject);
            inRange.Remove(target.gameObject);
            StartCoroutine("checkForNotInRange");
        }
    }

    void restartParticleSystem(GameObject obj, bool val)
    {
        currentWeapon.SetActive(true);
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        if (val)
        {
            ps.Play(true);
        }
        else  
        {
            ps.Pause();
           
        }      
    }

    protected bool inSight(Transform t)
    {
        RaycastHit hit;
        Vector3 rayDirection = t.position - rotator.transform.position;
        Debug.DrawRay(rotator.transform.position, rayDirection);
        if (Physics.Raycast(rotator.transform.position, rayDirection, out hit))
        {
            if (hit.transform.tag == "Enemy") 
            {
                return true;
            }
        }
        return false;
    }

    protected IEnumerator enableFiring(Transform t)
    {
        target = t;
        aim.setActiveTarget(target);
        aim.enabled = true;
        //restartParticleSystem(currentWeapon, true);
        currentWeapon.SetActive(true);
        damager.updateTarget(target);
        damager.enabled = true;
        damager.start();
        firing = true;
        StartCoroutine("checkTarget");
        yield return new WaitForEndOfFrame();
    }

    protected void disableFiring()
    {
        aim.reset(transform.position + transform.forward);
        currentWeapon.SetActive(false);
        //restartParticleSystem(currentWeapon, false);
        aim.enabled = false;
        aim.setActiveTarget(null);
        damager.stop();
        firing = false;
    }

    protected Transform FindChildWithTag(string name)
    {
        Transform[] trans = GetComponentsInChildren<Transform>(true);

        foreach (Transform t in trans)
        {
            if (t.gameObject.tag.Equals(name))
                return t;
        }
        return null;
    }

    public GameObject getWeapon() { return currentWeapon; }

    public Transform getTarget() { return target; }

    

}
