using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float speed = 7;
    [SerializeField] float rotateSpeed = 100;

    [Header("Attacks")]
    private ParticleSystem directAttackBurst;
    public ParticleSystem areaAttackBurst;
    private GameObject target;
    private float maxRange = 10.0f;
    private float targetDist;
    private int firePower = 1;
    private float shootCooldown = 1.5f;
    private float shootCDEnd;
    private int aoePower = 3;
    private float aoeRadius = 5;
    private int aoeCooldown = 5;
    private float aoeCDEnd;

    //private Rigidbody playerRB;
    private GameManager gameManager;
    public Animator playerAnim;
    public GameObject playerModel;

    //private float horizInput;
    private float vertInput;
    private float rotateInput;

    // Start is called before the first frame update
    void Start()
    {
        //playerRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shootCDEnd = aoeCDEnd = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.fear < gameManager.maxFear)
        {
            if (target)
            {
                targetDist = Vector3.Distance(target.transform.position, transform.position);
            }

            Move();
            Target();
            DirectAttack();
            AOEAttack();

            // Deselect target without selecting new
            if (Input.GetKeyDown(KeyCode.Escape) && target)
            {
                DeselectTarget();
            }
        }
        else
        {
            playerAnim.SetBool("Death_b", true);
        }
    }

    private void Move()
    {
        //horizInput = Input.GetAxis("Horizontal"); Still bugs to solve in strafing
        vertInput = Input.GetAxis("Vertical");
        rotateInput = Input.GetAxis("Rotate");
        if (vertInput != 0)
        {
            playerAnim.SetBool("IsMoving", true);
            transform.Translate(Vector3.forward * vertInput * speed * Time.deltaTime);
            playerModel.transform.position = transform.position;
        }
        else
        {
            playerAnim.SetBool("IsMoving", false);
        }
        // May try physics based movement later but for now going with Translate
        //playerRB.AddForce(Vector3.right * horizInput * speed);
        //playerRB.AddForce(Vector3.forward * vertInput * speed);
        //transform.Translate(Vector3.right * horizInput * speed * Time.deltaTime);        
        transform.Rotate(Vector3.up * rotateInput * rotateSpeed * Time.deltaTime);
    }

    private void Target()
    {
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
    }

    private void DirectAttack()
    {
        // Attack targeted enemy
        if (Input.GetKeyDown(KeyCode.Alpha1) && target && targetDist <= maxRange && Time.time >= shootCDEnd)
        {
            target.GetComponent<Enemy>().health -= firePower;
            playerAnim.SetBool("Shoot_b", true);
            shootCDEnd = Time.time + shootCooldown;
            Debug.Log(target + " health is " + target.GetComponent<Enemy>().health);
        }
        else
        {
            playerAnim.SetBool("Shoot_b", false);
        }
    }

    private void AOEAttack()
    {
        // Attack surrounding enemies
        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time >= aoeCDEnd)
        {
            Debug.Log("AOE attack used");
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, aoeRadius);

            foreach (var collider in hitEnemies)
            {
                if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Spawner"))
                {
                    collider.gameObject.GetComponent<Enemy>().health -= aoePower;
                }
            }

            playerAnim.SetBool("Jump_b", true);

            aoeCDEnd = Time.time + aoeCooldown;
        }
        else
        {
            playerAnim.SetBool("Jump_b", false);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            gameManager.GameWin();
        }
    }
}
