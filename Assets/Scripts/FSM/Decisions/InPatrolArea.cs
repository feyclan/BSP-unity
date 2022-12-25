using FSM;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/In Patrol Area")]
    public class InPatrolArea : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // get the zombie controller from the game object
            var zombie = stateMachine.GetComponent<Zombie>();
            // Retrieve the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            // Retrieve the zombie's room
            if (zombie.assignedRoom != null)
            {
                var room = zombie.assignedRoom.rect;
                if (room.Contains(player.transform.position))
                {
                    return true;
                }
            }
            return false;
        }
    }
}