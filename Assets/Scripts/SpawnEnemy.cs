using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> enemies;
    private GameManager gameManager;
    private Vector3 spawnPos;
    private float startDelay = 0f;
    private float repeatRate = 15f;
    private int spawnCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnPos = transform.position;
        InvokeRepeating("SpawnWave", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWave()
    {
        if (gameManager.gameActive)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int index = Random.Range(0, enemies.Count);
                spawnPos += new Vector3(i, i, i);
                Instantiate(enemies[index], spawnPos, enemies[index].transform.rotation);
            }
        }        
    }
}
