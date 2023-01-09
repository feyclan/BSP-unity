using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Health : MonoBehaviour
{
    public int health = 0;
    public int maxHealth = 100;
    public TextMeshProUGUI txt;


    void Start()
    {
        health = maxHealth;
    }

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

    }

    public void Recover()
    {
        health += 1;
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