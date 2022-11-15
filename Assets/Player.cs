using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;
	private Vector2 playerDirection;

    BoardManager bm;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bm = GameObject.FindGameObjectWithTag("Door").GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
		float directionY = Input.GetAxisRaw("Vertical");
		
		playerDirection = new Vector2(directionX, directionY).normalized;
    }
	
    void FixedUpdate()
    { 
	    rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
	}

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Trigger!");
        bm.Start();
    }
  
}  