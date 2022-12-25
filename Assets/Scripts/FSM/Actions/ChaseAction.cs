using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Chase")]
    public class ChaseAction : FSMAction
    {
        public int speed = 2;
        public override void Execute(BaseStateMachine stateMachine)
        {
            // var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            // var enemySightSensor = stateMachine.GetComponent<EnemySightSensor>();
            //
            // navMeshAgent.SetDestination(enemySightSensor.Player.position);
            // Debug.Log("Chasing...");
            var renderer = stateMachine.gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = new Color(0f, 255f, 0f, 1f);
            }

            //-- Chase the player
            Debug.Log("Chasing player");
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            stateMachine.gameObject.transform.position = Vector2.MoveTowards(stateMachine.gameObject.transform.position, player.transform.position, step);
        }
    }
}