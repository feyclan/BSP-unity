using System.Collections;
using System.Collections.Generic;
using BSP;
using UnityEngine;
using UnityEngine.AI;

public class BoardManager : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject floorTile;
    public GameObject floorTile2;
    public GameObject floorTile3;
    public GameObject corridorTile;
    public GameObject corridorTile2;

    public GameObject[,] boardPositionsFloor;
    public SubDungeon dungeon;
    public List<SubDungeon> dungeons = new List<SubDungeon>(); 
    public static List<Rect> corridors = new List<Rect>();
    
    //-- Prefabs --//
    public GameObject well;
    public GameObject door;

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
                        int pick = Random.Range(1, 4);

                        GameObject cor;
                        if (pick == 1)
                        {
                            cor = corridorTile;
                        }
                        else
                        {
                            cor = corridorTile2;
                        }
                        GameObject instance = Instantiate(cor, new Vector3(i, j, 0.0001f), Quaternion.identity) as GameObject;
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
            dungeons.Add(subDungeon);
            for (int i = (int)subDungeon.room.x; i < subDungeon.room.xMax; i++)
            {
                for (int j = (int)subDungeon.room.y; j < subDungeon.room.yMax; j++)
                {
                    int pick = Random.Range(1,4);
                    GameObject tile;
                    if(pick == 1)
                    {
                        tile = floorTile;
                    }
                    else if (pick == 2)
                    {
                        tile = floorTile2;
                    } else
                    {
                        tile = floorTile3;
                    }
                    GameObject instance = Instantiate(tile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
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
        
        // Debug.Log($"Root: {rootSubDungeon}");
        boardPositionsFloor = new GameObject[boardRows, boardColumns];


        //rootSubDungeon.CreateCorridor();
        //CreateCorridorBetween(rootSubDungeon.left, rootSubDungeon.right);
        
        // Debug.Log($"Positions: {boardPositionsFloor[0, 0].transform.position.x}, {boardPositionsFloor[0, 0].transform.position.y}");
        DrawCorridors(rootSubDungeon);
        DrawRooms(rootSubDungeon);
        PlaceExit();
        PlaceWells();
    }

    public void Reset()
    {
        // Iterate through the children of the parent object
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            // Destroy the child object
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }

        dungeons = new List<SubDungeon>();
        boardPositionsFloor = new GameObject[boardRows, boardColumns];
        corridors = new List<Rect>();
    }

    public void PlaceExit()
    {
        //-- Place exit of dungeon in a random room --//
        var lastRoom = dungeons[dungeons.Count-1];
        var doorPosX = (lastRoom.room.position.x + lastRoom.room.width / 2);
        var doorPosY = lastRoom.room.position.y+lastRoom.room.height;
        var doorGO = Instantiate(door, new Vector3(doorPosX, doorPosY, 0f), Quaternion.identity);
        doorGO.transform.parent = transform;
    }

    public void PlaceWells()
    {
        for (var i = 0; i < dungeons.Count; i++)
        {
            // retrieve the current room
            var room = dungeons[i];
            // compute the coordinates of the well (bottom middle)
            var x = room.room.x + (room.room.width / 2);
            var y = room.room.y;
            // Place a well for the NPC to heal
            var wellGO = Instantiate(well, new Vector3(x, y, -9f), Quaternion.identity);
            wellGO.transform.parent = transform;
            // Save the well in the room
            room.well = wellGO;
        }
    }
}
