using System.Collections;
using System.Collections.Generic;
using BSP;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject floorTile;
    public GameObject corridorTile;
    public GameObject[,] boardPositionsFloor;
    public SubDungeon dungeon;
    public static List<Rect> corridors = new List<Rect>();

    public void CreateBSP(SubDungeon subDungeon)
    {
        // Debug.Log("Splitting sub-dungeon " + subDungeon.debugId + ": " + subDungeon.rect);
        if (subDungeon.IAmLeaf())
        {
            // if the sub-dungeon is too large
            if (subDungeon.rect.width > maxRoomSize
              || subDungeon.rect.height > maxRoomSize
              || Random.Range(0.0f, 1.0f) > 0.25)
            {

                if (subDungeon.Split(minRoomSize, maxRoomSize))
                {
                    // Debug.Log("Splitted sub-dungeon " + subDungeon.debugId + " in "
                    //   + subDungeon.left.debugId + ": " + subDungeon.left.rect + ", "
                    //   + subDungeon.right.debugId + ": " + subDungeon.right.rect);

                    CreateBSP(subDungeon.left);
                    CreateBSP(subDungeon.right);
                }
            }
        }
    }


    public static void CreateCorridorBetween(SubDungeon left, SubDungeon right)
    {

        Rect lroom = left.GetRoom();
        Rect rroom = right.GetRoom();

        // Debug.Log("Creating corridor(s) between " + left.debugId + "(" + lroom + ") and " + right.debugId + " (" + rroom + ")");

        // attach the corridor to a random point in each room
        Vector2 lpoint = new Vector2((int)Random.Range(lroom.x + 1, lroom.xMax - 1), (int)Random.Range(lroom.y + 1, lroom.yMax - 1));
        Vector2 rpoint = new Vector2((int)Random.Range(rroom.x + 1, rroom.xMax - 1), (int)Random.Range(rroom.y + 1, rroom.yMax - 1));

        // always be sure that left point is on the left to simplify the code
        if (lpoint.x > rpoint.x)
        {
            Vector2 temp = lpoint;
            lpoint = rpoint;
            rpoint = temp;
        }

        int w = (int)(lpoint.x - rpoint.x);
        int h = (int)(lpoint.y - rpoint.y);

        // Debug.Log("lpoint: " + lpoint + ", rpoint: " + rpoint + ", w: " + w + ", h: " + h);

        // if the points are not aligned horizontally
        if (w != 0)
        {
            // choose at random to go horizontal then vertical or the opposite
            if (Random.Range(0, 1) > 2)
            {
                // add a corridor to the right
                corridors.Add(new Rect(lpoint.x, lpoint.y, Mathf.Abs(w) + 1, 1));

                // if left point is below right point go up
                // otherwise go down
                if (h < 0)
                {
                    corridors.Add(new Rect(rpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                }
                else
                {
                    corridors.Add(new Rect(rpoint.x, lpoint.y, 1, -Mathf.Abs(h)));
                }
            }
            else
            {
                // go up or down
                if (h < 0)
                {
                    corridors.Add(new Rect(lpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                }
                else
                {
                    corridors.Add(new Rect(lpoint.x, rpoint.y, 1, Mathf.Abs(h)));
                }

                // then go right
                corridors.Add(new Rect(lpoint.x, rpoint.y, Mathf.Abs(w) + 1, 1));
            }
        }
        else
        {
            // if the points are aligned horizontally
            // go up or down depending on the positions
            if (h < 0)
            {
                corridors.Add(new Rect((int)lpoint.x, (int)lpoint.y, 1, Mathf.Abs(h)));
            }
            else
            {
                corridors.Add(new Rect((int)rpoint.x, (int)rpoint.y, 1, Mathf.Abs(h)));
            }
        }


        // Debug.Log("Corridors: ");
        // foreach (Rect corridor in corridors)
        // {
        //     Debug.Log("corridor: " + corridor);
        // }
    }



    void DrawCorridors(SubDungeon subDungeon)
    {
        if (subDungeon == null)
        {
            return;
        }

        DrawCorridors(subDungeon.left);
        DrawCorridors(subDungeon.right);

        foreach (Rect corridor in corridors)
        {
            for (int i = (int)corridor.x; i < corridor.xMax; i++)
            {
                for (int j = (int)corridor.y; j < corridor.yMax; j++)
                {
                    if (boardPositionsFloor[i, j] == null)
                    {
                        GameObject instance = Instantiate(corridorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(transform);
                        boardPositionsFloor[i, j] = instance;
                    }
                }
            }
        }
    }

    public void DrawRooms(SubDungeon subDungeon)
    {
        if (subDungeon == null)
        {
            return;
        }
        if (subDungeon.IAmLeaf())
        {
            for (int i = (int)subDungeon.room.x; i < subDungeon.room.xMax; i++)
            {
                for (int j = (int)subDungeon.room.y; j < subDungeon.room.yMax; j++)
                {
                    GameObject instance = Instantiate(floorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(transform);
                    boardPositionsFloor[i, j] = instance;
                }
            }
        }
        else
        {
            DrawRooms(subDungeon.left);
            DrawRooms(subDungeon.right);
        }
    }


    public void Initialize()
    {
        SubDungeon rootSubDungeon = new SubDungeon(new Rect(0, 0, boardRows, boardColumns));
        CreateBSP(rootSubDungeon);
        rootSubDungeon.CreateRoom();

        dungeon = rootSubDungeon;
        
        Debug.Log($"Root: {rootSubDungeon}");
        boardPositionsFloor = new GameObject[boardRows, boardColumns];


        //rootSubDungeon.CreateCorridor();
        //CreateCorridorBetween(rootSubDungeon.left, rootSubDungeon.right);
        
        // Debug.Log($"Positions: {boardPositionsFloor[0, 0].transform.position.x}, {boardPositionsFloor[0, 0].transform.position.y}");
        DrawCorridors(rootSubDungeon);
        DrawRooms(rootSubDungeon);
        
        
        // for (int i = 0; i < boardRows; i++)
        // {
        //     for (int j = 0; j < boardColumns; j++)
        //     {
        //         Debug.Log(boardPositionsFloor[i, j]);
        //         // boardPositionsFloor[i, j]
        //     }
        // }
    }

    public void Start()
    {
        

    }
}
