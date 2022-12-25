using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Chase")]
    public class ChaseAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            // get the zombie controller from the game object
            var zombie = stateMachine.GetComponent<Zombie>();
            //-- Chase the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            float step = zombie.chasingSpeed * Time.deltaTime;
            // move sprite towards the target location
            stateMachine.gameObject.transform.position = Vector2.MoveTowards(stateMachine.gameObject.transform.position, player.transform.position, step);
            // clamp the position of the NPC into the room
            var posX = Mathf.Clamp(stateMachine.gameObject.transform.position.x, zombie.assignedRoom.room.x, zombie.assignedRoom.room.x+zombie.assignedRoom.room.width);
            var posY = Mathf.Clamp(stateMachine.gameObject.transform.position.y, zombie.assignedRoom.room.y, zombie.assignedRoom.room.y+zombie.assignedRoom.room.height);
            stateMachine.gameObject.transform.position = new Vector3(posX, posY, 0);

        }
    }
}