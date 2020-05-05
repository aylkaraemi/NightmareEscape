using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject player;

    [Header("Enemy Stats")]
    public int attackPower;

    private float minAttackDistance = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<HuntPlayer>().distanceFromPlayer < minAttackDistance)
        {
            gameManager.fear -= attackPower;
        }
    }
}
