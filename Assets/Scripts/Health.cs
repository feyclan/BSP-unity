using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Health : MonoBehaviour
{
    public int health = 100;
    public TextMeshProUGUI txt;
    public HealthBar healthBar;


    public void Hit(GameObject attacker, int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (attacker.CompareTag("Player"))
            {
                var score = attacker.GetComponent<Score>();
                score.AddXP(100);
            }
            Die();
        }

        UpdateHP();
    }

    public void Recover()
    {
        health += 1;
        UpdateHP();
    }

    public void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            Application.Quit();
        }
        Destroy(gameObject);
    }

    public void UpdateHP()
    {   

            if (gameObject.CompareTag("Player"))
            {
            healthBar.SetHealth( health );  
            }

            if (gameObject.CompareTag("Enemy"))
            {
            txt.text = $"Health: {health}";                 
            }
    }
    
}