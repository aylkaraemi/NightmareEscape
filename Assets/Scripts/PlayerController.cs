using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Attacks")]
    public GameObject projectilePrefab;
    public GameObject areaAttackPrefab;
    public GameObject target;

    private Rigidbody playerRB;
    private GameManager gameManager;

    [Header("Player Stats")]    
    [SerializeField] float speed = 7;
    [SerializeField] float rotateSpeed = 100;
    private float maxRange = 10.0f;

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

            // Target Enemy
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Spawner"))
                    {
                        if (target)
                        {
                            DeselectTarget();
                        }
                        SelectTarget(hit.transform.gameObject);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape) && target)
            {
                DeselectTarget();
            }

            // Attack targeted enemy
            if (Input.GetKeyDown(KeyCode.Alpha1) && target)
            {
                Debug.Log("Single target attack used");
            }
            // Attack surrounding enemies
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("AOE attack used");
            }
        }
        else
        {
            Debug.Log("Fear levels have reached maximum");            
        }
    }
        
    public void SelectTarget(GameObject newTarget)
    {
        target = newTarget;
        target.GetComponent<Enemy>().targetIndicator.SetActive(true);
    }

    public void DeselectTarget()
    {
        target.GetComponent<Enemy>().targetIndicator.SetActive(false);
    }
}
