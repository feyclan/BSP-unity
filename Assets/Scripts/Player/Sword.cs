using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;

    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Enemy")
        {
            //-- Retrieve the Health controller NPC that is hit --//
            var h = collider2D.gameObject.GetComponent<Health>();
            //-- Hit the player --//
            h.Hit(player, damage);
        }
    }
}
