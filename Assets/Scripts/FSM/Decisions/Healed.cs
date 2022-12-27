using FSM;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Healed")]
    public class Healed : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // Retrieve the player
			var health = stateMachine.gameObject.GetComponent<Health>();
            // Get the Enemy script
            if (health.health == 100)
            {
                return true;
            }
            return false;
        }
    }
}