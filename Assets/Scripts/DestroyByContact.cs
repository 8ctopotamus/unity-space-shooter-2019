using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    public GameController gameController;
    public int scoreValue;

    void Start () 
    {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        } else {
            Debug.Log ("Cannot find GameController script.");
        }
    }

    void OnTriggerEnter (Collider other) 
    {   
        if (other.tag == "Boundary") 
        {
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);
        
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        Destroy(other.gameObject); // bolt
        Destroy(gameObject); // asteroid
        gameController.AddScore(scoreValue);
    }
}
