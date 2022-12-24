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
            // var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            // var patrolPoints = stateMachine.GetComponent<PatrolPoints>();
            //
            // if (patrolPoints.HasReached(navMeshAgent))
            //     navMeshAgent.SetDestination(patrolPoints.GetNext().position);
            // Debug.Log("Patrolling...");
            //-- Color to indicate state --//
            var renderer = stateMachine.gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = new Color(255f, 0f, 0f, 1f);
            }
        }
    }
}