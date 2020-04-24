using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntPlayer : MonoBehaviour
{
    private Rigidbody hunterRB;
    private GameObject player;

    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        hunterRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = (player.transform.position - transform.position).normalized;
        hunterRB.AddForce(targetDirection * speed);

    }
}
