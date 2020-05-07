﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> spawners;
    private GameObject exit;
    private GameObject player;

    private int spawnerCount = 3;

    public int fear = 0;
    public int maxFear = 1000;
    private int ambientFear = 1;
    private float increaseSpeed = 1;
    private bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AmbientFearIncrease());
        exit = GameObject.Find("Escape Portal");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (fear >= maxFear)
        {
            gameActive = false;
            GameLost();
        }
    }

    public void SpawnTarget(List<GameObject> targets, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    IEnumerator AmbientFearIncrease()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(increaseSpeed);
            fear += ambientFear;
        }  
    }

    void GameLost()
    {
        gameActive = false;
        Debug.Log("Fear has reached maximum, you are lost in nightmare");
    }

    public void GameWin()
    {
        gameActive = false;
        Debug.Log("You've escaped!");
    }
}
