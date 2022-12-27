using FSM;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Well Reached")]
    public class WellReached : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // get the zombie controller from the game object
            var zombie = stateMachine.GetComponent<Zombie>();
            //-- Retrieve the position of the well in the room --//
            var well = zombie.assignedRoom.well;
            var goal = well.transform.position;
            Debug.Log($"distance to well: {Vector3.Distance(zombie.transform.position, goal)}");
            if (Vector3.Distance(zombie.transform.position, goal) < 1)
            {
                return true;
            }
            return false;
        }
    }
}