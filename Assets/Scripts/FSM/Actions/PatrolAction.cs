using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
    public class PatrolAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var zombie = stateMachine.GetComponent<Zombie>();
            //-- Each Zombie gets assigned one room he is patrolling in, patrolling means going in squares around the room --//
            var goal = zombie.GetPatrolPoint();
            float step = zombie.speed * Time.deltaTime;
            // move sprite towards the target location
            stateMachine.gameObject.transform.position = Vector2.MoveTowards(stateMachine.gameObject.transform.position, goal, step);

            if (Vector3.Distance(goal, stateMachine.gameObject.transform.position) < 1)
            {
                zombie.currentPatrolPoint += 1;
                if (zombie.currentPatrolPoint >= zombie.patrolRoute.Count)
                {
                    zombie.currentPatrolPoint = 0;
                }
            }
        }
    }
}