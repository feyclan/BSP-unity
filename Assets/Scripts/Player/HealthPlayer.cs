using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour

{
    public int currentHealthPlayer = 0;
    public int maxHealthPlayer = 100;

    public HealthBar healthBar;

    void Start()
    {
        currentHealthPlayer = maxHealthPlayer;
    }


    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            DamagePlayer(10);
        }
    }
    


    public void Hit(GameObject attacker, int damage)
    {
        currentHealthPlayer -= damage;

        if (currentHealthPlayer <= 0)
        {
            if (attacker.CompareTag("Player"))
            {
                var score = attacker.GetComponent<Score>();
                score.AddXP(100);
            }
            Die();
        }

    }
    

    public void DamagePlayer( int damage )
    {
        currentHealthPlayer -= damage;
        if (currentHealthPlayer <= 0)
        {
           Die(); 
        }
        healthBar.SetHealth( currentHealthPlayer );
    }

    public void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            Application.Quit();
        }
        Destroy(gameObject);
    }
}
