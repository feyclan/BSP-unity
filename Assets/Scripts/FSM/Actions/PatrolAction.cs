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
        }
    }
}