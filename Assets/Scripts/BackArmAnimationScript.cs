using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackArmAnimationScript : MonoBehaviour {
    public GameObject frontArm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = frontArm.transform.eulerAngles;
	}
}
