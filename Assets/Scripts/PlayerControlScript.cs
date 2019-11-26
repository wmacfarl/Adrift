using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {

    public GameObject anchorObject;
    Rigidbody2D rigidbody;
    SpringJoint2D anchorJoint;
    BoxCollider2D collider;

    public float r;
    public float legGrabbedRotateSpeed;
    public GameObject body;
    public GameObject frontArm;
    public GameObject backArm;
    public GameObject leg;
    public float legPushForce = 100;
    public float armPushForce = 100;
    public float legRotationSpeed;
    public LayerMask obstacleLayers;
    public Sprite frontArmRetracted;
    public Sprite backArmRetracted;
    public Sprite frontArmExtended;
    public Sprite backArmExtended;


    public List<Sprite> standingSprites;
    public List<Sprite> squattingSprites;


    public bool feetGrabbed;
    bool blinking = false;
    int blinkStateIndex = 0;
    int blinkFrameDelay = 5;
    int blinkFrameCount = 0;
    public float blinkChance = .005f;

	// Use this for initialization
	void Start () {
        anchorJoint = GetComponent<SpringJoint2D>();
        rigidbody = body.GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        rigidbody.AddForce(Vector2.up*-100);  
    }

  

    // Update is called once per frame
    void Update () {
        if (feetGrabbed)
        {
            leg.GetComponent<SpriteRenderer>().sprite = squattingSprites[blinkStateIndex];

        }
        else
        {
            leg.GetComponent<SpriteRenderer>().sprite = standingSprites[blinkStateIndex];
        }
        if (blinkStateIndex == 0)
        {
            if (Random.value < blinkChance)
            {
                blinkStateIndex = 1;
            }
        }
        else
        {
            blinkFrameCount++;
            if (blinkFrameCount > blinkFrameDelay)
            {
                blinkFrameCount = 0;
                blinkStateIndex++;
                if (blinkStateIndex >= standingSprites.Count)
                {
                    blinkStateIndex = 0;
                }
            }
        }

        if (!feetGrabbed)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                leg.transform.Rotate(new Vector3(0, 0, legRotationSpeed));


            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                leg.transform.Rotate(new Vector3(0, 0, -legRotationSpeed));
            }
        }
        else {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                body.GetComponent<Rigidbody2D>().AddForce(leg.transform.right*legGrabbedRotateSpeed);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                body.GetComponent<Rigidbody2D>().AddForce(leg.transform.right * -legGrabbedRotateSpeed);
            }
        }

            if (Input.GetKey(KeyCode.D))
            {
                frontArm.transform.Rotate(new Vector3(0, 0, legRotationSpeed*5*leg.transform.localScale.x));

            }
            else if (Input.GetKey(KeyCode.A))
            {
                frontArm.transform.Rotate(new Vector3(0, 0, -legRotationSpeed * 5 * leg.transform.localScale.x));
            }
            else
            {
                
            }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (feetGrabbed)
            {
                launchFromFeet();
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            
            frontArm.GetComponent<SpriteRenderer>().sprite = frontArmExtended;
            backArm.GetComponent<SpriteRenderer>().sprite = backArmExtended;
            launchFromArm();
        }
        else
        {
            frontArm.GetComponent<SpriteRenderer>().sprite = frontArmRetracted;
            backArm.GetComponent<SpriteRenderer>().sprite = backArmRetracted;
        }

        if (frontArm.transform.localEulerAngles.z >170)
        {
            leg.transform.localScale = new Vector3(leg.transform.localScale.x*-1,1,1);
            frontArm.transform.localEulerAngles = new Vector3(frontArm.transform.localEulerAngles.x, frontArm.transform.localEulerAngles.y, 
                170);
        }
    else if (frontArm.transform.localEulerAngles.z < 20)
        {
            leg.transform.localScale = new Vector3(leg.transform.localScale.x * -1, 1, 1);
            frontArm.transform.localEulerAngles = new Vector3(frontArm.transform.localEulerAngles.x, frontArm.transform.localEulerAngles.y,
                20);
        }
    }

    public void launchFromFeet()
    {
        if (feetGrabbed)
        {
            feetGrabbed = false;
            leg.GetComponent<PointAndGrabScript>().grabScript.framesSinceLaunch = 0;
            leg.GetComponent<PointAndGrabScript>().grabScript.grabJoint.enabled = false;
            leg.GetComponent<Rigidbody2D>().AddForce(leg.transform.up * legPushForce);
            body.GetComponent<Rigidbody2D>().AddForce(leg.transform.up * legPushForce);
        }
    }

    public void launchFromArm()
    {   
        
        if (frontArm.GetComponent<CircleCollider2D>().GetComponent<ArmColliderScript>().touchingSomething)
        {
            body.GetComponent<Rigidbody2D>().AddForce(frontArm.transform.up * armPushForce);
        }

    }


}
