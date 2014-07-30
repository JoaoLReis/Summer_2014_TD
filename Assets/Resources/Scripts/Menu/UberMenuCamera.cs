using UnityEngine;
using System.Collections;

public class UberMenuCamera : MonoBehaviour {

	void Update () {
        transform.Rotate(transform.up, 0.5f *Time.deltaTime);
	}
}
