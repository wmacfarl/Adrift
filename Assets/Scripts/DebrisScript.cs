using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisScript : MonoBehaviour {

    private void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }

    private void OnBecameVisible()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    private void OnBecameInvisible()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }

}
