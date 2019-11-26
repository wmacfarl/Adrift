using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObjectScript : MonoBehaviour {

    public GameObject player;
    public GameObject gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision with: " + collision.gameObject);
        if (collision.gameObject == player || (collision.transform.parent != null && collision.transform.parent.gameObject == player))
        {
            gameManager.GetComponent<RespawnScript>().respawn();
        }
    }
}
