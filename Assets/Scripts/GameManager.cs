using System.Collections.Generic;
using BSP;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //-- References to managers --//
    public MainPlayer player;
    public BoardManager board;
    
    //-- Managing levels --//
    private int currentLvl = 1;
    public TextMeshProUGUI levelTxt;
    
    //-- Managing NPC --//
    public GameObject npc;
    public GameObject npcParent;
    private int enemiesInLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        //-- Initialize the board --//
        board.Initialize();
        PlacePlayer();
        PlaceNPC();
    }

    public void NextLevel()
    {
        //-- Update the text that indicates the current level --//
        currentLvl += 1;
        levelTxt.text = $"Level: {currentLvl}";
        //-- Put one more NPC in each level --//
        enemiesInLevel += 1;
        //-- Destroy the old dungeon --//
        board.Reset();
        //-- Destroy the old NPC's --//
        ResetNPC();
        //-- Generate the new dungeon --//
        board.Initialize();
        //-- Place the player in the new dungeon --//
        PlacePlayer();
        //-- Place the NPC's --//
        PlaceNPC();
        
    }

    public void PlacePlayer()
    {
        //-- Retrieve the coordinates of the first room --//
        var origRoom = board.dungeon.GetRoom();
        
        // compute the x- and y- spawning coordinates of the player as the middle of the room
        var spawnX = origRoom.x + (origRoom.width / 2);
        var spawnY = origRoom.y + (origRoom.height / 2);
        player.transform.position = new Vector3(spawnX, spawnY, player.transform.position.z);
    }
    
    //---------//
    //-- NPC --//
    //---------//

    private void PlaceNPC()
    {
        // var room = board.dungeons[0]; 
        foreach (var room in board.dungeons)
        {
            for (var j = 0; j < enemiesInLevel; j++)
            {
                // compute the x- and y- spawning coordinates of the NPC
                int randomX = Mathf.RoundToInt(room.room.x);
                int randomY = Mathf.RoundToInt(room.room.y);
                
                var npcGO = Instantiate(npc, new Vector3(randomX, randomY, 0f), Quaternion.identity);
                var zombie = npcGO.GetComponent<Zombie>();
                //-- Assign the room to the zombie --//
                zombie.assignedRoom = room;
                zombie.InitializePatrolRoute();
                // retrieve the spawn point of this zombie
                var position = zombie.patrolRoute[j];
                npcGO.transform.localPosition = position;
                npcGO.transform.parent = npcParent.transform; 
            }
        }
    }

    public void ResetNPC()
    {
        // Iterate through the children of the parent object
        for (int i = 0; i < npcParent.transform.childCount; i++)
        {
            // Destroy the child object
            GameObject.Destroy(npcParent.transform.GetChild(i).gameObject);
        }
    }
}
