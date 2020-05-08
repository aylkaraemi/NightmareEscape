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
    
    [Header("Sounds and Animation")]
    public Animator enemyAnim;
    private AudioSource enemyAudio;
    public AudioClip injured;
    public AudioClip attack;

    private float currentDistance;
    private float cooldownStart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyAudio = GetComponent<AudioSource>();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        cooldownStart = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive)
        {
            Attack();
            if (health < 1)
            {
                Destroy(gameObject);
            }
        }
        
    }

    void Attack()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);

        if (currentDistance < attackRange && Time.time > cooldownStart + attackSpeed)
        {
            enemyAnim.SetTrigger("attack");
            enemyAudio.PlayOneShot(attack, 1.0f);
            gameManager.fear += attackPower;
            player.GetComponent<PlayerController>().Feared();
            cooldownStart = Time.time;
        }      
    }

    public void Injured()
    {
        hitBurst.Play();
        enemyAudio.PlayOneShot(injured);
    }
}
