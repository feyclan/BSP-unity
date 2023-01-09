using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Health : MonoBehaviour
{
    public int health = 100;
    public TextMeshProUGUI txt;
    private AudioSource[] source;
    private void Start()
    {
       
        source = GameObject.Find("Player").GetComponents<AudioSource>();
    }

    public void Hit(GameObject attacker, int damage)
    {
        health -= damage;
        source[1].Play();
        
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
        txt.text = $"Health: {health}";
    }
}