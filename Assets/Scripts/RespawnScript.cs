using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnScript : MonoBehaviour {

    public GameObject lastCheckpoint;
    public GameObject player;
    public GameObject leg;
    public Vector2 respawnPosition;
    public Vector2 legRespawnPosition;
    public Quaternion legRespawnRotation;
    public List<GameObject> debrisFields;


    public List<GameObject> playerParts;
    public List<Vector3> playerPartPositions;
    public List<Quaternion> playerPartRotations;



    // Use this for initialization
    void Start () {
        
        setCheckpoint(lastCheckpoint);
        respawn();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            respawn();
        }
	}

    public void setCheckpoint(GameObject checkpoint)
    {
        lastCheckpoint = checkpoint;
        
        for (int i = 0; i < playerParts.Count; i++){
            GameObject playerPart = playerParts[i];
            playerPartPositions[i] = playerPart.transform.position;
            playerPartRotations[i] = playerPart.transform.rotation;
        }
    }

    public void respawn()
    {
        Debug.Log("respawning");
        player.GetComponent<PlayerControlScript>().launchFromFeet();
        
        for (int i = 0; i < playerParts.Count; i++)
        {
            GameObject playerPart = playerParts[i];
            playerPart.transform.position = playerPartPositions[i];
            playerPart.transform.rotation = playerPartRotations[i];
            if (playerPart.GetComponent<Rigidbody2D>() != null)
            {
                playerPart.GetComponent<Rigidbody2D>().angularVelocity = 0;
                playerPart.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1);
       
            }
        }
        player.transform.localScale = new Vector3(-1, 1, 1);

        foreach (GameObject debrisField in debrisFields)
        {
            debrisField.GetComponent<DebrisFieldScript>().spawnDebris();
        }
    }

    public void winGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("You win!");
    }
}
