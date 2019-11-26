using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public GameObject gameManager;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entered trigger with: " + collision.gameObject.name + ", tag = " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Deadly")
        {
            gameManager.GetComponent<RespawnScript>().respawn();
        }else if (collision.gameObject.tag == "WinGame")
        {
            gameManager.GetComponent<RespawnScript>().winGame();
        }
    }
}