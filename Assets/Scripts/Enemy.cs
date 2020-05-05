using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject targetIndicator;

    private GameManager gameManager;
    private GameObject player;    

    [Header("Enemy Stats")]
    [SerializeField] int attackPower;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackRange;
    public int health;

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
    }

    void OnMouseDown()
    {
        GameObject currentTarget = player.GetComponent<PlayerController>().target;
        if (currentTarget)
        {
            player.GetComponent<PlayerController>().DeselectTarget();
            player.GetComponent<PlayerController>().SelectTarget(gameObject);
            targetIndicator.SetActive(true);
        }
        else
        {
            player.GetComponent<PlayerController>().SelectTarget(gameObject);
            targetIndicator.SetActive(true);
        }
    }
}
