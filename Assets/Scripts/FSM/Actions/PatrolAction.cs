using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
    public class PatrolAction : FSMAction
    {
        //-- Each Zombie gets assigned one room he is patrolling in, patrolling means going in squares around the room --//
        public override void Execute(BaseStateMachine stateMachine)
        {
            // get the zombie controller from the game object
            var zombie = stateMachine.GetComponent<Zombie>();
            // get the next patrolling point
            var goal = zombie.GetPatrolPoint();
            // compute the next step based on the zombie's patrol speed and the game time
            float step = zombie.patrolSpeed * Time.deltaTime;
            // move sprite towards the next patrol location
            stateMachine.gameObject.transform.position = Vector2.MoveTowards(stateMachine.gameObject.transform.position, goal, step);
            // when the zombie reaches the patrol location (close enough <= 0.1) switch to the next poinnt
            if (Vector3.Distance(goal, stateMachine.gameObject.transform.position) <= 0.1)
            {
                zombie.NextPatrolPoint();
            }
            // clamp the position of the NPC into the room
            var posX = Mathf.Clamp(stateMachine.gameObject.transform.position.x, zombie.assignedRoom.room.x, zombie.assignedRoom.room.x+zombie.assignedRoom.room.width);
            var posY = Mathf.Clamp(stateMachine.gameObject.transform.position.y, zombie.assignedRoom.room.y, zombie.assignedRoom.room.y+zombie.assignedRoom.room.height);
            stateMachine.gameObject.transform.position = new Vector3(posX, posY, 0);
        }
    }
}