using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Health : MonoBehaviour
{
    public int health = 100;
    public TextMeshProUGUI txt;

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
        Destroy(gameObject);
    }

    private void UpdateHP()
    {
        txt.text = $"Health: {health}";
    }
}