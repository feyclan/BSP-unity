using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the GameManager
        gameManager = GameObject.Find("World").GetComponent<GameManager>();
    }
    
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            gameManager.NextLevel();
        }
    }
}
