using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisFieldScript : MonoBehaviour {


    List<GameObject> debrisList;
    public float angularVelocityRange;
    public float linearVelocityRange;
    public List<GameObject> debrisTypes;
    public List<int> debrisCounts;
    public GameObject electricityOrb;
    public float zapSpawnRate = .5f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void spawnDebris()
    {   if (debrisList == null)
        {
            debrisList = new List<GameObject>();
        }
        else
        {
            foreach (GameObject go in debrisList)
            {
                GameObject.Destroy(go);
            }
        }

        PolygonCollider2D polyCollider = GetComponent<PolygonCollider2D>();
  

        for (int i = 0; i < debrisTypes.Count; i++)
        {
            int spawnedObjects = 0;
            GameObject objectToSpawn = debrisTypes[i];
            int debrisCount = debrisCounts[i];
            while (spawnedObjects < debrisCount)
            {
                GameObject newObject = GameObject.Instantiate(objectToSpawn);
                newObject.transform.position = new Vector2(Random.Range(polyCollider.bounds.min.x, polyCollider.bounds.max.x), Random.Range(polyCollider.bounds.min.y, polyCollider.bounds.max.y));
                newObject.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                newObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-angularVelocityRange, angularVelocityRange);
                newObject.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * linearVelocityRange;
                Collider2D newCollider = newObject.GetComponent<Collider2D>();
                Collider2D[] overlapResults = new Collider2D[100];
                if (newCollider.OverlapCollider(new ContactFilter2D(), overlapResults) == 0)
                {
                    debrisList.Add(newObject);

                    if (i == 0)
                    {
                        if (Random.value > zapSpawnRate)
                        {
                            GameObject zapper = GameObject.Instantiate(electricityOrb);
                            zapper.transform.parent = newObject.transform;
                            zapper.transform.localPosition = Random.insideUnitCircle*2f;
                            debrisList.Add(zapper);
                        }
                    }
                    spawnedObjects++;
                }
                else
                {
                    GameObject.Destroy(newObject);
                }
            }
        }
        /*
        for (int i = 0; i < chunk2Count; i++)
        {
            GameObject.Instantiate(Chunk2);

        }
        for (int i = 0; i < chunk3Count; i++)
        {
            GameObject.Instantiate(Chunk3);

        }
        for (int i = 0; i < girderCount; i++)
        {
            GameObject.Instantiate(Girder);

        }
        */
    }
}
