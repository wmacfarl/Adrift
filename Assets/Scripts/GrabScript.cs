using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

    public string type;
    public PointAndGrabScript pointAndGrabScript;
    public HingeJoint2D grabJoint;
    public int framesSinceLaunch;

	// Use this for initialization
	void Start () {
        grabJoint = GetComponent<HingeJoint2D>();
        grabJoint.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        framesSinceLaunch++;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null)
        {
            if (framesSinceLaunch > 10)
            {
                pointAndGrabScript.triggerGrab(collision, type);
                grabJoint.connectedBody = collision.rigidbody;
                grabJoint.connectedAnchor = collision.rigidbody.transform.InverseTransformPoint(collision.GetContact(0).point);
                //  grabJoint.distance = .01f;
                grabJoint.enabled = true;
            }
        }
    }
}