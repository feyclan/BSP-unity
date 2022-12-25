using System.Collections;
using System.Collections.Generic;
using BSP;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public SubDungeon assignedRoom;

    public List<Vector3> patrolRoute;
    public int currentPatrolPoint = 0;
    public int speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializePatrolRoute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializePatrolRoute()
    {
        //-- Get dimensions of the room --//
        var x1 = assignedRoom.room.x;
        var x2 = assignedRoom.room.x + assignedRoom.room.width;
        var y1 = assignedRoom.room.y;
        var y2 = assignedRoom.room.y + assignedRoom.room.height;


        //-- Compute the 4 corners of the room --//
        var p1 = new Vector3(x1+1, y2-1, 0);
        var p2 = new Vector3(x1+1, y1+1, 0);
        var p3 = new Vector3(x2-1, y1+1, 0);
        var p4 = new Vector3(x2-1, y2-1, 0);

        patrolRoute = new List<Vector3>();
        patrolRoute.Add(p1);
        patrolRoute.Add(p2);
        patrolRoute.Add(p3);
        patrolRoute.Add(p4);
    }

    public Vector3 GetPatrolPoint()
    {
        return patrolRoute[currentPatrolPoint];
    }
}
