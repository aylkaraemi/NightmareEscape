using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject areaAttackPrefab;

    private Rigidbody playerRB;
    public float speed;
    public float rotateSpeed;
    private float horizInput;
    private float vertInput;
    private float rotateInput;
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
        rotateInput = Input.GetAxis("Rotate");
        //playerRB.AddForce(Vector3.right * horizInput * speed);
        //playerRB.AddForce(Vector3.forward * vertInput * speed);
        transform.Translate(Vector3.right * horizInput * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * vertInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotateInput * rotateSpeed * Time.deltaTime);
    }
}
