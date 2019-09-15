using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public int score;

    public Text restartText;
    public Text gameOverText;

    public bool gameOver;
    public bool restart;

    void Start () 
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        StartCoroutine ( SpawnWaves () );
        UpdateScore ();
    }

    void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
            }
        }
    }

    IEnumerator SpawnWaves () 
    {
        yield return new WaitForSeconds(startWait); // 2 start delay
        while(true) 
        {
            for (int i = 0; i < hazardCount; i++) 
            {
                Vector3 spawnPosition = new Vector3 (
                    Random.Range(-spawnValues.x, spawnValues.x),
                    spawnValues.y,
                    spawnValues.z
                );
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait); // 1 harzard delay
            }
            yield return new WaitForSeconds(waveWait); // 3 wave delay
            
            if (gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}