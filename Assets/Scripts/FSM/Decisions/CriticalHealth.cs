using FSM;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Critical Health")]
    public class CriticalHealth : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // Retrieve the player
			var health = stateMachine.gameObject.GetComponent<Health>();
            // Get the Enemy script
            if (health.health <= 20)
            {
                return true;
            }
            return false;
        }
    }
}