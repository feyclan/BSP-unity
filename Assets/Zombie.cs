using System;
using System.Collections;
using System.Collections.Generic;
using BSP;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public SubDungeon assignedRoom;

    public List<Vector3> patrolRoute;
    public int currentPatrolPoint = 0;
    
    //-- Different speeds for zombies --//
    public int patrolSpeed = 2;
    public int chasingSpeed = 5;
    
    public int damage = 5;

    public void InitializePatrolRoute()
    {
        //-- Get dimensions of the room --//
        var x1 = assignedRoom.room.x;
        var x2 = assignedRoom.room.x + assignedRoom.room.width;
        var y1 = assignedRoom.room.y;
        var y2 = assignedRoom.room.y + assignedRoom.room.height;
        var w = assignedRoom.room.width;
        var h = assignedRoom.room.height;


        //-- Compute the 4 corners of the room --//
        var p1 = new Vector3(x1+1, y2-1, 0); // left top
        var p2 = new Vector3(x1+1, y1+1, 0); // left bottom
        var p3 = new Vector3(x1 + w / 2, y1, 0); // middle bottom
        var p4 = new Vector3(x2-1, y1+1, 0); // right bottom
        var p5 = new Vector3(x2-1, y2-1, 0); // right top
        var p6 = new Vector3(x1 + w/2, y2, 0); // middle top

        patrolRoute = new List<Vector3>();
        patrolRoute.Add(p1);
        patrolRoute.Add(p2);
        patrolRoute.Add(p3);
        patrolRoute.Add(p4);
        patrolRoute.Add(p5);
        patrolRoute.Add(p6);
    }

    public Vector3 GetPatrolPoint()
    {
        return patrolRoute[currentPatrolPoint];
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy"))
        {
            NextPatrolPoint();
        }
    /*
        if (col.collider.CompareTag("Player"))
        {
            //-- Chase the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            Debug.Log("Le player est " + player);
            var h = player.transform.GetComponent<HealthPlayer>();
            //h.Hit(gameObject, damage);
            h.Hit("Player", damage);
        }
    */
        if (col.collider.CompareTag("Player"))
        {
            //-- Chase the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            Debug.Log("Le player est " + player);
            var h = player.transform.GetComponent<HealthPlayer>();
            Debug.Log("Le hest " + h);
            //h.Hit(gameObject, damage);
            h.Hit(gameObject, damage);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Well"))
        {
            var health = GetComponent<Health>();
            // NPC was fleeing
            if (health.health <= 20)
            {
                health.health = 100;
                //health.UpdateHP();
            }
        }
    }

    public void NextPatrolPoint()
    {
        currentPatrolPoint += 1;
        // when we have been through all the points we can come back to the original one and start the cycle again
        if (currentPatrolPoint >= patrolRoute.Count)
        {
            // reset the patrol points
            currentPatrolPoint = 0;
        }
    }
}
