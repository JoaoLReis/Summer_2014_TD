﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class TowerBehaviour : Imports {

    protected Transform target;
    protected GameObject rotator;
    protected GameObject currentWeapon; 
    protected List<GameObject> inRange;
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
        if (other.tag == "Enemy")
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
            try
            {
                inRange.Remove(target.gameObject);

            
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
            catch (Exception e)
            {
                Debug.Log("Something Went wrong -> trigger exit tower behaviour");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // #######################
    //  AUXILIARY FUNCTIONS
    // #######################

    public IEnumerator checkTarget()
    {
        while(true)
        {
            if (!firing)
                yield break;
            yield return new WaitForSeconds(0.2f);
            updateTarget();
        }
    }

    private void updateTarget()
    {
        if (target == null)
        {
            inRange.RemoveAll(item => item == null);
            if (inRange.Count > 0)
            {
                inRange.RemoveAll(item => item == null);
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

    public void recalculateTarget()
    {
        if (target == null)
        {
            inRange.RemoveAll(item => item == null);
            if (inRange.Count > 0)
            {
                inRange.RemoveAll(item => item == null);
                target = inRange.First().transform;
                aim.setActiveTarget(target);
                damager.updateTarget(target);
            }
            else
            {
                disableFiring();
                damager.stop();
            }
        }
    }

    protected IEnumerator enableFiring(Transform t)
    {
        do
        {
            target = t;
            aim.setActiveTarget(target);
            aim.enabled = true;
            currentWeapon.SetActive(true);
            damager.updateTarget(target);
            damager.enabled = true;
            yield return new WaitForEndOfFrame();
            damager.start();
            firing = true;
            StartCoroutine("checkTarget");
        } while (false);
    }

    protected void disableFiring()
    {
        aim.reset(transform.position + transform.forward);
        currentWeapon.SetActive(false);
        aim.enabled = false;
        aim.setActiveTarget(null);
        damager.enabled = false;
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
