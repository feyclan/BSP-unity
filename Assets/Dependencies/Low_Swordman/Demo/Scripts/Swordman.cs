using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : PlayerController
{

    public BoardManager board;
    public Vector3 lastPosition;
    
    private void Start()
    {
        //-- Retrieve player's components --//
        m_CapsulleCollider  = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
        health = this.transform.GetComponent<Health>();
        // var player = GameObject.FindGameObjectsWithTag("Player")[0];
        mainPlayer = gameObject.GetComponent<MainPlayer>();
    }



    private void Update()
    {
        // Process the player movement
        ProcessInput();
        
        // Debug.Log($"Player current position: ({transform.position.x}, {transform.position.y}) => {}");
        // board.boardPositionsFloor
    }

    public void ProcessInput()
    {
        //-- List of possible animations --//
        /* Ducking animation: m_Anim.Play("Sit");
         * Standing animation: m_Anim.Play("Idle");
         * Attacking animation: m_Anim.Play("Attack");
         * Die animation: m_Anim.Play("Die");
         */
        
        //-- Die --//
        // if (Input.GetKey(KeyCode.Alpha1))
        // {
        //     m_Anim.Play("Die");
        // }
        
        //-- Movement --//
        m_MoveX = Input.GetAxis("Horizontal");
        m_MoveY = Input.GetAxis("Vertical");
        
        // Attack only attacks when not running
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                m_Anim.Play("Attack");
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                m_Anim.Play("Sit");
            }
            else
            {
                if (m_MoveX == 0)
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
