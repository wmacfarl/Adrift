using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndGrabScript : MonoBehaviour {

    Vector2 nullPoint = new Vector2(-12345, -12345);
    public float rotationSpeed;
    public float checkRadius = 2;
    public LayerMask layers;
    public CircleCollider2D grabCollider;
    public GrabScript grabScript;
    public GameObject body;
    public float jointLimitsOffset;
    public PlayerControlScript playerControlScript;


	// Update is called once per frame
	void Update () {
        
    }

    void rotateTowardsTarget(Vector2 targetPosition, float speed)
    {
        Vector3 vectorToTarget = targetPosition - (Vector2) transform.position;

        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    Vector2 findClosestObstaclePoint()
    {
        Vector2 closestPoint = new Vector2();
        return closestPoint;
    }

    Vector2 findPointToFace(float checkRadius)
    {

        RaycastHit2D[] hits = Physics2D.CircleCastAll((Vector2)grabCollider.transform.position, checkRadius, new Vector2(), 0,layers);
        List<Transform> transforms = new List<Transform>();
        float shortestDistance = 100000;
        ColliderDistance2D shortestColliderDistance = new ColliderDistance2D();
        GameObject nearestObject = null;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != transform && transforms.Contains(hit.transform) == false)
            {
                ColliderDistance2D colliderDistance = hit.collider.Distance(grabCollider);
                float distance = colliderDistance.distance;

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestObject = hit.collider.gameObject;
                    shortestColliderDistance = colliderDistance;
                }
            }
        }

        if (shortestColliderDistance.pointB != null)
        {
            Debug.DrawLine(shortestColliderDistance.pointA, shortestColliderDistance.pointB, Color.green);
            return shortestColliderDistance.pointA;
        }
        else
        {
            return nullPoint;
        }
    }

    float normalizeAngle(float angle)
    {
        float newAngle = angle;
        while (newAngle <= -180) newAngle += 360;
        while (newAngle > 180) newAngle -= 360;
        return newAngle;
    }

    public void triggerGrab(Collision2D collision, string grabType)
    {
        playerControlScript.feetGrabbed = true;
    }
}