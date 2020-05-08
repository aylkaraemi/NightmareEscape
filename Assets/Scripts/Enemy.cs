using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject targetIndicator;
    public ParticleSystem hitBurst;

    private GameManager gameManager;
    private GameObject player;    

    [Header("Enemy Stats")]
    public int health;
    [SerializeField] int attackPower;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackRange;    

    private float currentDistance;
    private float cooldownStart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        cooldownStart = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);

        if (currentDistance < attackRange && Time.time > cooldownStart + attackSpeed)
        {
            gameManager.fear += attackPower;
            cooldownStart = Time.time;
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }
    }    
}
