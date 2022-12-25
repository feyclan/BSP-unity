using BSP;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MainPlayer player;
    public BoardManager board;
    private int currentLvl = 1;
    public TextMeshProUGUI levelTxt;

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
        //-- Update the text that indicates the current level --//
        currentLvl += 1;
        levelTxt.text = $"Level: {currentLvl}";
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
