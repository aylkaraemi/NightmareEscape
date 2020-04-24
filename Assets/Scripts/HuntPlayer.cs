using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntPlayer : MonoBehaviour
{
    private Rigidbody hunterRB;
    private GameObject player;

    public float speed;
    private float distanceFromPlayer;
    private float huntingTrigger = 10.0f;
    private bool hunting = false;


    // Start is called before the first frame update
    void Start()
    {
        hunterRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {        
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (hunting)
        {
            Vector3 targetDirection = (player.transform.position - transform.position).normalized;
            hunterRB.AddForce(targetDirection * speed);
        }
        else if (distanceFromPlayer <= huntingTrigger)
        {
            hunting = true;
        }
        

    }
}
