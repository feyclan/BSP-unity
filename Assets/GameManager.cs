using BSP;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MainPlayer player;
    public BoardManager board;

    // Start is called before the first frame update
    void Start()
    {
        //-- Initialize the board --//
        board.Initialize();
        PlacePlayer();
    }

    public void NextLevel()
    {
        //-- Destroy the old dungeon --//
        board.Reset();
        //-- Generate the new dungeon --//
        board.Initialize();
        //-- Place the player in the new dungeon --//
        PlacePlayer();
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
}
