using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> spawners;

    private int spawnerCount = 3;

    public int fear = 0;
    public int maxFear = 1000;
    private int ambientFear = 1;
    private int increasePerSec = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FearIncrease(ambientFear, increasePerSec, true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTarget(List<GameObject> targets, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    IEnumerator FearIncrease(int addFear, int fearRate, bool condition)
    {
        while (condition)
        {
            yield return new WaitForSeconds(fearRate);
            fear += addFear;
        }  
    }
}
