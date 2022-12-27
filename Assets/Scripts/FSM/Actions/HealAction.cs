using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Heal")]
    public class HealAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            // get the zombie controller from the game object
            var zombie = stateMachine.GetComponent<Zombie>();
            // get the health component
            var health = zombie.GetComponent<Health>();
            // regenerate the health of the npc
            health.Recover();

        }
    }
}