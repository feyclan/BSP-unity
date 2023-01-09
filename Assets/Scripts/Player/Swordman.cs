using UnityEngine;

public class Swordman : MonoBehaviour
{
    //-- Reference to managers --//
    public BoardManager board;
    private AudioSource source;
    // last position of the player, used to avoid moving out of the dungeon
    public Vector3 lastPosition;
    
    //-- Objects of the controller --//
    public Rigidbody2D m_rigidbody;
    private CapsuleCollider2D m_CapsulleCollider;
    public Animator m_Anim;
    private HealthPlayer currentHealthPlayer;
    private MainPlayer mainPlayer;
    private float m_MoveX;
    private float m_MoveY;
    private bool OnceJumpRayCheck;
    public Vector2 speed = new(50, 50);
    public int attackCounter = 0;
    
    public GameObject collidingEnemy;

    public bool isAttacking = false;
    // actually look for where tf the mouse is checked and store the variable there instead
    

    
    private void Start()
    {
        //-- Retrieve player's components --//
        m_CapsulleCollider  = transform.GetComponent<CapsuleCollider2D>();
        m_Anim = transform.Find("model").GetComponent<Animator>();
        m_rigidbody = transform.GetComponent<Rigidbody2D>();
        //currentHealthPlayer = transform.GetComponent<HealthPlayer>();
        //Debug.LogFormat("Points de vie restants : {0}", currentHealthPlayer);
        // var player = GameObject.FindGameObjectsWithTag("Player")[0];
        mainPlayer = gameObject.GetComponent<MainPlayer>();
        source = GetComponent<AudioSource>();
    }
    
    private void Filp(bool bLeft)
    {
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
    }

    private void Update()
    {
        // Process the player movement
        ProcessInput();
        HandleAttack();

    }

    

    private void HandleAttack()
    {
        // Check if the left mouse button is down
        if (Input.GetMouseButtonDown(0))
        {
            // The left mouse button is down, so set the attacking flag to true
            isAttacking = true;
            m_Anim.Play("Attack");
            source.Play();
            attackCounter += 1;
        }
    }

    /*

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                
                currentHealthPlayer -= 5;
                Debug.LogFormat("Points de vie restants : {0}", currentHealthPlayer);
            }
            
        }
    */

    public void ProcessInput()
    {
        //-- List of possible animations --//
        /*
         * Ducking animation: m_Anim.Play("Sit");
         * Standing animation: m_Anim.Play("Idle");
         * Attacking animation: m_Anim.Play("Attack");
         * Die animation: m_Anim.Play("Die");
         */
        
        //-- Movement --//
        m_MoveX = Input.GetAxis("Horizontal");
        m_MoveY = Input.GetAxis("Vertical");
        
        // Attack only attacks when not running
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                m_Anim.Play("Sit");
            }
            else
            {
                if (m_MoveX == 0 && m_MoveY == 0)
                {
                    if (!OnceJumpRayCheck)
                        m_Anim.Play("Idle");
                }
                else
                {
                    m_Anim.Play("Run");
                }

            }
        }
        
        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
        Vector3 movement = new Vector3(speed.x * m_MoveX, speed.y * m_MoveY, 0);
        movement *= Time.deltaTime;
        transform.transform.Translate(movement);
        if (m_MoveX <= 0)
        {
            Filp(true);
        }
        else
        {
            Filp(false);
        }

        //-- Limit movement to dungeon only --//
        var posX = Mathf.RoundToInt(transform.position.x);;
        var posY = Mathf.RoundToInt(transform.position.y);
        if (board.boardPositionsFloor[posX, posY] == null)
        {
            transform.position = lastPosition;
        }

        lastPosition = transform.position;
    }
}
