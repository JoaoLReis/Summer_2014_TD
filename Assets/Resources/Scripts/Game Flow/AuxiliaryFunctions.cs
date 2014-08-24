using UnityEngine;
using System.Collections;

public class AuxiliaryFunctions : MonoBehaviour
{
    public Transform FindChildWithTag(string name)
    {
        Transform[] trans = GetComponentsInChildren<Transform>(true);

        foreach (Transform t in trans)
        {
            if (t.gameObject.tag.Equals(name))
                return t;
        }
        return null;
    }

    public Transform FindChild(string name)
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
