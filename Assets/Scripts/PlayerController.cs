using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject areaAttackPrefab;

    private Rigidbody playerRB;
    private GameManager gameManager;

    [Header("Player Stats")]    
    [SerializeField] float speed = 7;
    [SerializeField] float rotateSpeed = 100;

    private float horizInput;
    private float vertInput;
    private float rotateInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.fear < gameManager.maxFear)
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
        else
        {
            Debug.Log("Fear levels have reached maximum");
        }
    }
}
