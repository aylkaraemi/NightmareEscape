using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject areaAttackPrefab;

    private Rigidbody playerRB;
    public float speed;
    private float horizInput;
    private float vertInput;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        playerRB.AddForce(Vector3.right * horizInput * speed);
        playerRB.AddForce(Vector3.forward * vertInput * speed);
    }
}
