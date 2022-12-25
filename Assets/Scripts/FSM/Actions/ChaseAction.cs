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
        }
    }
}