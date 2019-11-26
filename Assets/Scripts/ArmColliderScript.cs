using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColliderScript : MonoBehaviour {

    public bool touchingSomething;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchingSomething = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingSomething = false;
    }
}
